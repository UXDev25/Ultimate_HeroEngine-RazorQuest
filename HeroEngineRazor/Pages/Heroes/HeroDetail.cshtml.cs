using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Ultimate_HeroEngine.Entities;
using Ultimate_HeroEngine.Hierarchy.Entities.Heroes;
using Ultimate_HeroEngine.Logic.ProgramEngine;

namespace HeroEngineRazor.Pages;

public class HeroDetailModel : PageModel
{
    public Hero? ActualHero { get; set; }
    
    public void OnGet(string id)
    {
        ActualHero = (Hero)HeroStorage.HeroTeamList.Members.Find(hero => hero.Name == id);
    }
}