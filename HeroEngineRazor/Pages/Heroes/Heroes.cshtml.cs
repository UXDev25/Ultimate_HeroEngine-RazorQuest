using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Ultimate_HeroEngine.Hierarchy;
using Ultimate_HeroEngine.Logic.ProgramEngine;

namespace HeroEngineRazor.Pages.Heroes;

public class HeroesModel : PageModel
{
    public Team ActualHeroTeam { get; set; } = new Team();
    public void OnGet()
    {
        ActualHeroTeam = HeroStorage.HeroTeamList;
    }
}