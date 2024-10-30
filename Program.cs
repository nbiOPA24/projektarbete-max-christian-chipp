using System.Collections.Generic;
using System;
using TutorialTheGame;
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
       /* double playerHealth = 100; // sätt spelarens starthälsa
        double playerDamage = 20;
        double playerMana = 0; */
        Stats playerStats = new Stats(10,10,5);
        Player player = new Player("BitchAss", playerStats);
        //Player player = new Player("");
        //playerHealth = playerStats.CalculateStamina(playerStats.Stamina, playerHealth);
        //playerMana = playerStats.CalculateIntelligence(playerStats.Intelligence, playerMana);
       

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
            Console.WriteLine("3. End The Game");
            Console.Write("Choose 1-3:");
            string input = Console.ReadLine();
            
            Random random = new Random(); // används för att slumpa skada och häloregenerering
            switch (input)
            {
                case "1": // Attackera en viss fiende
                    Console.Write("Who do you want to attack:");
                    // läs in vem spelaren vill attackera,
                    // tag värde -1 för att få rätt index i listan
                    int enemyIndex = int.Parse(Console.ReadLine()) - 1;
                    // om spelaren valt en osynlig fiende, skriv ut felmeddelande och hoppa ur switchen
                    if (invisibleEnemyIndexes.Contains(enemyIndex))
                    {
                        Console.WriteLine("There was no enemy to attack, you strike air");
                        break;
                    }
                    // ge fienden skada:
                    //double damage = Stats.IncreaseStats();
                   // -- double damage = playerStats.CalculateStrength(playerStats.Strength, playerDamage) + random.Next(0,10); // Använder strength

                    //double damage = playerDamage; //+ random.Next(0, 30);
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
                    
                    /*Console.WriteLine("========================================");
                    System.Console.WriteLine($"You attack {e.Name} for {damage} damage");
                    Console.WriteLine("========================================");

                    e.Defend(damage); */
                    break;

                case "2": // Heala sig själv
                    /*int heal = 50 + random.Next(0, 30);
                    Console.WriteLine("========================================");
                    Console.WriteLine($"You healed yourself for {heal}");
                    Console.WriteLine("========================================");
                    playerHealth += heal; */
                    player.Heal(50 + random.Next(0,30));
                    break;

                case "3": // Avsluta spelet
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
