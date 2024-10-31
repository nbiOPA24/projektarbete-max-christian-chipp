using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace TutorialTheGame
{
    public class GameData
    {
        public string PlayerName { get; set; }
        public double PlayerHealth { get; set; }
        public double PlayerMana { get; set; }
        public double PlayerDamage { get; set; }
        public Stats PlayerStats { get; set; }
    }

    public static class GameDataManager
    {
        public static void SaveGame(Player player, string filePath)
        {
            GameData data = new GameData
            {
                PlayerName = player.Name,
                PlayerHealth = player.PlayerHealth,
                PlayerMana = player.PlayerMana,
                PlayerDamage = player.PlayerDamage,
                PlayerStats = player.PlayerStats
            };

            string jsonString = JsonSerializer.Serialize(data);
            File.WriteAllText(filePath, jsonString);
            Console.WriteLine("Game saved successfully!");
        }

        public static Player LoadGame(string filePath)
        {
            if (!File.Exists(filePath))
            {
                Console.WriteLine("Save file not found.");
                return null;
            }

            string jsonString = File.ReadAllText(filePath);
            GameData data = JsonSerializer.Deserialize<GameData>(jsonString);

            if (data != null)
            {
                Stats playerStats = data.PlayerStats;
                Player player = new Player(data.PlayerName, playerStats)
                {
                    PlayerHealth = data.PlayerHealth,
                    PlayerMana = data.PlayerMana,
                    PlayerDamage = data.PlayerDamage
                };
                Console.WriteLine("Game loaded successfully!");
                return player;
            }

            return null;
        }
    }
}