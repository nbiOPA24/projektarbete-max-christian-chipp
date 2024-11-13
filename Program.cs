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
static class Program //skriva metoder på menyn istället så det inte blir stökigt, samma med ui
{
    static void Main(string[] args)
    {
      
        Stats playerStats = new Stats(10,100,5);
        Player player = new Player("BitchAss", playerStats);
        CharacterSpells spells = new CharacterSpells();
        FloorHandler floorHandler = new FloorHandler();
        Ui ui = new Ui();
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
                
        // spelloop - spelet körs så länge spelaren har hälsa kvar och inte har klarat spelet
        while (player.PlayerHealth > 0 && !gameCompleted) 
        {
            List<Enemy> enemies = floorHandler.CreateEnemies(); // --- sätt en ordentlig kommentar när jag inte är trött
            // Skriv ut en meny
            while (enemies.Count > 0 && player.PlayerHealth > 0)  // spelloop, körs så länge det finns enemies och man har hp kvar
            {
                ui.DisplayInfo(player, floorHandler);  // visar hp, mana, floor och xp
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
                // Val mellan 1-6 för attack, heal, spell, save, quit, inventory
                ui.MenuOptions();
                string input = Console.ReadLine();
                
                Random random = new Random(); // används för att slumpa skada och häloregenerering kanske lägga den utanför loopen, men vet inte om det funkar utan konstruktor
                int enemyIndex = -1;
                Enemy e = null;

                switch (input)
                {
                    case "1": // Attackera en viss fiende
                        ui.ChooseTarget(); // fråga vilken fiende man vill attackera
                        enemyIndex = int.Parse(Console.ReadLine()) - 1;
                        // läs in vem spelaren vill attackera,
                        // tag värde -1 för att få rätt index i listan
                        // om spelaren valt en osynlig fiende, skriv ut felmeddelande och hoppa ur switchen
                        if (invisibleEnemyIndexes.Contains(enemyIndex))
                        {
                            ui.InvalidTarget();  // säger till om man försöker attackera den osynliga
                            break;
                        }
                        e = enemies[enemyIndex];  // måste ligga efter if satsen för att kontrollera om det är osynligt eller ej
                        // ge fienden skada:
                        player.AttackEnemy(e);
                        break;

                    case "2": 
                        player.Heal(50 + random.Next(0,30));
                        break;

                    case "3": // Väljer en spell att kasta 1-5, fire, lightning, arcane, poison, shield
                        ui.SpellOptions();              
                        string spellInput = Console.ReadLine();
                        if (spellInput == "1" || spellInput == "2" || spellInput == "3") // Kontrollera om det är en single-target spell   generelisera spellsen så man kan lägga till mer,  lista på current spells // kompisiton
                            {
                                ui.ChooseTarget();
                                enemyIndex = int.Parse(Console.ReadLine()) - 1;
                                if (enemyIndex < 0 || enemyIndex >= enemies.Count || invisibleEnemyIndexes.Contains(enemyIndex))
                                {
                                    ui.InvalidTarget();
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
                                ui.InvalidInput();
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
                        ui.InvalidInput();
                        break;
                }
                // Gå igenom alla fiender, om fienden har <0 hp skriv att den är död annars attackera spelaren.
                for (int i = 0; i < enemies.Count; i++)
                {
                    // om fienden har 0 eller mindre hälsa, döda den
                    if (enemies[i].Health <= 0)
                    {
                        ui.EnemyDiedMessage(enemies[i]); // visar ett meddelande om vilken fiende som dör
                        // ta bort fienden från listan utifrån dess index
                        //Console.WriteLine($"Attempting to drop loot from {enemies[i].Name}..."); // bara för att se att det funkar, tas bort sen

                        player.AddExperience(enemies[i].ExperienceValue); // ny metod för att lägga på xp, ska eventuellt flyttas men första test
                        Weapon droppedLoot = enemies[i].DropLoot();
                        if (droppedLoot != null)
                        {
                            player.PickUpLoot(droppedLoot);
                        
                        }
                        else 
                        {
                            Console.WriteLine("No loot");  // ska nog tas bort sen när loot systemet är klart.
                        }

                        enemies.RemoveAt(i);

                        // i och med att denna fiende tas bort från listan så kommer alla andra fiender att flyttas ett steg uppåt i listan, minskar i med -1 då.
                        i--;
                    }
                    else if (enemies[i] is Shaman shaman)// && enemies.Count > 1)
                    {
                        shaman.Heal(enemies);
                    }
                    /*else if (enemies[i] is Boss)  tror inte denna behövs faktiskt.
                    {
                        player.TakeDamage(enemies[i].Attack());
                    } */
                    else
                    {
                        // om vi inte kör continue ovan, riskerar vi att hamna out of bounds
                        // , på sista elementet men tror inte den behövs endå?
                        player.TakeDamage(enemies[i].Attack());
                    }
                }
                    if (enemies.Count == 0)
                    {
                        if (floorHandler.CurrentFloor < 10)
                        {
                            floorHandler.AdvanceFloor();
                        }
                        else
                        {
                            ui.GameClearMessage();
                            gameCompleted = true;
                        }
                    }

                // Kontrollera om spelaren har dött, isf avsluta mainloopen
                if (player.PlayerHealth <= 0)
                {   
                    ui.DeathMessage();
                    Console.ReadKey(); 
                    break;
                }
                // Vänta på att användaren ska trycka på en tangent och rensa skärmen
                Console.ReadKey();
                Console.Clear();
            }
        }
    }
}
