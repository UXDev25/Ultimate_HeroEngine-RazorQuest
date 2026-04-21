using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Ultimate_HeroEngine.Hierarchy;
using Ultimate_HeroEngine.Logic.ProgramEngine;

namespace HeroEngineRazor.Pages;

public class HeroesModel : PageModel
{
    public CombatEngine? ActualCombatEngine { get; set; }
    public Team? ActualHeroTeam { get; set; }
    public void OnGet()
    {
        if (ActualCombatEngine == null || ActualHeroTeam == null) return;
        ActualCombatEngine = MenuManager.ActualCombat;
        ActualHeroTeam = MenuManager.ActualCombat.HeroTeam;
    }
}