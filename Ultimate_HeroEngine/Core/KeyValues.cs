namespace Ultimate_HeroEngine.Core;

public class KeyValues
{
    //***General***
    public  int ActionMaxNumber { get; set; } = 4;
    public  int ActionMinNumber { get; set; } = 1;
    public  float DefPower { get; set; } = 40;
    public  int RandomLevelRange { get; set; } = 4;
    public  int DefTeamSize { get; set; } = 4;
    public  int BossTeamSize { get; set; } = 2;
    public  int MaxAbilityNum { get; set; } = 4;
    public  int MinDefaultDamage { get; set; } = 1;
    
    //***Heroes***
    public  string DefAbilityCost { get; set; } = "Points";
    public  string WarrAbilityCost { get; set; } = "Hp";
    public  string RogueAbilityCost { get; set; } = "Hidden daggers";
    public  string MageAbilityCost { get; set; } = "Mana";
    
    
    //---------------------------Level Increase
    public  float DefHpIncrease { get; set; } = 11.5f;
    public  float DefDefIncrease { get; set; } = 1.5f;
    public  float DefSkillIncrease { get; set; } = 2.5f;
    public  int DefArmorIncrease { get; set; } = 2;
    public  int DefManaIncrease { get; set; } = 10;
    public  int DefArkLvlIncrease { get; set; } = 1;
    public  int DefDaggersIncrease { get; set; } = 1;
    public  float DefMultIncrease { get; set; } = 0.01f;
    
    //--------------------------Default Ability Costs
    public  int DefFreeCost { get; set; } = 0;
    public  int DefCommonCost { get; set; } = 5;
    public  int DefRareCost { get; set; } = 15;
    public  int DefEpicCost { get; set; } = 25;
    public  int DefLegendaryCost { get; set; } = 40;
    
    //--------------------------Default Ability Power
    public  int DefMinPower { get; set; } = 0;
    public  int DefCommonPower { get; set; } = 20;
    public  int DefRarePower { get; set; } = 50;
    public  int DefEpicPower { get; set; } = 80;
    public  int DefLegendaryPower { get; set; } = 120;
    public  float DefDefense { get; set; } = 5;
   
    //-------Warrior
    public  float DefWarriorHp { get; set; } = 150;
    
    public  float DefWarriorSkill { get; set; } = 5;
    public  int DefWarriorArmor { get; set; } = 10;
    
    //-------Rogue
    public  int DefRogueHp { get; set; } = 80;
    public  float DefRogueSkill { get; set; } = 12;
    public  float DefRogueMult { get; set; } = 1.5f;
    public  int DefRogueDaggers { get; set; } = 10;
    
    //-------Mage
    public  float DefMageHp { get; set; } = 100;
    public  float DefMageSkill { get; set; } = 1;
    public  int DefMageMana { get; set; } = 15;
    public  int DefMageArk { get; set; } = 1;

    //--Introduces
    public  string DefIntroduce { get; set; } = "[{0}] {1} | Level: {2} | HP: {3}/{4}";
    public  string WarriorIntroduce { get; set; } = " Armor: {0} | Battle Cry: '{1}'";
    public  string MichaelCry { get; set; } = "For pain and for glory! Bytecroft will not sucumb against bugs!";
    public  string MageIntroduce { get; set; } = " Mana: {0} | arkLvl: {1}";
    public  string RogueIntroduce { get; set; } = " Damage Multiplicator: {0} | Hidden Daggers: {1}";
    
    //***Enemies***
    public  int DefBossPow { get; set; } = 70;
    public  int DefElitePow { get; set; } = 50;
    public  int DefMinionPow { get; set; } = 30;
    
    //----BugBoss
    public  int DefBugBossHp { get; set; } = 1000;
    public  int DefBugBossDefenseBuff { get; set; } = 5;
    public  float DefBugBossSkill { get; set; } = 15.0f;
    public  float DefBugBossDmgMult { get; set; } = 1.25f;
    public  int DefBugBossArmor { get; set; } = 20;
    public  int DefBugBossMana { get; set; } = 250;
    
    // Elite
    public  int DefEliteHp { get; set; } = 300;
    public  float DefEliteSkill { get; set; } = 12.0f;
    public  float DefEliteDefenseBuff { get; set; } = 3.0f;
    public  int DefEliteMana { get; set; } = 100;

    // Minion
    public  float DefMinionHp { get; set; } = 40.0f;
    public  float DefMinionSkill { get; set; } = 3.0f;
    public  float DefMinionDefenseBuff { get; set; } = 1.0f;
    
