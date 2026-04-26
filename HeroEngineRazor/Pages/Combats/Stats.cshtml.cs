using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Ultimate_HeroEngine.Hierarchy.Entities.Heroes;
using Ultimate_HeroEngine.Logic.Data;
using Ultimate_HeroEngine.Logic.ProgramEngine;

namespace HeroEngineRazor.Pages.Combats;

public class Stat
{
    public string ClassName { get; set; }
    public int Count { get; set; }
    public double Percentage { get; set; }
}

public class StatsModel : PageModel
{
    public int TotalHeroes { get; set; }
    public List<Stat> ClassDistribution { get; set; } = new List<Stat>();
    public List<Hero> TopHeroes { get; set; } = new List<Hero>();
    public Dictionary<string, int> CommonAbilities { get; set; } = new Dictionary<string, int>();
    public List<CombatResult> CombatHistory { get; set; } = new List<CombatResult>();

    [BindProperty(SupportsGet = true)]
    public string FilterResult { get; set; }

    public void OnGet()
    {
        var allHeroes = HeroStorage.HeroTeamList.Members.OfType<Hero>().ToList();
        TotalHeroes = allHeroes.Count;

        if (TotalHeroes > 0)
        {
            ClassDistribution = allHeroes
                .GroupBy(h => h.GetType().Name)
                .Select(g => new Stat
                {
                    ClassName = g.Key,
                    Count = g.Count(),
                    Percentage = Math.Round((double)g.Count() / TotalHeroes * 100, 2)
                })
                .OrderByDescending(c => c.Count)
                .ToList();
        }
        
        TopHeroes = HeroAnalitics<object>.GetTopHeroesByLevel(3);
        
        CommonAbilities = allHeroes
            .SelectMany(h => h.Abilities)
            .GroupBy(a => a.GetType().Name)
            .OrderByDescending(g => g.Count())
            .Take(5)
            .ToDictionary(g => g.Key, g => g.Count());

        string filePath = Path.Combine(Directory.GetCurrentDirectory(), "Files", "combat_stats.csv");
        if (System.IO.File.Exists(filePath))
        {
            var lines = System.IO.File.ReadAllLines(filePath).Skip(1);
            foreach (var line in lines)
            {
                var parts = line.Split(',');
                if (parts.Length >= 7)
                {
                    CombatHistory.Add(new CombatResult
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

            if (!string.IsNullOrEmpty(FilterResult) && FilterResult != "All")
            {
                CombatHistory = CombatHistory
                    .Where(c => c.Result.Equals(FilterResult, StringComparison.OrdinalIgnoreCase))
                    .ToList();
            }

            CombatHistory = CombatHistory.OrderByDescending(c => c.Date).ToList();
        }
    }
}