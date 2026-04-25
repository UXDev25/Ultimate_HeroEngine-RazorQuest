using Ultimate_HeroEngine.Hierarchy;
using Ultimate_HeroEngine.Hierarchy.Entities.Heroes;

namespace Ultimate_HeroEngine.Logic.ProgramEngine;

public static class HeroStorage
{
    private static Team _heroTeamList = new Team();
    public static Team HeroTeamList
    {
        get => _heroTeamList;
        set
        {
            _heroTeamList = value;
        }
    }
}