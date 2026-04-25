using System;
using System.IO;
using System.Xml.Serialization;

namespace Ultimate_HeroEngine.Logic.ProgramEngine;

// Aquestes classes representen l'estructura del XML
public class KeyValuesConfig
{
    // ***General***
    public int ActionMaxNumber { get; set; } = 4;
    public int ActionMinNumber { get; set; } = 1;
    public float DefPower { get; set; } = 40;
    public int RandomLevelRange { get; set; } = 4;
    public int DefTeamSize { get; set; } = 4;
    public int BossTeamSize { get; set; } = 2;
    public int MaxAbilityNum { get; set; } = 4;
    public int MinDefaultDamage { get; set; } = 1;

    // Afegeix aquí la resta de propietats (WarriorHp, BossHp, etc...) seguint aquest format.
    // Només te'n poso unes quantes com a exemple per no fer un bloc de codi de 200 línies.
    public float DefWarriorHp { get; set; } = 150;
    public int DefEliteHp { get; set; } = 300;
}

public class UIConfig
{
    public string Title { get; set; } = "--ULTIMATE HERO ENGINE--";
    public string Log { get; set; } = "BATTLE LOG - Round {0}";
    public string EntityAction { get; set; } = "[{0}]  {1} > {2} > {3}";
    // ... resta de variables de UI
}

public class MessagesConfig
{
    public string Error { get; set; } = "Invalid value, please try again";
    public string HeroWin { get; set; } = "All enemies were defeated, the HERO team wins, congratulations!";
    // ... resta de variables de Messages
}

// Aquesta és la classe arrel que se serialitzarà a XML
[XmlRoot("GameConfiguration")]
public class ConfigData
{
    public KeyValuesConfig KeyValues { get; set; } = new KeyValuesConfig();
    public MessagesConfig Messages { get; set; } = new MessagesConfig();
    public UIConfig UI { get; set; } = new UIConfig();
}

public class GameConfig
{
    private static GameConfig _instance;
    private static readonly object _lock = new object();
    
    // Ruta on guardarem el fitxer XML
    public static string ConfigFilePath { get; } = Path.Combine(Directory.GetCurrentDirectory(), "Files", "Config.xml");

    // Les dades carregades a memòria
    public ConfigData Data { get; private set; }

    // Propietat Singleton
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

        // Si no existeix el fitxer, el creem amb els valors per defecte de les classes
        if (!File.Exists(ConfigFilePath))
        {
            Data = new ConfigData();
            SaveConfig();
            return;
        }

        // Si existeix, el llegim amb XmlSerializer
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
    
    // Per quan fem canvis des de la web i volem que el sistema els forci a recarregar
    public void ReloadConfig()
    {
        LoadConfig();
    }
}