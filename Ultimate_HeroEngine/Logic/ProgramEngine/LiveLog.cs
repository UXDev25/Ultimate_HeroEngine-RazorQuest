namespace Ultimate_HeroEngine.Logic.ProgramEngine;

public static class LiveLog
{
    public static List<string> CombatLog { get; set; } = new List<string>();

    public static void Log(string message)
    {
        CombatLog.Add(message);
    }
}