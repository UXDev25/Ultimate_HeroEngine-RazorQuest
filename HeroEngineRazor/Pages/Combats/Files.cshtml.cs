using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Ultimate_HeroEngine.Logic.ProgramEngine;
namespace HeroEngineRazor.Pages.Combats;
public class FilesModel : PageModel
{
    public List<CombatResult> LastMatches { get; set; } = new List<CombatResult>();
    private readonly string FilePath = Path.Combine(Directory.GetCurrentDirectory(), "Files", "combat_stats.csv");

    [BindProperty]
    public ConfigData CurrentConfig { get; set; }
    
    public void OnGet()
    {
        if (!System.IO.File.Exists(FilePath)) return;

        var lines = System.IO.File.ReadAllLines(FilePath).Skip(1).ToList();

        foreach (var line in lines.TakeLast(10).Reverse())
        {
            var parts = line.Split(',');
            if (parts.Length >= 7)
            {
                LastMatches.Add(new CombatResult
                {
                    Date = DateTime.Parse(parts[0]),
                    HeroesNames = parts[1].Trim('"'),
                    EnemiesNames = parts[2].Trim('"'),
                    Result = parts[3],
                    TotalRounds = int.Parse(parts[4]),
                    TotalDamage = float.Parse(parts[5]),
                    EffectiveHero = parts[6]
                });
            }
        }
        CurrentConfig = GameConfig.Instance.Data;
    }
    
    public IActionResult OnPostUpdateConfig()
    {
        if (CurrentConfig != null)
        {
            GameConfig.Instance.Data.KeyValues.DefWarriorHp = CurrentConfig.KeyValues.DefWarriorHp;
            GameConfig.Instance.Data.KeyValues.DefEliteHp = CurrentConfig.KeyValues.DefEliteHp;
            GameConfig.Instance.Data.Messages.HeroWin = CurrentConfig.Messages.HeroWin;
            
            GameConfig.Instance.SaveConfig();
        }

        return RedirectToPage();
    }
    

    public IActionResult OnGetDownload()
    {
        if (!System.IO.File.Exists(FilePath)) return NotFound();
        var bytes = System.IO.File.ReadAllBytes(FilePath);
        
        return File(bytes, "text/csv", "combat_stats.csv");
    }
}