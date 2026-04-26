using System.Text.Json.Serialization;
using Ultimate_HeroEngine.Abilities;
using Ultimate_HeroEngine.Core;
using Ultimate_HeroEngine.Core.Interfaces;
using Ultimate_HeroEngine.Core.Objects;
using Ultimate_HeroEngine.Entities;
using Ultimate_HeroEngine.Logic.ProgramEngine;

namespace Ultimate_HeroEngine.Hierarchy.Entities.Heroes;

[JsonPolymorphic(TypeDiscriminatorPropertyName = "type")]
[JsonDerivedType(typeof(Warrior), typeDiscriminator: "Warrior")]
[JsonDerivedType(typeof(Mage), typeDiscriminator: "Mage")]
[JsonDerivedType(typeof(Rogue), typeDiscriminator: "Rogue")]
public abstract class Hero : Entity, IUseAbility, IComparable<Hero>
{
    public int KillCount { get; set; }
    public List<Ability> Abilities { get; set; }
    public abstract int CostStat { get; set; }

    public Hero() { }
    public Hero(string name, int level, float hp, float skill, float defenseBuff, List<Ability> abilities) : base(name, level, hp, skill, defenseBuff)
    {
        Abilities = AbilityCatalog.GetRandomAbilities(this);
        AssignAbilitiesToUser();
    }
    

    //**Abilities**
    public void UseAbility(int abilityIndex, ITargetable? target)
    {
        if (target is Entity ent) LiveLog.Log(String.Format(GameConfig.Instance.Data.Messages.UseAbility, ent.GetType().Name, Name, Abilities[abilityIndex].Name));
        if (target is Team team)
        {
            LiveLog.Log(String.Format(GameConfig.Instance.Data.Messages.UseAbility, team.Members.GetType().Name, Name, Abilities[abilityIndex].Name));
            foreach (var member in team.Members)
            {
                Abilities[abilityIndex].Execute(member);
            }
        }
        else Abilities[abilityIndex].Execute((Entity)target!);
    }
    
    public void AssignAbilitiesToUser()
    {
        Abilities.ForEach(ability => ability.User = this);
    }

    public List<Ability> SortAbilitiesList()
    {
        Abilities.Sort();
        return Abilities;
    }

    public override string GetCategoryName() => "Hero";
    public virtual string GetAbilityCostValue() => GameConfig.Instance.Data.KeyValues.DefAbilityCost;

    public override void AttackMeth(Entity? target)
    {
        base.AttackMeth(target);
        if (target!.IsDefeated && target is Enemy) KillCount++;
    }
    
    //*IComparable
    public int CompareTo(Hero? other)
    {
        if (other == null) return 1;
        return KillCount.CompareTo(other.KillCount);
    }
}