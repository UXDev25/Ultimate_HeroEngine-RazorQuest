using Ultimate_HeroEngine.Core;
using Ultimate_HeroEngine.Entities;
using Ultimate_HeroEngine.Hierarchy;
using Ultimate_HeroEngine.Hierarchy.Entities.Heroes;

namespace Ultimate_HeroEngine.Logic.ProgramEngine;

public static class UiManager
{
        /// <summary>
    /// Displays the team's header information, including its category and total number of members, 
    /// and then lists all individual members.
    /// </summary>
    /// <param name="team">The team whose header and members will be displayed.</param>
    public static void ListTeam(Team? team)
    {
        string teamData = String.Format(GameConfig.Instance.Data.UI.TeamTitle, team!.Members[0].GetCategoryName(), team.Members.Count + team.DefeatedMembers.Count);
        LiveLog.Log(String.Format(GameConfig.Instance.Data.UI.GenDivider, teamData));
        ShowMembers(team);
    }

    /// <summary>
    /// Prompts the player with a target selection message and displays the list of potential targets 
    /// from the specified team.
    /// </summary>
    /// <param name="targets">The team containing the available target entities.</param>
    public static void ListTargets(Team? targets)
    {
        LiveLog.Log(GameConfig.Instance.Data.UI.SelectTarget);
        ShowMembers(targets);
    }

    /// <summary>
    /// Consolidates both active and defeated members of a team and displays their detailed statistics 
    /// (such as name, HP, class, and defeat status) in a numbered list format.
    /// </summary>
    /// <param name="team">The team whose members' statistics will be shown.</param>
    public static void ShowMembers(Team? team)
    {
        int i = 1;
        List<Entity> allMembers = team!.Members.Concat(team.DefeatedMembers).ToList();
        foreach (var entity in allMembers)
        {
            LiveLog.Log(String.Format(GameConfig.Instance.Data.UI.EntityStats, i, entity.GetCategoryName(), entity.Name, entity.Hp, entity.MaxHp, entity.GetType().Name) + String.Format(entity.IsDefeated ? GameConfig.Instance.Data.UI.DefeatState : ""));
            i++;
        }
    }

    /// <summary>
    /// Displays a formatted list of all available abilities for a specific hero, including their stats 
    /// and costs. If the hero currently has no abilities, a warning message is shown instead.
    /// </summary>
    /// <param name="hero">The hero whose abilities will be listed and sorted.</param>
    public static void ListAbilities(Hero hero)
    {
        LiveLog.Log(String.Format(GameConfig.Instance.Data.UI.GenDivider, String.Format(GameConfig.Instance.Data.UI.AbilityListTitle, hero.Name)));
        if (hero.Abilities.Count == 0)
        {
            LiveLog.Log(GameConfig.Instance.Data.Messages.NoAbility);
            LiveLog.Log(GameConfig.Instance.Data.UI.Divider);
            return;
        }
        foreach (var ability in hero.SortAbilitiesList())
        {
            LiveLog.Log(String.Format(GameConfig.Instance.Data.UI.AbilityStats, ability.Rarity, ability.Name, ability.GetType().Name, ability.TargetType, ability.Cost, hero.GetAbilityCostValue()));
        }
        LiveLog.Log(GameConfig.Instance.Data.UI.Divider);
    }
}