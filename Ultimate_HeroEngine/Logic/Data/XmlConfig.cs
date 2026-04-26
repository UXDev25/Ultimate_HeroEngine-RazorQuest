using System;
using System.IO;
using System.Xml.Serialization;
using Ultimate_HeroEngine.Core;

namespace Ultimate_HeroEngine.Logic.ProgramEngine;

[XmlRoot("GameConfiguration")]
public class ConfigData
{
    public KeyValues KeyValues { get; set; } = new();
    public Messages Messages { get; set; } = new();
    public UI UI { get; set; } = new();
}

public class GameConfig
{
    private static GameConfig _instance;
    private static readonly object _lock = new object();
    
    public static string ConfigFilePath { get; } = Path.Combine(Directory.GetCurrentDirectory(), "Files", "Config.xml");

    public ConfigData Data { get; private set; }
    public static GameConfig Instance
    {
        get
        {
            lock (_lock)
            {
                if (_instance == null)
                {
                    _instance = new GameConfig();
                    _instance.LoadConfig();
                }
                return _instance;
            }
        }
    }

    private GameConfig() { }

    public void LoadConfig()
    {
        string directory = Path.GetDirectoryName(ConfigFilePath);
        if (!Directory.Exists(directory)) Directory.CreateDirectory(directory);

        if (!File.Exists(ConfigFilePath))
        {
            Data = new ConfigData();
            SaveConfig();
            return;
        }

        XmlSerializer serializer = new XmlSerializer(typeof(ConfigData));
        using (FileStream fs = new FileStream(ConfigFilePath, FileMode.Open))
        {
            Data = (ConfigData)serializer.Deserialize(fs);
        }
    }

    public void SaveConfig()
    {
        XmlSerializer serializer = new XmlSerializer(typeof(ConfigData));
        using (StreamWriter writer = new StreamWriter(ConfigFilePath))
        {
            serializer.Serialize(writer, Data);
        }
    }
    public void ReloadConfig()
    {
        LoadConfig();
    }
}