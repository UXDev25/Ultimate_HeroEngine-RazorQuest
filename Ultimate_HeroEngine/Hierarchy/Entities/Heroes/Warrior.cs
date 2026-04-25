using Ultimate_HeroEngine.Abilities;
using Ultimate_HeroEngine.Core;
using Ultimate_HeroEngine.Hierarchy.Entities.Heroes;
using Ultimate_HeroEngine.Logic.ProgramEngine;

namespace Ultimate_HeroEngine.Entities;

public class Warrior : Hero
{
    public int Armor { get; set; }
    public string BattleCry { get; set; }

    public override int CostStat
    {
        get => Armor;
        set
        {
            Armor = value;
        }
    }

    public Warrior() { }
    public Warrior(string name, int level, float hp, float skill, float defenseBuff, List<Ability> abilities, int armor, string battleCry) : base(name, level, hp, skill, defenseBuff, abilities)
    {
        Armor = armor;
        BattleCry = battleCry;
    }
    public Warrior(string name, int level) : this(name, level, KeyValues.DefWarriorHp, KeyValues.DefWarriorSkill, KeyValues.DefDefense, new List<Ability>(), KeyValues.DefWarriorArmor, KeyValues.MichaelCry) { }
    
    public override string ToString()
    {
        return base.ToString() + (KeyValues.WarriorIntroduce, Armor, BattleCry);
    }

    public override void ReceiveDamage(float damage)
    {
        float actualDamage = damage - (1 + (DefenseBuff / 10)) - (DefenseBuff / 10) * (Armor / 100);
        actualDamage = Math.Max(KeyValues.MinDefaultDamage, actualDamage);
        Hp -= actualDamage;
        LiveLog.Log(String.Format(Messages.Recieved, GetType().Name.ToUpper(), Name, actualDamage, Hp, MaxHp));
    }
    
    public override void LevelUp()
    {
        LiveLog.Log(String.Format(Messages.LevelUp, GetType().Name, Name));
        base.LevelUp();
        Armor += KeyValues.DefArmorIncrease;
    }
}