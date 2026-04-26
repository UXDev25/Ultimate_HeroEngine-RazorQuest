using System.Text.RegularExpressions;
using Ultimate_HeroEngine.Abilities;
using Ultimate_HeroEngine.Core.Enums;
using Ultimate_HeroEngine.Core.Interfaces;
using Ultimate_HeroEngine.Hierarchy.Entities.Heroes;
using Ultimate_HeroEngine.Logic.ProgramEngine;

namespace Ultimate_HeroEngine.Logic.Data;

public static class HeroAnalitics<Tkey>
{
    public static List<Hero> GetTopHeroesByLevel(int n)
    {
        return HeroStorage.HeroTeamList.Members
            .OfType<Hero>()
            .OrderByDescending(h => h.Level) 
            .Take(n)
            .ToList();
    }

    public static List<Ability> GetAbilitiesByRarity(ERarity rarity)
    {
        return HeroStorage.HeroTeamList.Members
            .OfType<Hero>()
            .SelectMany(hero => hero.Abilities)
            .Where(ability => ability.Rarity == rarity)
            .ToList();
    }

    public static List<Hero> GetHeroesWithAbilityCount(int min)
    {
        return (from hero in HeroStorage.HeroTeamList.Members.OfType<Hero>() 
                where hero.Abilities.Count >= min
                select hero).ToList();
    }

    public static Dictionary<string, float> GetAverageDamagePerClass()
    {
        return HeroStorage.HeroTeamList.Members
            .OfType<Hero>()
            .GroupBy(hero => hero.GetType().Name) 
            .ToDictionary(
                group => group.Key,
                group => group.Average(hero => hero.Skill) 
            );
    }

    public static List<Hero> SearchHeroesByName(string pattern)
    {
        return HeroStorage.HeroTeamList.Members
            .OfType<Hero>()
            .Where(hero => Regex.IsMatch(hero.Name, pattern, RegexOptions.IgnoreCase))
            .ToList();
    }
}