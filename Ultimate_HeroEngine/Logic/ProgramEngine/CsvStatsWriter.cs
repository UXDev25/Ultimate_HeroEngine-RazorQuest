using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Ultimate_HeroEngine.Core.Objects;
using Ultimate_HeroEngine.Entities;

namespace Ultimate_HeroEngine.Logic.ProgramEngine;

public class CombatResult
{
    public DateTime Date { get; set; }
    public string HeroesNames { get; set; }
    public string EnemiesNames { get; set; }
    public string Result { get; set; } // "Victoria" o "Derrota"
    public int TotalRounds { get; set; }
    public float TotalDamage { get; set; }
    public string EffectiveHero { get; set; }
}

public static class CsvStatsWriter
{
    private static readonly string FilePath = Path.Combine(Directory.GetCurrentDirectory(), "Files", "combat_stats.csv");

    public static void AppendCombatStats(CombatResult result)
    {
        string directory = Path.GetDirectoryName(FilePath);
        if (!Directory.Exists(directory)) Directory.CreateDirectory(directory);

        bool fileExists = File.Exists(FilePath);

        string newLine = $"{result.Date:yyyy-MM-dd HH:mm:ss}," +
                         $"\"{result.HeroesNames}\"," +
                         $"\"{result.EnemiesNames}\"," +
                         $"{result.Result}," +
                         $"{result.TotalRounds}," +
                         $"{result.TotalDamage:F2}," +
                         $"{result.EffectiveHero}";

        using (StreamWriter sw = new StreamWriter(FilePath, append: true))
        {
            if (!fileExists)
            {
                sw.WriteLine("Date,Heroes,Enemies,Result,Rounds,Damage,EffectiveHero");
            }
            sw.WriteLine(newLine);
        }
    }
}