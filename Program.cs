using System.Collections.Generic;
using System;
using TutorialTheGame.Enemies;
using TutorialTheGame.GameHandler;
using TutorialTheGame.PlayerChar;
static class Program 
{
    static void Main(string[] args)
    {
        Ui.AskForName();
        string name = Console.ReadLine();       
        Player player = new Player(name);
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
        // Intro story för spelet.
        Ui.IntroStory(player);
        Ui.PressKeyContinue();
        Ui.FirstFloorStory();
        Ui.PressKeyContinue(); 
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
                        if (floorHandler.CurrentFloor == 3) { Ui.MidLevelStory(); }
                        else if (floorHandler.CurrentFloor == 6) { Ui.HighLevelStory(); }
                        floorHandler.AdvanceFloor();
                        if (floorHandler.CurrentFloor == 10) {Ui.BossLevelStory(); }
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
                Ui.PressKeyContinue();
            }
        }
    }
}
