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
static class Program //skriva metoder på menyn istället så det inte blir stökigt, samma med ui  // lägga till xp på xp display // ändra så shaman inte healer död // fixa turn så du kan gå igenom inventory utan att bli attakerad
{
    static void Main(string[] args)
    {
        //instanserar lite klasser och skapar en spelare
        //Stats playerStats = new Stats(10,100,50);
        Ui.AskForName();
        string name = Console.ReadLine();       
        Player player = new Player(name); //("BitchAss");//, playerStats);
        //CharacterSpells spells = new CharacterSpells();
        //GameSaver gameSaver = new GameSaver();
        //Player player = gameSaver.LoadOrMakePlayer();
        FloorHandler floorHandler = new FloorHandler();
        PlayerActionHandler action = new PlayerActionHandler();
        bool gameCompleted = false;

        Console.WriteLine("Do you want to load a saved game? (y/n)");  // logik för att ladda ett sparat spel
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
        /* Ui.IntroStory(player);
        Ui.PressKeyContinue();
        Ui.FirstFloorStory();
        Ui.PressKeyContinue(); */
        // spelloop - spelet körs så länge spelaren har hälsa kvar och inte har klarat spelet
        while (player.PlayerHealth > 0 && !gameCompleted) 
        {
            List<Enemy> enemies = floorHandler.CreateEnemies(); // --- sätt en ordentlig kommentar när jag inte är trött
            
            while (enemies.Count > 0 && player.PlayerHealth > 0)  // spelloop, körs så länge det finns enemies och man har hp kvar
            {
                Ui.DisplayInfo(player, floorHandler);  // visar hp, mana, floor och xp
                Ui.DisplayEnemies(enemies); // visar alla enemies
                Ui.MenuOptions(); // Val mellan 1-6 för attack, heal, spell, save, quit, inventory

                action.PlayerAction(player, enemies); // man gör sin tur, attack/heal/spell
                action.EnemiesAction(player, enemies); // enemeies attackerar

                if (enemies.Count == 0) // ser till att du går upp i nivå / klarar spelet
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
                {   Console.WriteLine(player.PlayerHealth); //tilffäligt
                    Ui.DeathMessage();
                    Console.ReadKey(); 
                    break;
                }
                // Vänta på att användaren ska trycka på en tangent och rensa skärmen
                //Finish:;
                Console.ReadKey();
               // Console.Clear(); test tillfälligt för att hitta fel
            }
        }
    }
}
