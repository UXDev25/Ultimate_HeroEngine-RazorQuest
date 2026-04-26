using Ultimate_HeroEngine.Abilities;
using Ultimate_HeroEngine.Core;
using Ultimate_HeroEngine.Hierarchy.Entities.Heroes;
using Ultimate_HeroEngine.Logic.ProgramEngine;

namespace Ultimate_HeroEngine.Entities;

public class Mage : Hero
{
    public int Mana { get; set; }
    public int ArkLvl { get; set; }
    public override int CostStat 
    { 
        get => Mana;
        set
        {
            Mana = value;
        }
    }
    public Mage() { }

    public Mage(string name, int level, float hp, float skill, float defenseBuff , List<Ability> abilities , int mana, int arkLvl) : base(name, level, hp, skill, defenseBuff, abilities)
    {
        Mana = mana;
        ArkLvl = arkLvl;
    }
    
    public Mage(string name, int level) : this(name, level, GameConfig.Instance.Data.KeyValues.DefMageHp, GameConfig.Instance.Data.KeyValues.DefMageSkill, GameConfig.Instance.Data.KeyValues.DefDefense, new List<Ability>(), GameConfig.Instance.Data.KeyValues.DefMageMana, GameConfig.Instance.Data.KeyValues.DefMageArk) { }
    
    public override string ToString() => base.ToString() + String.Format(GameConfig.Instance.Data.KeyValues.MageIntroduce, Mana, ArkLvl);
    
    public override void LevelUp()
    {
        LiveLog.Log(String.Format(GameConfig.Instance.Data.Messages.LevelUp, GetType().Name, Name));
        base.LevelUp();
        Mana += GameConfig.Instance.Data.KeyValues.DefManaIncrease;
        ArkLvl += GameConfig.Instance.Data.KeyValues.DefArkLvlIncrease;
    }
    
    public override string GetAbilityCostValue() => GameConfig.Instance.Data.KeyValues.MageAbilityCost;
}