using Ultimate_HeroEngine.Abilities;
using Ultimate_HeroEngine.Core;
using Ultimate_HeroEngine.Core.Interfaces;
using Ultimate_HeroEngine.Hierarchy.Entities.Heroes;
using Ultimate_HeroEngine.Logic.ProgramEngine;

namespace Ultimate_HeroEngine.Entities;

public class Rogue : Hero
{
    public float DmgMult { get; set; }
    public int HiddenDaggers { get; set; }
    public override int CostStat 
    { 
        get => HiddenDaggers; set
        {
            HiddenDaggers = value;
        } 
    }

    public Rogue() { }
    public Rogue(string name, int level, float hp, float skill, float defenseBuff, List<Ability> abilities, float dmgMult, int hiddenDaggers) : base(name, level, hp, skill, defenseBuff, abilities)
    {
        DmgMult = dmgMult;
        HiddenDaggers = hiddenDaggers;
    }
    
    public Rogue(string name, int level) : this(name, level, GameConfig.Instance.Data.KeyValues.DefRogueHp, GameConfig.Instance.Data.KeyValues.DefRogueSkill, GameConfig.Instance.Data.KeyValues.DefDefense, new List<Ability>(), GameConfig.Instance.Data.KeyValues.DefRogueMult, GameConfig.Instance.Data.KeyValues.DefRogueDaggers) { }
    
    public override string ToString() => base.ToString() + String.Format(GameConfig.Instance.Data.KeyValues.RogueIntroduce, DmgMult, HiddenDaggers);
    
    public override void AttackMeth(Entity? target)
    {
        LiveLog.Log(String.Format(GameConfig.Instance.Data.Messages.Attack, GetType().Name.ToUpper(), Name, target!.GetType().Name, target.Name));
        target.ReceiveDamage(Skill * GameConfig.Instance.Data.KeyValues.DefPower * DmgMult);
    }
    
    public override void LevelUp()
    {
        LiveLog.Log(String.Format(GameConfig.Instance.Data.Messages.LevelUp, GetType().Name, Name));
        base.LevelUp();
        DmgMult += GameConfig.Instance.Data.KeyValues.DefMultIncrease;
        HiddenDaggers += GameConfig.Instance.Data.KeyValues.DefDaggersIncrease;
    }
    
    public override string GetAbilityCostValue() => GameConfig.Instance.Data.KeyValues.RogueAbilityCost;
}