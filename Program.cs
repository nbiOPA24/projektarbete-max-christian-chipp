using System.Collections.Generic;
using System;
using TutorialTheGame;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Security.Cryptography.X509Certificates;

// -------TO DO LIST---------
//--------Welcome to the Tutorial---------
// 10 levels
// Easy, intermediate, hardcore
// exp från att döda och gå upp i levels
// ** Får sätta ut 10 stat points i början på sin karaktär (stamina, mana, strength), för om man vill vara warrior, mage eller assasin
// stats som man kan öka på sin karaktär
// utrustning, vapen, drops?
// index som uppdateras när fiender dör/försvinner
// 
// Spells, mortal strike, fireball / cc?, sneack attack?
// confuse? random attack
// crit chanse, dodge chanse?
// add final boss Trangius
// Fixes :
// lägga in hur mycket man healer sig själv för
// Justera Stat/skada samt enemy hp
// ha random enemies istället för samma 
// Total damage display
// Race/name/Weapon väljs som start
// En ide, lägga till raser, raser har olika allokerade stats som bas, när sedan dingar i spelet så får man statPoints att lägga ut.

static class Program
{
    static void Main(string[] args)
    {
        int currentLevel = 1; // Start level 1
        Stats playerStats = new Stats(10, 10, 5); // Initiera spelaren med startstats
        Player player = new Player("BitchAss", playerStats);
        CharacterSpells spells = new CharacterSpells();

        Console.WriteLine("Do you want to load a saved game? (y/n)");
        string loadInput = Console.ReadLine().ToLower();

        if (loadInput == "y")
        {
            Player loadedPlayer = GameDataManager.LoadGame("savegame.json");
            if (loadedPlayer != null)
            {
                player = loadedPlayer;
                Console.WriteLine("Game loaded!");
            }
            else
            {
                Console.WriteLine("Failed to load game. Starting a new game.");
            }
        }

        while (player.PlayerHealth > 0 && currentLevel <= 10)
        {
            // Skriv ut aktuell nivå, spelarens HP och Mana i början av nivån
            Console.WriteLine("========================================");
            Console.WriteLine($"You are currently on Level {currentLevel}");
            Console.WriteLine($"You have {player.PlayerHealth} HP left");
            Console.WriteLine($"You have {player.PlayerMana} Mana Left");
            Console.WriteLine("========================================");

            // Generera fiender för nivån
            List<Enemy> enemies = GenerateEnemies(currentLevel);

            // Spelloop - spelet körs så länge spelaren har hälsa kvar och fiender finns
            while (player.PlayerHealth > 0 && enemies.Count > 0)
            {
                Console.WriteLine("Existing enemies:");
                List<int> invisibleEnemyIndexes = new List<int>();
                for (int i = 0; i < enemies.Count; i++)
                {
                    if (enemies[i].GetInfo() != null) // kontrollerar så att fienden är synlig
                    {
                        Console.WriteLine($"{(i + 1)}. {enemies[i].Name} have {enemies[i].Health} HP"); // skriv ut den synliga fienden
                    }
                    else // detta är en osynlig fiende (den returnerade "")
                    {
                        invisibleEnemyIndexes.Add(i); // lägg till indexet i listan med osynliga fiender
                    }
                }

                // Val för spelaren
                Console.WriteLine("---------------------------");
                Console.WriteLine("Your options:");
                Console.WriteLine("1. Attack");
                Console.WriteLine("2. Heal");
                Console.WriteLine("3. Cast Spell");
                Console.WriteLine("4. Save Game");
                Console.WriteLine("5. End The Game");
                Console.Write("Choose 1-5:");
                string input = Console.ReadLine();

                Random random = new Random(); // används för att slumpa skada och häloregenerering
                int enemyIndex = -1;

                switch (input)
                {
                    case "1": // Attackera en viss fiende
                        Console.Write("Who do you want to attack:");
                        if (!int.TryParse(Console.ReadLine(), out enemyIndex) || enemyIndex < 1 || enemyIndex > enemies.Count)
                        {
                            Console.WriteLine("Invalid target!");
                            break;
                        }
                        enemyIndex--; // Anpassa för nollindexering

                        if (invisibleEnemyIndexes.Contains(enemyIndex))
                        {
                            Console.WriteLine("There was no enemy to attack, you strike air");
                            break;
                        }

                        Enemy targetEnemy = enemies[enemyIndex];
                        player.AttackEnemy(targetEnemy);
                        if (targetEnemy.Health <= 0)
                        {
                            Console.WriteLine($"{targetEnemy.Name} is defeated!");
                            enemies.RemoveAt(enemyIndex);
                        }
                        break;

                    case "2":
                        player.Heal(50 + random.Next(0, 30));
                        Console.WriteLine($"You healed yourself. Current HP: {player.PlayerHealth}");
                        break;

                    case "3": // Välj en spell att kasta
                        Console.WriteLine("Choose a spell:");
                        Console.WriteLine("1. Fireball");
                        Console.WriteLine("2. Lightning Strike");
                        Console.WriteLine("3. Arcane Blast");
                        Console.WriteLine("4. Poison Cloud");
                        Console.WriteLine("5. Ice Shield");
                        Console.Write("Choose 1-5: ");
                        string spellInput = Console.ReadLine();

                        if (spellInput == "1" || spellInput == "2" || spellInput == "3") // Kontrollera om det är en single-target spell
                        {
                            Console.Write("Choose an enemy to target with the spell: ");
                            if (!int.TryParse(Console.ReadLine(), out enemyIndex) || enemyIndex < 1 || enemyIndex > enemies.Count)
                            {
                                Console.WriteLine("Invalid target!");
                                break;
                            }
                            enemyIndex--; // Anpassa för nollindexering
                        }

                        switch (spellInput)
                        {
                            case "1":
                                double fireballDamage = spells.Fireball(player);
                                if (fireballDamage > 0 && enemyIndex >= 0 && enemyIndex < enemies.Count)
                                {
                                    Enemy e = enemies[enemyIndex];
                                    e.Health -= fireballDamage;
                                    Console.WriteLine($"Dealt {fireballDamage} Fireball damage to {e.Name}");
                                    if (e.Health <= 0)
                                    {
                                        Console.WriteLine($"{e.Name} is defeated!");
                                        enemies.RemoveAt(enemyIndex);
                                    }
                                }
                                break;

                            case "2":
                                double lightningDamage = spells.LightningStrike(player);
                                if (lightningDamage > 0 && enemyIndex >= 0 && enemyIndex < enemies.Count)
                                {
                                    Enemy e = enemies[enemyIndex];
                                    e.Health -= lightningDamage;
                                    Console.WriteLine($"Dealt {lightningDamage} Lightning damage to {e.Name}");
                                    if (e.Health <= 0)
                                    {
                                        Console.WriteLine($"{e.Name} is defeated!");
                                        enemies.RemoveAt(enemyIndex);
                                    }
                                }
                                break;

                            case "3":
                                double arcaneDamage = spells.ArcaneBlast(player);
                                if (arcaneDamage > 0 && enemyIndex >= 0 && enemyIndex < enemies.Count)
                                {
                                    Enemy e = enemies[enemyIndex];
                                    e.Health -= arcaneDamage;
                                    Console.WriteLine($"Dealt {arcaneDamage} Arcane Blast damage to {e.Name}");
                                    if (e.Health <= 0)
                                    {
                                        Console.WriteLine($"{e.Name} is defeated!");
                                        enemies.RemoveAt(enemyIndex);
                                    }
                                }
                                break;

                            case "4":
                                spells.PoisonCloud(enemies, player);
                                Console.WriteLine("Poison Cloud cast, damaging all enemies.");
                                break;

                            case "5":
                                spells.IceShield(player);
                                Console.WriteLine("Ice Shield cast, providing protection.");
                                break;

                            default:
                                Console.WriteLine("Invalid spell selection");
                                break;
                        }
                        break;

                    case "4":
                        GameDataManager.SaveGame(player, "savegame.json");
                        Console.WriteLine("Game saved!");
                        break;

                    case "5": // Avsluta spelet
                        Console.WriteLine("End the game");
                        return;

                    default:
                        Console.WriteLine("Wrong input");
                        break;
                }

                // Fiendernas attack och uppdatering
                for (int i = 0; i < enemies.Count; i++)
                {
                    if (enemies[i].Health <= 0)
                    {
                        Console.WriteLine($"***  {enemies[i].Name} died  ***");
                        enemies.RemoveAt(i);
                        i--;
                    }
                    else if (enemies[i] is Shaman shaman)
                    {
                        shaman.Heal(enemies);
                    }
                    else
                    {
                        player.TakeDamage(enemies[i].Attack());
                        Console.WriteLine($"{enemies[i].Name} attacks you. Your HP: {player.PlayerHealth}");
                    }
                }

                // Kontrollera om spelaren har dött
                if (player.PlayerHealth <= 0)
                {
                    Console.WriteLine("***** You died, try again *****");
                    break;
                }

                // Nästa nivå om alla fiender är besegrade
                if (enemies.Count == 0)
                {
                    Console.WriteLine($"--------You have cleared Level {currentLevel}!--------");
                    currentLevel++;
                    if (currentLevel <= 10)
                    {
                        Console.WriteLine($"Starting Level {currentLevel}...");
                    }
                }

                Console.ReadKey();
                Console.Clear();
            }
        }
        Console.WriteLine("***** GAME OVER *****");
        Console.ReadKey();
    }

    // Placeholder för GenerateEnemies-metoden
    static List<Enemy> GenerateEnemies(int level)
    {
        // Placeholder - generera en lista med fiender
        List<Enemy> enemies = new List<Enemy>
        {
            new Mage("Mage"),
            new Assassin("Assassin"),
            new Warrior("Warrior"),
            new Shaman("Shaman")
        };
        return enemies;
    }
}
