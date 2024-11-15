using System;
using System.Collections.Generic;
namespace TutorialTheGame
{
    public static class Ui
    {
        //Enemy enemy = new Enemy();
        public static void MenuOptions()
        {
            Console.WriteLine("Your options:");
            Console.WriteLine("1. Attack");
            Console.WriteLine("2. Heal");
            Console.WriteLine("3. Cast Spell");
            Console.WriteLine("4. Save Game");
            Console.WriteLine("5. End The Game");
            Console.WriteLine("6. Show Inventory");
            Console.Write("Choose 1-6:");
           // string input = Console.ReadLine();
        }
        public static void SpellOptions()
        {
            Console.WriteLine("Choose a spell:");
            Console.WriteLine("1. Fireball");
            Console.WriteLine("2. Lightning Strike");
            Console.WriteLine("3. Arcane Blast");
            Console.WriteLine("4. Poison Cloud");
            Console.WriteLine("5. Ice Shield");
            Console.Write("Choose 1-5: ");
        }
        public static void DisplayInfo(Player player, FloorHandler floorHandler)
        {
            BigLine();
            Console.WriteLine($"You have {player.PlayerHealth} HP left");
            Console.WriteLine($"You have {player.PlayerMana} Mana Left");
            Console.WriteLine($"You are on floor {floorHandler.CurrentFloor}");
            Console.WriteLine($"You have {player.Experience} / {player.GetExperienceForNextLevel()}");   // sätta in en character display kanske istället? eller visa staten med inventoryt?
            BigLine();
        }
        public static void DisplayEnemies(List<Enemy> enemies)
        {
            Console.WriteLine("Existing enemies:");
            for (int i = 0; i < enemies.Count; i++)
            {
                if (enemies[i].GetInfo() != null) // kontrollerar så att fienden är synlig
                {
                    Console.WriteLine($"{(i + 1)}.  {enemies[i].GetInfo()}"); // skriv ut den synliga fienden
                }
            }
        }
        public static void ChooseTarget()
        {
            Console.Write("Who do you want to attack:");
        }
        public static void SmallLine()
        {
            Console.WriteLine("----------------------------------------");
        }
        public static void BigLine()
        {
            Console.WriteLine("========================================");
        }
        public static void DeathMessage()
        {
            SmallLine();
            Console.WriteLine("***** You died try again *****");
            SmallLine();
        }
        public static void GameClearMessage()
        {
            Console.WriteLine("--------You have cleared the Tutorial!--------");
        }
        public static void EnemyDiedMessage(Enemy enemy)
        {
            SmallLine();
            Console.WriteLine($"***  {enemy.Name} died  ***");
            SmallLine();
        }
        public static void InvalidInput()
        {
            Console.WriteLine("Invalid input, please try again");
        }
        public static void InvalidTarget()
        {
            Console.WriteLine("There was no enemy to attack, you strike air");
        }
        public static void NoMana(string enemyName)
        {
            Console.WriteLine($"{enemyName} has no mana for casting");
            SmallLine();
        }
    }

}

/* foreach (var enemy in enemies)  // TEST FÖR ATT SE ATT VAPEN FUNGERAR 
        {
            if (enemy.loot == null)
            {
                Console.WriteLine($"Warning: {enemy.Name} does not have a loot instance.");
                continue;
            }

            Weapon testLoot = enemy.DropLoot();
            if (testLoot == null)
            {
                Console.WriteLine($"Warning: {enemy.Name} could not drop loot (loot list might be empty).");
            }
            else
            {
                Console.WriteLine($"Test: {enemy.Name} can drop {testLoot.Name} with {testLoot.Damage} damage.");
            }
        }
        Loot lootTest = new Loot();
        Weapon testWeapon = lootTest.GenerateLoot();

        if (testWeapon != null)
        {
            Console.WriteLine($"Test loot: {testWeapon.Name} with {testWeapon.Damage} damage and rarity {testWeapon.Rarity}.");
        }
        else
        {
            Console.WriteLine("Test loot failed to generate a weapon.");
        } */ 