    public  string EliteIntroduce { get; set; } = " Mana: {0}";
    public  string BossIntroduce { get; set; } = " Damage Multiplicator: {0} | Mana: {1} | Armor: {2}";
}

public class Messages
{
    //**General**
    public  string Error { get; set; } = "Invalid value, please try again";
    
    
    //**In game**
    public  string HeroWin { get; set; } = "All enemies were defeated, the HERO team wins, congratulations!";
    public  string EnemyWin { get; set; } = "All heroes were defeated... the ENEMY team wins... What a shame :(";
    public  string LevelUp { get; set; } = "[{0}] {1} has leveled up! All its stats rised;";
    public  string NoFounds { get; set; } = "insufficient founds to execute this ability";
    
    //-------------Actions
    public  string Attack { get; set; } = "[{0}] {1} Attacked the {2} {3}!";
    public  string Introduce { get; set; } = "{0} introduces itself:";
    public  string Recieved { get; set; } = "[{0}] {1} recieved {2} points of damage! (Total health: {3}/{4})";
    public  string DefeatMsg { get; set; } = "[{0}] {1} has been DEFEATED!";
    public  string Failed { get; set; } = "One action Has failed!";
    
    //------------------------------Abilities
    public  string NoAbility { get; set; } = "{0} has no abilities...";
    public  string CantUseAbility { get; set; } = "{0} tried to do an ability, but doesn't know how to do it!";
    public  string DeadAbility { get; set; } = "{0} tried to do an ability, but is dead...";
    public  string UseAbility { get; set; } = "[{0}] {1} Used the Ability: {2}!";
    public  string Heal { get; set; } = "{0} has recovered {1} Hp"; 
    public  string BuffDefense { get; set; } = "{0} has gained more Defense"; 
    //-----------------------------------------------Effects
    public  string Nothing { get; set; } = "Nothing Happened...";
    public  string Cheer { get; set; } = "'You can do it {0}! i believe in you!'";
    public  string InstaKill { get; set; } = "{0} was suddenly killed...";
    public  string Fact { get; set; } = "Life is a carrussel";
}

public class UI
{
    //**Combat**
    //---------------General
    public  string GenDivider { get; set; } = "==================================================\n{0}\n--------------------------------------------------";
    public  string Divider { get; set; } = "==================================================";
    public  string DividerAlt { get; set; } = "--------------------------------------------------";
    public  string Title { get; set; } = "--ULTIMATE HERO ENGINE--";
    public  string Team { get; set; } = "You are a brave Commander from the ByteCroft Army, you have been assigned to a HERO team, enter any key when you are ready to FIGHT";
    public  string InsertKey { get; set; } = "--INSERT ANY KEY TO CONTINUE--";
    
    //---------------Log
    public  string Log { get; set; } = "BATTLE LOG - Round {0}";
    public  string EntityAction { get; set; } = "[{0}]  {1} > {2} > {3}";
    public  string RemainingEntities { get; set; } = "Remaining enemies: {0} | Heroes standing: {1}";

    public  string TotalStats { get; set; } = "TOTAL STATS:";
    public  string Stats { get; set; } = "[{0}] {1}:\n- total damage: {2}\n- kills: {3}\n- defeated on: {4}";
    public  string Result { get; set; } = "COMBAT RESULT:";
    public  string FinalTotal { get; set; } = "- Total damage made: {0}\n- Best hero: {1}\n- Fastest defeated Enemy: {2}";
    
    //---------------In Game
    public  string StartBattle { get; set; } = "Your team arrives to the battlefield... an enemy team appeared!\n FIGHT!\n";
    
    public  string LifeState { get; set; } = " ALIVE";
    public  string DefeatState { get; set; } = " DEFEATED";

    public  string Round { get; set; } = "--- ROUND {0}---";
    public  string TeamTitle { get; set; } = "{0} Team | {1} remaining";
    public  string EntityStats { get; set; } = "{0} [{1}] {2} | HP: {3}/{4} -> {5}";
    
    public  string ActionList { get; set; } = "- What will {0} do?\n1: Attack\n2: Use Ability\n3: Introduce Themself";
    public  string SelectTarget { get; set; } = "Select the target:";
    public  string GoBackMember { get; set; } = "\n4: Go back";
    
    //----------------------------------------Abilities
    public  string AbilityListTitle { get; set; } = "{0}'s ABILITY LOADOUT: ";
    public  string AbilityStats { get; set; } = "[{0}]  {1} | Type: {2} | Target: {3} | Cost: {4} {5}";
}