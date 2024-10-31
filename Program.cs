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
//
//  
// Spells, mortal strike, fireball / cc?, sneack attack?
//
// confuse? random attack
// crit chanse,dodge chanse?

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
      
        Stats playerStats = new Stats(10,10,5);
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
       

        // skapa en lista med fiender
        List<Enemy> enemies = new List<Enemy>();
        enemies.Add(new Mage("Human Cultist"));
        enemies.Add(new Assassin("Shadow Goblin"));
        //enemies.Add(new Mage("Human Cultist"));
       // enemies.Add(new Assassin("Shadow Goblin"));
        enemies.Add(new Warrior("Orc Warrior"));
        enemies.Add(new Shaman("Hobgoblin shaman"));

        // spelloop - spelet körs så länge spelaren har hälsa kvar
        while (player.PlayerHealth > 0)
        {
            // Skriv ut en meny
            Console.WriteLine("========================================");
            Console.WriteLine($"You have {player.PlayerHealth} HP left");
            Console.WriteLine($"You have {player.PlayerMana} Mana Left");
            Console.WriteLine("========================================");
            Console.WriteLine("Existing enemies:");
            List<int> invisibleEnemyIndexes = new List<int>();
            for (int i = 0; i < enemies.Count; i++)
            {
                if (enemies[i].GetInfo() != null) // kontrollerar så att fienden är synlig
                {
                    Console.WriteLine($"{(i + 1)}.  {enemies[i].GetInfo()}"); // skriv ut den synliga fienden
                }
                else // detta är en osynlig fiende (den returnerade "")
                {
                    // Ingen utskrift här...
                    invisibleEnemyIndexes.Add(i); // lägg till indexet i listan med osynliga fiender
                }
            }
            Console.WriteLine("---------------------------");
            Console.WriteLine("Your options:");
            Console.WriteLine("1. Attack");
            Console.WriteLine("2. Heal");
            Console.WriteLine("3. Cast Spell");
            Console.WriteLine("4. Save Game");
            Console.WriteLine("5. End The Game");
            Console.Write("Choose 1-4:");
            string input = Console.ReadLine();
            
            Random random = new Random(); // används för att slumpa skada och häloregenerering
            int enemyIndex = -1;

            switch (input)
            {
                case "1": // Attackera en viss fiende
                    Console.Write("Who do you want to attack:");
                    enemyIndex = int.Parse(Console.ReadLine()) - 1;
                    // läs in vem spelaren vill attackera,
                    // tag värde -1 för att få rätt index i listan
                    // om spelaren valt en osynlig fiende, skriv ut felmeddelande och hoppa ur switchen
                    if (invisibleEnemyIndexes.Contains(enemyIndex))
                    {
                        Console.WriteLine("There was no enemy to attack, you strike air");
                        break;
                    }
                    // ge fienden skada:
                    if (enemyIndex >= 0 && enemyIndex < enemies.Count)
                    {
                        Enemy e = enemies[enemyIndex];
                        player.AttackEnemy(e);
                        if (e.Health <= 0)
                        {
                            Console.WriteLine($"{e.Name} is defeated!");
                            enemies.RemoveAt(enemyIndex);
                        }
                    }
                    else 
                    {
                        Console.WriteLine("Invalid input!");  
                    }
                    break;

                case "2": 
                    player.Heal(50 + random.Next(0,30));
                    break;
                case "3": // Välj en spell att kasta                // något som spökar när du ska kasta spells ++ lägga till så man inte kan attackera osynliga
                    Console.WriteLine("Choose a spell:");
                    Console.WriteLine("1. Fireball");
                    Console.WriteLine("2. Lightning Strike");
                    Console.WriteLine("3. Arcane Blast");
                    Console.WriteLine("4. Poison Cloud");
                    Console.WriteLine("5. Ice Shield");
                    Console.Write("Choose 1-5: ");
                    string spellInput = Console.ReadLine();
                    //int spellEnemyIndex = -1;
                   // enemyIndex = int.Parse(Console.ReadLine()) - 1;
                    if (spellInput == "1" || spellInput == "2" || spellInput == "3") // Kontrollera om det är en single-target spell
                        {
                            Console.Write("Choose an enemy to target with the spell: ");
                            enemyIndex = int.Parse(Console.ReadLine()) - 1;
                        }

                    switch (spellInput)
                    {
                        case "1":
                            double fireballDamage = spells.Fireball(player);
                            if (fireballDamage > 0)
                            {
                               // Console.Write("Choose an enemy to hit with Fireball: ");
                                //int spellenemyIndex = int.Parse(Console.ReadLine()) - 1;
                                if (enemyIndex >= 0 && enemyIndex < enemies.Count)
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
                            }
                            break;

                        case "2":
                            double lightningDamage = spells.LightningStrike(player);
                            if (lightningDamage > 0)
                            {
                                Console.Write("Choose an enemy to hit with Lightning Strike: ");
                                //int spellenemyIndex = int.Parse(Console.ReadLine()) - 1;
                                if (enemyIndex >= 0 && enemyIndex < enemies.Count)
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
                            }
                            break;

                        case "3":
                            double arcaneDamage = spells.ArcaneBlast(player);
                            if (arcaneDamage > 0)
                            {
                                Console.Write("Choose an enemy to hit with Arcane Blast: ");
                                //int enemyIndex = int.Parse(Console.ReadLine()) - 1;
                                if (enemyIndex >= 0 && enemyIndex < enemies.Count)
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
                            }
                            break;
                    

                        case "4":
                            spells.PoisonCloud(enemies, player); // Skadar alla fiender i listan
                            break;

                        case "5":
                            spells.IceShield(player); // Ger spelaren en sköld
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

            // ---------------------------------------------
            // Gå igenom alla fiender, en efter en och:
            //
            // OM:      en fiende har 0 eller mindre hälsa, skriv att den är död
            //          och ta bort den ur listan
            // 
            // ANNARS   låt fienden attackera spelaren 
            // ---------------------------------------------
            for (int i = 0; i < enemies.Count; i++)
            {
                // om fienden har 0 eller mindre hälsa, döda den
                if (enemies[i].Health <= 0)
                {
                    Console.WriteLine("------------------------------------");
                    Console.WriteLine($"***  {enemies[i].Name} died  ***");
                    Console.WriteLine("------------------------------------");
                    // ta bort fienden från listan utifrån dess index
                    enemies.RemoveAt(i);

                    // i och med att denna fiende tas bort från listan så kommer 
                    // alla andra fiender att flyttas ett steg uppåt i listan
                    // Därför måste vi minska i med 1 för att inte hoppa över en fiende
                    i--;
                    // Vi hoppar över resten av loopen och går till nästa iteration.
                    continue;
                }
                else if (enemies[i] is Shaman shaman)
                {
                    shaman.Heal(enemies);
                }
                else
                {
                    // om vi inte kör continue ovan, riskerar vi att hamna out of bounds
                    // , på sista elementet

                    // Fienden gör sin attack på spelaren:
                    //playerHealth -= enemies[i].Attack();
                    player.TakeDamage(enemies[i].Attack());
                }
            }

            // ---------------------------------------------
            // Kontrollera om spelaren har dött, isf avsluta mainloopen
            if (player.PlayerHealth <= 0)
            {
                Console.WriteLine("-------------------------------");
                Console.WriteLine("***** You died try again *****");
                Console.WriteLine("-------------------------------");

                break;
            }

            else if (enemies.Count <= 0 && invisibleEnemyIndexes.Count <= 0)
            {
                Console.WriteLine("--------You have cleared the Tutorial!--------");
            }
            // Vänta på att användaren ska trycka på en tangent och rensa skärmen
            Console.ReadKey();
            Console.Clear();
        }
        Console.WriteLine("*****  GAME OVER LOSER  *****");
        Console.ReadKey();
    }
}
