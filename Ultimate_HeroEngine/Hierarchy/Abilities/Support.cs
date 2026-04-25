using Ultimate_HeroEngine.Abilities;
using Ultimate_HeroEngine.Core;
using Ultimate_HeroEngine.Core.Enums;
using Ultimate_HeroEngine.Entities;
using Ultimate_HeroEngine.Logic.ProgramEngine;

namespace Ultimate_HeroEngine.Hierarchy.Abilities;

public class Support : Ability
{
    public EEffect Effect { get; set; }

    public Support() { }
    public Support(string name, int cost, ERarity rarity, int power, ETarget targetType, Entity? user, EEffect effect, EClasses classType) : base(name, cost, rarity, power, targetType, user, classType)
    {
        Effect = effect;
    }

    public override void Execute(Entity? target)
    {
        switch (Effect)
        {
            case EEffect.Cheer: 
                LiveLog.Log(String.Format(Messages.Cheer, target!.Name));
                break;
            case EEffect.InstaKill:
                LiveLog.Log(String.Format(Messages.InstaKill, target!.Name));
                target.Hp = 0;
                break;
            case EEffect.TellFacts:
                LiveLog.Log(Messages.Fact);
                break;
            default: 
                LiveLog.Log(Messages.Nothing);
                break;
        }
    }
}