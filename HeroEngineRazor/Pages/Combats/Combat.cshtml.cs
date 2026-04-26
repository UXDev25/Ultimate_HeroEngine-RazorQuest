using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Ultimate_HeroEngine.Hierarchy;
using Ultimate_HeroEngine.Logic.ProgramEngine;

namespace HeroEngineRazor.Pages;

public class Combat : PageModel
{
    
    public List<string> UltimatCombatText { get; set; } = new List<string>();
    public List<string> CombatRegister { get; set; } = new List<string>();
    public Boolean hasHeroes { get; set; } = true;
    
    public void OnGet()
    {
        UltimatCombatText = LogRegister.GetLastCombatLog();
    }
    public IActionResult OnPostSimulate()
    {
        CombatEngine engine = new CombatEngine();
        hasHeroes = MenuManager.CombatFlow(engine);
        if (!hasHeroes) return Page();
        CombatRegister = LiveLog.CombatLog;
        UltimatCombatText = LogRegister.GetLastCombatLog();
        return Page();
    }
    
}