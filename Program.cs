using System.Collections.Generic;
using System;
using TutorialTheGame;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Security.Cryptography.X509Certificates;
using System.Transactions;
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


// Frame symbols saved for later (possible) use:
// ╔══╦══╗  ╔═════╗  ╔══╤══╗
// ║  ║  ║  ╠═════╣  ║  |  ║
// ╚══╩══╝  ╚═════╝  ╚══╧══╝
// ╔╗
// ║║           läggat ill threadsleep på enemy attacker
// ╚╝
static class Program
{
    static void Main(string[] args)
    {
      
        Stats playerStats = new Stats(10,100,5);
        Player player = new Player("BitchAss", playerStats);
        CharacterSpells spells = new CharacterSpells();
        FloorHandler floorHandler = new FloorHandler();
        bool gameCompleted = false;

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

        // spelloop - spelet körs så länge spelaren har hälsa kvar
        while (player.PlayerHealth > 0 && !gameCompleted)
        {
            List<Enemy> enemies = floorHandler.CreateEnemies();
            // Skriv ut en meny
            while (enemies.Count > 0 && player.PlayerHealth > 0)
            {
            Console.WriteLine("========================================");
            Console.WriteLine($"You have {player.PlayerHealth} HP left");
            Console.WriteLine($"You have {player.PlayerMana} Mana Left");
            Console.WriteLine($"You are on floor {floorHandler.CurrentFloor}");
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
            Console.WriteLine("6. Show Inventory");
            Console.Write("Choose 1-6:");
            string input = Console.ReadLine();
            
            Random random = new Random(); // används för att slumpa skada och häloregenerering
            int enemyIndex = -1;
            Enemy e = null;

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
                    e = enemies[enemyIndex];  // måste ligga efter if satsen för att kontrollera om det är osynligt eller ej
                    // ge fienden skada:
                    player.AttackEnemy(e);
                    break;

                case "2": 
                    player.Heal(50 + random.Next(0,30));
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
                            enemyIndex = int.Parse(Console.ReadLine()) - 1;
                            if (enemyIndex < 0 || enemyIndex >= enemies.Count || invisibleEnemyIndexes.Contains(enemyIndex))
                            {
                                Console.WriteLine("Your spell goes in the air and dissapears.");
                                break;
                            }
                            e = enemies[enemyIndex]; // måste ligga efter if satsen så den hinner kontrollera om den är osynlig eller ej
                        }

                    switch (spellInput)
                    {
                        case "1":
                            double fireballDamage = spells.Fireball(player);
                            if (fireballDamage > 0 && e != null)
                            {
                                e.Defend(fireballDamage);
                            }
                            break;

                        case "2":
                            double lightningDamage = spells.LightningStrike(player);
                            if (lightningDamage > 0 && e != null)
                            {
                                e.Defend(lightningDamage);
                            }
                            break;

                        case "3":
                            double arcaneDamage = spells.ArcaneBlast(player);
                            if (arcaneDamage > 0 && e != null)
                            {
                                e.Defend(arcaneDamage);
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
                
                case "6":
                    player.ShowInventory();
                    break;

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
                    Console.WriteLine($"Attempting to drop loot from {enemies[i].Name}..."); // bara för att se att det funkar, tas bort sen


                    Weapon droppedLoot = enemies[i].DropLoot();
                    if (droppedLoot != null)
                    {
                        player.PickUpLoot(droppedLoot);
                     
                    }
                    else 
                    {
                        Console.WriteLine("No loot");
                    }

                    enemies.RemoveAt(i);

                    // i och med att denna fiende tas bort från listan så kommer 
                    // alla andra fiender att flyttas ett steg uppåt i listan
                    // Därför måste vi minska i med 1 för att inte hoppa över en fiende
                    i--;
                    // Vi hoppar över resten av loopen och går till nästa iteration.
                   // continue; testar att ta bort den?
                }
                else if (enemies[i] is Shaman shaman)// && enemies.Count > 1)
                {
                    shaman.Heal(enemies);
                }
                else if (enemies[i] is Boss)
                {
                    player.TakeDamage(enemies[i].Attack());
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
                if (enemies.Count == 0)
                {
                    if (floorHandler.CurrentFloor < 10)
                    {
                    Console.WriteLine("All enemies are defeated, Moving on to the next floor....");
                    floorHandler.AdvanceFloor();
                    }
                    else
                    {
                        Console.WriteLine("--------You have cleared the Tutorial!--------");
                        gameCompleted = true;
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

           /* else if (enemies.Count <= 0 && floorHandler.CurrentFloor == 10)
            {
                Console.WriteLine("--------You have cleared the Tutorial!--------");
            } */
            // Vänta på att användaren ska trycka på en tangent och rensa skärmen
            Console.ReadKey();
            Console.Clear();
        }
        if (player.PlayerHealth <= 0)
        {
        Console.WriteLine("*****  GAME OVER LOSER  *****");
        Console.ReadKey();
        }
    }
    }
}
