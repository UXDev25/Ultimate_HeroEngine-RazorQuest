using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Ultimate_HeroEngine.Entities;
using Ultimate_HeroEngine.Hierarchy.Entities.Heroes;
using Ultimate_HeroEngine.Logic.ProgramEngine;

namespace HeroEngineRazor.Pages;

public class CreateHeroModel : PageModel
{
    [BindProperty]
    public string Name { get; set; }
    
    [BindProperty]
    public int Level { get; set; }
    
    public string WarriorCry { get; set; }

    [BindProperty]
    public string SelectedHeroType { get; set; }

    public void OnGet() { }

    public IActionResult OnPost()
    {
        if (!ModelState.IsValid) return Page();

        Hero newHero;

        // Lògica per crear la instància correcta segons la selecció
        switch (SelectedHeroType)
        {
            case "Warrior":
                newHero = new Warrior(Name, Level, WarriorCry);
                break;
            case "Mage":
                newHero = new Mage(Name, Level);
                break;
            case "Rogue":
                newHero = new Rogue(Name, Level);
                break;
            default:
                return Page();
        }

        MenuManager.ActualCombat.HeroTeam.Members.Add(newHero);

        return RedirectToPage("./Index");
    }
}