using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Ultimate_HeroEngine.Entities;
using Ultimate_HeroEngine.Files.Data;
using Ultimate_HeroEngine.Hierarchy.Entities.Heroes;
using Ultimate_HeroEngine.Logic.ProgramEngine;

namespace HeroEngineRazor.Pages.Heroes;

public class CreateHeroModel : PageModel
{
    [BindProperty]
    [Required(ErrorMessage =  "Name is required")]
    [StringLength(20, MinimumLength = 3, ErrorMessage = "Name has to have between 3 and 20 characters")]
    public string Name { get; set; }
    
    [BindProperty]
    [Required(ErrorMessage =  "Level is required")]
    [Range(1, 100, ErrorMessage = "Level has to be between 1 and 100")]
    public int Level { get; set; }

    [BindProperty]
    [Required(ErrorMessage = "Please choose a class")]
    public string SelectedHeroType { get; set; }

    public void OnGet() { }

    public IActionResult OnPost()
    {
        if (!ModelState.IsValid) return Page();

        Hero newHero;

        switch (SelectedHeroType)
        {
            case "Warrior":
                newHero = new Warrior(Name, Level);
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

        HeroRepository.Add(newHero);

        return RedirectToPage("./Heroes");
    }
}