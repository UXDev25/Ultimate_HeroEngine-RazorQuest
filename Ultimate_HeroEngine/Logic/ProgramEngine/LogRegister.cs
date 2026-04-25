using Ultimate_HeroEngine.Hierarchy;

namespace Ultimate_HeroEngine.Logic.ProgramEngine;

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

public static class LogRegister
{
    public static List<String> ActionData { get; set; } = new List<String>();
    private const string SessionSeparator = "=====DONT DELETE THIS SEPARATOR=====";
    private static readonly string FilePath = Path.Combine(Directory.GetCurrentDirectory(), "Files", "BattleLogs.txt");

    public static void SaveCombatSession(Team heroes, Team enemies, string finalResult)
    {
        EnsureDirectoryExists();

        var lines = new List<string>();
        
        lines.Add("==================================================");
        lines.Add($"COMBAT DATE: {DateTime.Now:yyyy-MM-dd HH:mm:ss}");
        
        string heroNames = string.Join(", ", heroes.Members.Select(h => h.Name));
        string enemyNames = string.Join(", ", enemies.Members.Select(e => e.Name));
        
        lines.Add($"HEROES: {heroNames}");
        lines.Add($"ENEMIES: {enemyNames}");
        lines.Add("--------------------------------------------------");
        
        lines.AddRange(ActionData);
        
        lines.Add("--------------------------------------------------");
        lines.Add($"RESULT: {finalResult}");
        lines.Add("\n"); 

        File.AppendAllLines(FilePath, lines);

        ActionData.Clear();
    }

    public static List<string> GetLastCombatLog()
    {
        if (!File.Exists(FilePath)) 
            return new List<string> { "There is no registered combat yet." };

        string fullText = File.ReadAllText(FilePath);
        
        string[] allCombats = fullText.Split(new[] { SessionSeparator }, StringSplitOptions.RemoveEmptyEntries);
        
        if (allCombats.Length == 0) 
            return new List<string> { "Historial is empty." };

        string lastCombat = allCombats.Last();

        return lastCombat.Trim().Split('\n').Select(line => line.TrimEnd('\r')).ToList();
    }

    private static void EnsureDirectoryExists()
    {
        string? directory = Path.GetDirectoryName(FilePath);
        if (!Directory.Exists(directory))
        {
            Directory.CreateDirectory(directory!);
        }
    }
}