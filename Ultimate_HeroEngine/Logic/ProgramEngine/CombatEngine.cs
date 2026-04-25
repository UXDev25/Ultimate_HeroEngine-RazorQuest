using Ultimate_HeroEngine.Abilities;
using Ultimate_HeroEngine.Core;
using Ultimate_HeroEngine.Core.Enums;
using Ultimate_HeroEngine.Core.Interfaces;
using Ultimate_HeroEngine.Entities;
using Ultimate_HeroEngine.Hierarchy;
using Ultimate_HeroEngine.Hierarchy.Entities.Heroes;
using Ultimate_HeroEngine.Logic.AI;
using Ultimate_HeroEngine.Logic.SupportClasses;
using Utils;

namespace Ultimate_HeroEngine.Logic.ProgramEngine;


public class CombatEngine
{
    private Team? _enemyTeam; 
    private Team? _heroTeam;
    private Team _allEntities;

    private Team _initalHeroTeam;
    private Team _initialEnemyTeam;
    
    public int Round { get; set; }

    public Team? HeroTeam
    {
        get => _heroTeam;
        set
        {
            if (value!.Members.Any(entity => entity is not Hero)) 
                throw new ArgumentException("Hero Team only can contain Heroes");
            _heroTeam = value;
        }
    }
    
    public Team? EnemyTeam
    {
        get => _enemyTeam;
        set
        {
            if (value!.Members.Any(entity => entity is not Enemy)) 
                throw new ArgumentException("Enemy Team only can contain Enemies");
            _enemyTeam = value;
        }
    }

    public int FastestDefeatedEnemy { get; set; }

    public CombatEngine(Team heroes, Team enemies, Team enemyTeam, Team heroTeam)
    {
        HeroTeam = heroes;
        EnemyTeam = enemies;
        _enemyTeam = enemyTeam;
        _heroTeam = heroTeam;
        _allEntities = new Team();
        UpdateEntitiesList();
        Round = 1;
    }
    public CombatEngine(Team heroes, Team enemyTeam, Team heroTeam)
    {
        FastestDefeatedEnemy = 0;
        HeroTeam = heroes;
        _enemyTeam = enemyTeam;
        _heroTeam = heroTeam;
        EnemyTeam = new Team();
        _allEntities = new Team();
        UpdateEntitiesList();
        Round = 1;
    }
    public CombatEngine()
    {
        FastestDefeatedEnemy = 0;
        HeroTeam = new Team();
        EnemyTeam = new Team();
        _allEntities = new Team();
        UpdateEntitiesList();
        Round = 1;
    }
    
    

    
    /// <summary>
    /// Starts a new battle, goes on a loop until EnemyTeam.Members.Count == 0 or HeroTeam.Members.Count == 0
    /// </summary>
    /// <returns>true if EnemyTeam.Members.Count == 0, </returns>
    /// <returns>false if HeroTeam.Members.Count == 0</returns>
    public bool StartBattle()
    {
        _initalHeroTeam = HeroTeam;
        _initialEnemyTeam = EnemyTeam;
        List<CombatAction> actions;
        LiveLog.Log(UI.StartBattle);
        UpdateEntitiesList();
        Round = 1;
        while (EnemyTeam!.Members.Count != 0 && HeroTeam!.Members.Count != 0)
        {
            LiveLog.Log(String.Format(UI.Round, Round));
            UiManager.ListTeam(HeroTeam);
            actions = GetTeamActions(HeroTeam.Members);
            actions.AddRange(EnemyAi.GetEnemyActions(EnemyTeam, HeroTeam, _allEntities));
            actions.Sort();
            LogRegister.ActionData.Add(String.Format(UI.GenDivider, String.Format(UI.Log, Round)));
            ExecuteActions(actions);
            UpdateEntitiesList();
            Round++;
        }
        string winMsg = HeroTeam!.Members.Count != 0 ? Messages.HeroWin : Messages.EnemyWin; 
        LiveLog.Log(winMsg);
        LogRegister.SaveCombatSession(_initalHeroTeam, _initialEnemyTeam, winMsg);
        WriteOntoCsv();
        if (HeroTeam.Members.Count == 0) return true;
        HeroTeam.Members.ForEach(hero => hero.LevelUp());
        return false;
    }

    private void WriteOntoCsv()
    {
        var stats = new CombatResult
        {
            Date = DateTime.Now,
            HeroesNames = string.Join(" / ", HeroTeam.Members.Select(h => h.Name)),
            EnemiesNames = string.Join(" / ", EnemyTeam.Members.Select(e => e.Name)),
            Result = HeroTeam.Members.Count > 0 ? "Victoria" : "Derrota",
            TotalRounds = Round - 1,
            TotalDamage = StatCalculator.AllCombatDamage,
            // Fem servir el teu mètode de StatCalculator
            EffectiveHero = HeroTeam.Members.Count > 0 ? StatCalculator.CalculateEffectiveHero(HeroTeam).Name : "Cap"
        };

        CsvStatsWriter.AppendCombatStats(stats);
    }

    /// <summary>
    /// Resets the combat statistics and hero states for a new battle or round. 
    /// It clears the defeated enemy tracking variables, fully restores the health of all active hero team members, 
    /// and resets their defense buffs to the default values.
    /// </summary>
    public void RestartStats()
    {
        FastestDefeatedEnemy = 0;
        StatCalculator.FastestDefeatedEnemy = null;
        foreach (var member in HeroTeam!.Members)
        {
            member.Hp = member.MaxHp;
            member.DefenseBuff = KeyValues.DefDefense;
        }
    }

