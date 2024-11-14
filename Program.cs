using System.Collections.Generic;
using System;
using TutorialTheGame;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Security.Cryptography.X509Certificates;
using System.Transactions;
// -------TO DO LIST---------
// ** Får sätta ut 10 stat points i början på sin karaktär (stamina, mana, strength), för om man vill vara warrior, mage eller assasin
// Justera Stat/skada samt enemy hp
// Total damage display
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
        //CharacterSpells spells = new CharacterSpells();
        //GameSaver gameSaver = new GameSaver();
        //Player player = gameSaver.LoadOrMakePlayer();
        FloorHandler floorHandler = new FloorHandler();
        PlayerActionHandler action = new PlayerActionHandler();
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
                Ui.DisplayInfo(player, floorHandler);  // visar hp, mana, floor och xp
                Ui.DisplayEnemies(enemies);
                Ui.MenuOptions(); // Val mellan 1-6 för attack, heal, spell, save, quit, inventory

                action.PlayerAction(player, enemies);
                action.EnemiesTurn(player, enemies);

                    if (enemies.Count == 0)
                    {
                        if (floorHandler.CurrentFloor < 10)
                        {
                            floorHandler.AdvanceFloor();
                        }
                        else
                        {
                            Ui.GameClearMessage();
                            Console.ReadKey();
                            gameCompleted = true;
                        }
                    }

                // Kontrollera om spelaren har dött, isf avsluta mainloopen
                if (player.PlayerHealth <= 0)
                {   
                    Ui.DeathMessage();
                    Console.ReadKey(); 
                    break;
                }
                // Vänta på att användaren ska trycka på en tangent och rensa skärmen
                //Finish:;
                //Console.ReadKey();
                Console.Clear();
            }
        }
    }
}
