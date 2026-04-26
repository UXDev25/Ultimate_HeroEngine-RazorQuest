using System.Text.Json.Serialization;
using Ultimate_HeroEngine.Core;
using Ultimate_HeroEngine.Core.Interfaces;
using Ultimate_HeroEngine.Entities;
using Ultimate_HeroEngine.Hierarchy.Abilities;
using Ultimate_HeroEngine.Logic.ProgramEngine;

namespace Ultimate_HeroEngine.Abilities;
using Core.Enums;

[JsonPolymorphic(TypeDiscriminatorPropertyName = "type")]
[JsonDerivedType(typeof(Attack), typeDiscriminator: "Attack")]
[JsonDerivedType(typeof(Defense), typeDiscriminator: "Defense")]
[JsonDerivedType(typeof(Healing), typeDiscriminator: "Healing")]
[JsonDerivedType(typeof(Support), typeDiscriminator: "Support")]
public abstract class Ability : IComparable<Ability>
{
    private int _cost;
    private int _power;
    
    public string Name { get; set; }
    public int Cost { get => _cost;
        set
        {
            _cost = SetAbilityCost(value);
        }
    }
    public ERarity Rarity { get; set; }
    public int Power { get => _power;
        set
        {
            _power = SetAbilityPower(value);
        }
    }
    public ETarget TargetType { get; set; }
    
    [JsonIgnore]
    public Entity? User { get; set; }

    public EClasses ClassType { get; set; }

    public abstract void Execute(Entity? target);
    
    public Ability() { }
    public Ability(string name, int cost, ERarity rarity, int power, ETarget targetType, Entity? user, EClasses classType)
    {
        Name = name;
        Cost = cost;
        Rarity = rarity;
        Power = power;
        TargetType = targetType;
        User = user;
        ClassType = classType;
    }
    
    public virtual Ability Clone()
    {
        return (Ability)this.MemberwiseClone();
    }
    
    //***IComparer***
    public int CompareTo(Ability? other)
    {
        if (other == null) return 1;
        return Rarity.CompareTo(other.Rarity);
    }
    

        /// <summary>
    /// Validates and enforces the ability's resource cost based on its assigned rarity tier. 
    /// If the proposed cost value falls outside the predefined acceptable minimum or maximum bounds 
    /// for its specific rarity, it automatically defaults to the standard maximum cost for that tier.
    /// </summary>
    /// <param name="value">The proposed resource cost value to be evaluated.</param>
    /// <returns>The validated and securely bounded integer cost value.</returns>
    private int SetAbilityCost(int value)
    {
        switch (Rarity)
        {
            case ERarity.Common:
                if (value < GameConfig.Instance.Data.KeyValues.DefFreeCost || value > GameConfig.Instance.Data.KeyValues.DefCommonCost) return GameConfig.Instance.Data.KeyValues.DefCommonCost;
                return value;
            case ERarity.Rare:
                if (value < GameConfig.Instance.Data.KeyValues.DefCommonCost || value > GameConfig.Instance.Data.KeyValues.DefRareCost) return GameConfig.Instance.Data.KeyValues.DefRareCost;
                return value;
            case ERarity.Epic:
                if (value < GameConfig.Instance.Data.KeyValues.DefRareCost || value > GameConfig.Instance.Data.KeyValues.DefEpicCost) return GameConfig.Instance.Data.KeyValues.DefEpicCost;
                return value;
            case ERarity.Legendary:
                if (value < GameConfig.Instance.Data.KeyValues.DefEpicCost) return GameConfig.Instance.Data.KeyValues.DefLegendaryCost;
                return value;
            default: return value;
        }
    }

    /// <summary>
    /// Validates and enforces the ability's power level or effectiveness based on its assigned rarity tier. 
    /// If the proposed power value is lower than the minimum or exceeds the maximum bounds 
    /// for its specific rarity, it automatically defaults to the standard maximum power for that tier.
    /// </summary>
    /// <param name="value">The proposed power value to be evaluated.</param>
    /// <returns>The validated and securely bounded integer power value.</returns>
    private int SetAbilityPower(int value)
    {
        switch (Rarity)
        {
            case ERarity.Common:
                if (value < GameConfig.Instance.Data.KeyValues.DefMinPower || value > GameConfig.Instance.Data.KeyValues.DefCommonPower) return GameConfig.Instance.Data.KeyValues.DefCommonPower;
                return value;
            case ERarity.Rare:
                if (value < GameConfig.Instance.Data.KeyValues.DefCommonPower || value > GameConfig.Instance.Data.KeyValues.DefRarePower) return GameConfig.Instance.Data.KeyValues.DefRarePower;
                return value;
            case ERarity.Epic:
                if (value < GameConfig.Instance.Data.KeyValues.DefRarePower || value > GameConfig.Instance.Data.KeyValues.DefEpicPower) return GameConfig.Instance.Data.KeyValues.DefEpicPower;
                return value;
            case ERarity.Legendary:
                if (value < GameConfig.Instance.Data.KeyValues.DefEpicPower) return GameConfig.Instance.Data.KeyValues.DefLegendaryPower;
                return value;
            default: return value;
        }
    }
}