    //**PRIVATE METHODS**
    
    private void UpdateEntitiesList()
    {
        HeroTeam!.CleanDefeatedMembers();
        EnemyTeam!.CleanDefeatedMembers();
        _allEntities.Members.Clear();
        _allEntities.Members.AddRange(HeroTeam.Members);
        _allEntities.Members.AddRange(EnemyTeam.Members);
    }
    
    private List<CombatAction> GetTeamActions(List<Entity> heroTeam)
    {
        var actionList = new List<CombatAction?>();
        int i = 0;
        var rand = new Random();
        while (i < heroTeam.Count)
        {
            Hero currentHero = (Hero)heroTeam[i];
            UiManager.ListTeam(EnemyTeam);
            LiveLog.Log(String.Format(UI.ActionList, currentHero.Name) + (i == 0 ? "" : UI.GoBackMember));
            int actionChoiceRaw = rand.Next(KeyValues.ActionMinNumber, KeyValues.ActionMaxNumber);
            ECombatAction actionChoice = (ECombatAction)actionChoiceRaw; 
            
            if (actionChoice != ECombatAction.GoBack)
            {
                CombatAction? pendingAction = ManageAction(currentHero, actionChoice);
                if (pendingAction == null)
                {
                    LiveLog.Log(Messages.NoFounds);
                }
                else
                {
                    actionList.Add(pendingAction);
                    i++;
                }

            }
            else if (i > 0)
            {
                actionList.RemoveAt(actionList.Count - 1); 
                i--;
            }
        }
        return actionList!;
    }

    private CombatAction? ManageAction(Hero hero, ECombatAction actionChoice)
    {
        ITargetable? target;
        switch (actionChoice)
        {
            case ECombatAction.Attack:
                target = SelectTarget(hero, null);
                return new CombatAction(hero, actionChoice, target);
            
            case ECombatAction.Ability:
                int chosenAbilityIndex = SelectHability(hero);
                Ability choosenAbility = hero.Abilities[chosenAbilityIndex];
                if (hero.CostStat < choosenAbility.Cost) return null;
                target = SelectTarget(hero, choosenAbility);
                hero.CostStat -= choosenAbility.Cost;
                return new CombatAction(hero, actionChoice, chosenAbilityIndex, target);
            
            case ECombatAction.Introduce: return new CombatAction(hero, actionChoice);
            
            default: return new CombatAction(hero, actionChoice);
        }
    }

    private int SelectHability(Hero hero)
    {
        var rand = new Random();
        UiManager.ListAbilities(hero);
        int abilityChoice = rand.Next(1, hero.Abilities.Count + 1);
        return abilityChoice - 1; 
    }

    private ITargetable? SelectTarget(Hero hero, Ability? chosenAbility)
    {
        ETarget currentTargetType = chosenAbility?.TargetType ?? ETarget.SingleEnemy; 
        int targetChoice = 0;
        var rand = new Random();
        switch (currentTargetType)
        {
            case ETarget.SingleAny:
                UiManager.ListTargets(_allEntities);
                targetChoice = rand.Next(1, _allEntities.Members.Count + 1);
                return _allEntities.Members[targetChoice - 1];
            
            case ETarget.SingleAlly:
                UiManager.ListTargets(HeroTeam);
                targetChoice = rand.Next(1, HeroTeam!.Members.Count);
                return HeroTeam.Members[targetChoice - 1];
            
            case ETarget.SingleEnemy:
                UiManager.ListTargets(EnemyTeam);
                targetChoice = rand.Next(1, EnemyTeam!.Members.Count);
                return EnemyTeam.Members[targetChoice - 1];
            
            case ETarget.Self:
                return hero;
            
            case ETarget.AllAllies: return HeroTeam;
            case ETarget.AllEnemies: return EnemyTeam;
            case ETarget.Everyone: return _allEntities;
            default: return null;
        }
    }

    private void ExecuteActions(List<CombatAction> actionList)
    {
        foreach(var entry in actionList)
        {
            if (HeroTeam!.Members.Count == 0 || EnemyTeam!.Members.Count == 0) return;       
                Entity currentUser = entry.SelectedUser;
                if (currentUser.IsDefeated)
                {
                    LiveLog.Log(String.Format(Messages.DeadAbility, currentUser.Name));
                    continue;
                }
                if (entry.Target is Entity targetEntity && targetEntity.IsDefeated && entry.ActionType == ECombatAction.Attack)
                {
                    LiveLog.Log(Messages.Failed);
                    continue;
                }

                if (entry.Target is Enemy targetEnemy && targetEnemy.IsDefeated && FastestDefeatedEnemy < 1)
                {
                    StatCalculator.FastestDefeatedEnemy = targetEnemy;
                    FastestDefeatedEnemy++;
                }
                
                switch (entry.ActionType)
                {
                    case ECombatAction.Attack:
                        currentUser.AttackMeth((Entity)entry.Target!);
                        break;
                    case ECombatAction.Introduce:
                        LiveLog.Log(String.Format(Messages.Introduce, currentUser.Name));
                        LiveLog.Log(currentUser.ToString());
                        break;
                    case ECombatAction.Ability:
                        if (currentUser is IUseAbility abilityUser)
                        {
                            abilityUser.UseAbility(entry.AbilityIndex, entry.Target);
                        }
                        else
                        {
                            LiveLog.Log(Messages.CantUseAbility);
                        }
                        break;
                }

            
                LogRegister.ActionData.Add(String.Format(UI.EntityAction, currentUser.GetType().Name, currentUser.Name, entry.ActionType, StatCalculator.LastDamagePoint));
        }
    }
}