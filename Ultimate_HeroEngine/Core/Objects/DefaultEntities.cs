using Ultimate_HeroEngine.Abilities;
using Ultimate_HeroEngine.Core.Interfaces;
using Ultimate_HeroEngine.Entities;
using Ultimate_HeroEngine.Hierarchy.Entities.Enemies;
using Ultimate_HeroEngine.Hierarchy.Entities.Heroes;
using Ultimate_HeroEngine.Logic.ProgramEngine;

namespace Ultimate_HeroEngine.Core.Objects;

 public static class DefaultEntities
 {
        public static IReadOnlyList<Hero> Heroes { get; } = new List<Hero>
        {
            new Warrior("Michael", 1, GameConfig.Instance.Data.KeyValues.DefWarriorHp, GameConfig.Instance.Data.KeyValues.DefWarriorSkill, GameConfig.Instance.Data.KeyValues.DefDefense, new List<Ability>(), GameConfig.Instance.Data.KeyValues.DefWarriorArmor, GameConfig.Instance.Data.KeyValues.MichaelCry),
            new Mage("Cheich", 1, GameConfig.Instance.Data.KeyValues.DefMageHp, GameConfig.Instance.Data.KeyValues.DefMageSkill, GameConfig.Instance.Data.KeyValues.DefDefense, new List<Ability>(), GameConfig.Instance.Data.KeyValues.DefMageMana, GameConfig.Instance.Data.KeyValues.DefMageArk),
            new Rogue("Gonzalez", 1, GameConfig.Instance.Data.KeyValues.DefRogueHp
                , GameConfig.Instance.Data.KeyValues.DefRogueSkill, GameConfig.Instance.Data.KeyValues.DefDefense, new List<Ability>(), GameConfig.Instance.Data.KeyValues.DefRogueMult, GameConfig.Instance.Data.KeyValues.DefRogueDaggers),
        };
        
        public static IReadOnlyList<Enemy> Enemies { get; } = new List<Enemy>
        {
            //Minions
                new Minion("Little Goblin", 1, GameConfig.Instance.Data.KeyValues.DefMinionHp, GameConfig.Instance.Data.KeyValues.DefMinionSkill, GameConfig.Instance.Data.KeyValues.DefMinionDefenseBuff),
                new Minion("Slime", 1, GameConfig.Instance.Data.KeyValues.DefMinionHp, GameConfig.Instance.Data.KeyValues.DefMinionSkill, GameConfig.Instance.Data.KeyValues.DefMinionDefenseBuff),
                new Minion("Skeleton Warrior", 1, GameConfig.Instance.Data.KeyValues.DefMinionHp, GameConfig.Instance.Data.KeyValues.DefMinionSkill, GameConfig.Instance.Data.KeyValues.DefMinionDefenseBuff),
                new Minion("Rabid Wolf", 1, GameConfig.Instance.Data.KeyValues.DefMinionHp, GameConfig.Instance.Data.KeyValues.DefMinionSkill, GameConfig.Instance.Data.KeyValues.DefMinionDefenseBuff),
                new Minion("Bandit Scout", 1, GameConfig.Instance.Data.KeyValues.DefMinionHp, GameConfig.Instance.Data.KeyValues.DefMinionSkill, GameConfig.Instance.Data.KeyValues.DefMinionDefenseBuff),
                new Minion("Cave Bat", 1, GameConfig.Instance.Data.KeyValues.DefMinionHp, GameConfig.Instance.Data.KeyValues.DefMinionSkill, GameConfig.Instance.Data.KeyValues.DefMinionDefenseBuff),
                
                //Elites
                new Elite("Elite Guard", 1, GameConfig.Instance.Data.KeyValues.DefEliteHp, GameConfig.Instance.Data.KeyValues.DefEliteSkill, GameConfig.Instance.Data.KeyValues.DefEliteDefenseBuff, new List<Ability>(), GameConfig.Instance.Data.KeyValues.DefEliteMana),
                new Elite("Orc Warlord", 1, GameConfig.Instance.Data.KeyValues.DefEliteHp, GameConfig.Instance.Data.KeyValues.DefEliteSkill, GameConfig.Instance.Data.KeyValues.DefEliteDefenseBuff, new List<Ability>(), GameConfig.Instance.Data.KeyValues.DefEliteMana),
                new Elite("Dark Cultist", 1, GameConfig.Instance.Data.KeyValues.DefEliteHp, GameConfig.Instance.Data.KeyValues.DefEliteSkill, GameConfig.Instance.Data.KeyValues.DefEliteDefenseBuff, new List<Ability>(), GameConfig.Instance.Data.KeyValues.DefEliteMana),
                new Elite("Armored Golem", 1, GameConfig.Instance.Data.KeyValues.DefEliteHp, GameConfig.Instance.Data.KeyValues.DefEliteSkill, GameConfig.Instance.Data.KeyValues.DefEliteDefenseBuff, new List<Ability>(), GameConfig.Instance.Data.KeyValues.DefEliteMana),
                new Elite("Shadow Assassin", 1, GameConfig.Instance.Data.KeyValues.DefEliteHp, GameConfig.Instance.Data.KeyValues.DefEliteSkill, GameConfig.Instance.Data.KeyValues.DefEliteDefenseBuff, new List<Ability>(), GameConfig.Instance.Data.KeyValues.DefEliteMana),
                
                //Bosses
                new BugBoss("Scary Monster",1, GameConfig.Instance.Data.KeyValues.DefBugBossHp, GameConfig.Instance.Data.KeyValues.DefBugBossSkill, GameConfig.Instance.Data.KeyValues.DefBugBossDefenseBuff, new List<Ability>(), GameConfig.Instance.Data.KeyValues.DefBugBossDmgMult, GameConfig.Instance.Data.KeyValues.DefBugBossArmor, GameConfig.Instance.Data.KeyValues.DefBugBossMana),
                new BugBoss("Arachnid Queen", 1, GameConfig.Instance.Data.KeyValues.DefBugBossHp, GameConfig.Instance.Data.KeyValues.DefBugBossSkill, GameConfig.Instance.Data.KeyValues.DefBugBossDefenseBuff, new List<Ability>(), GameConfig.Instance.Data.KeyValues.DefBugBossDmgMult, GameConfig.Instance.Data.KeyValues.DefBugBossArmor, GameConfig.Instance.Data.KeyValues.DefBugBossMana),
                new BugBoss("Ancient Dragon", 1, GameConfig.Instance.Data.KeyValues.DefBugBossHp, GameConfig.Instance.Data.KeyValues.DefBugBossSkill, GameConfig.Instance.Data.KeyValues.DefBugBossDefenseBuff, new List<Ability>(), GameConfig.Instance.Data.KeyValues.DefBugBossDmgMult, GameConfig.Instance.Data.KeyValues.DefBugBossArmor, GameConfig.Instance.Data.KeyValues.DefBugBossMana),
                new BugBoss("Void Behemoth", 1, GameConfig.Instance.Data.KeyValues.DefBugBossHp, GameConfig.Instance.Data.KeyValues.DefBugBossSkill, GameConfig.Instance.Data.KeyValues.DefBugBossDefenseBuff, new List<Ability>(), GameConfig.Instance.Data.KeyValues.DefBugBossDmgMult, GameConfig.Instance.Data.KeyValues.DefBugBossArmor, GameConfig.Instance.Data.KeyValues.DefBugBossMana),
                new BugBoss("King of the Undead", 1, GameConfig.Instance.Data.KeyValues.DefBugBossHp, GameConfig.Instance.Data.KeyValues.DefBugBossSkill, GameConfig.Instance.Data.KeyValues.DefBugBossDefenseBuff, new List<Ability>(), GameConfig.Instance.Data.KeyValues.DefBugBossDmgMult, GameConfig.Instance.Data.KeyValues.DefBugBossArmor, GameConfig.Instance.Data.KeyValues.DefBugBossMana)
        };

        /// <summary>
        /// Sets Random enemies from the Default catalogue of enemy instantiations.
        /// </summary>
        /// <param name="isBossFight"> true if the battle will be a bossfight</param>
        /// <param name="aproxLevel"> the aproximate level of the heroes to cap new enemies level</param>
        /// <returns>a List with random Enemy instances</returns>
        public static List<Enemy> GetRandomEnemies(bool isBossFight, int aproxLevel)
        {
            var rand = new Random();
            List<Enemy> randomEnemies = new List<Enemy>();
            
            List<Enemy> bosses = Enemies.Where(enemy => enemy is BugBoss).ToList();
            List<Enemy> notBosses = Enemies.Where(enemy => enemy is not BugBoss).ToList();
            
            if (isBossFight) 
            {
                Enemy originalBoss = bosses[Random.Shared.Next(0, bosses.Count)];
                randomEnemies.Add((Enemy)originalBoss.Clone()); 
            }
            int targetTeamSize = isBossFight ? GameConfig.Instance.Data.KeyValues.BossTeamSize : GameConfig.Instance.Data.KeyValues.DefTeamSize;
            while (randomEnemies.Count < targetTeamSize)
            {
                Enemy originalMinion = notBosses[Random.Shared.Next(0, notBosses.Count)];
                randomEnemies.Add((Enemy)originalMinion.Clone());
            }

            foreach (var enemy in randomEnemies)
            {
                enemy.Level += rand.Next(aproxLevel, aproxLevel + GameConfig.Instance.Data.KeyValues.RandomLevelRange);
                if (enemy is IUseAbility notMinion) notMinion.AssignAbilitiesToUser();
            }

            return randomEnemies;
        }
 }
