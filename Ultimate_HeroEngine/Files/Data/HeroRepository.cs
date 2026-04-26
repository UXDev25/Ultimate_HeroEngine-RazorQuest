using System.Text.Json;
using Ultimate_HeroEngine.Hierarchy.Entities.Heroes;
using Ultimate_HeroEngine.Logic.ProgramEngine;

namespace Ultimate_HeroEngine.Files.Data
{
    public static class HeroRepository
    {
        private const string FilePath = "heroes.json";
        private static readonly JsonSerializerOptions JsonOptions = new JsonSerializerOptions
        {
            WriteIndented = true,
            PropertyNameCaseInsensitive = true
        };

        public static List<Hero> LoadAll()
        {
            if (!File.Exists(FilePath))
            {
                return new List<Hero>(); 
            }

            string json = File.ReadAllText(FilePath);
            
            if (string.IsNullOrWhiteSpace(json)) return new List<Hero>();
            var heroes = JsonSerializer.Deserialize<List<Hero>>(json, JsonOptions) ?? new List<Hero>();
            foreach (var hero in heroes)
            {
                hero.AssignAbilitiesToUser();
            }

            return heroes;
        }

        public static void SaveAll(IEnumerable<Hero> heroes)
        {
            string json = JsonSerializer.Serialize(heroes, JsonOptions);
            File.WriteAllText(FilePath, json);
        }

        public static void Add(Hero hero)
        {
            HeroStorage.HeroTeamList.Members.Add(hero);
            SaveAll(HeroStorage.HeroTeamList.Members.Cast<Hero>());
        }

        public static void Delete(string nom)
        {
            var hero = HeroStorage.HeroTeamList.Members.FirstOrDefault(h => h.Name == nom);
            if (hero != null)
            {
                HeroStorage.HeroTeamList.Members.Remove(hero);
                SaveAll(HeroStorage.HeroTeamList.Members.Cast<Hero>());
            }
        }
    }
}