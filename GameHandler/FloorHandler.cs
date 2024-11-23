using System;
using System.Collections.Generic;
using TutorialTheGame.Enemies;

namespace TutorialTheGame.GameHandler
{
    public class FloorHandler
    {
        public int CurrentFloor {get; set;} // = 1;
        private Random random = new Random();

        public FloorHandler()
        {
            CurrentFloor = 1;
        }
        public void AdvanceFloor()
        {
            CurrentFloor++;
            Console.WriteLine($"You've defeated all your enemies and embark in the tower to {CurrentFloor} floor!");
        }
        // skapar en lista med enemies beroende på vilken floor man är på, om det är 10 så skapas en boss istället
        public List<Enemy> CreateEnemies()
        {
            List<Enemy> enemies = new List<Enemy>();
            if (CurrentFloor == 10)
            {
                enemies.Add(new Boss("Trangius"));
            }
            else
            {
                int numberOfEnemies = random.Next(1,2 + CurrentFloor); //slumpar hur många fiender baserat på floor
                for (int i = 0; i < numberOfEnemies; i++)
                {
                    Enemy enemy = CreateEnemyForFloor();
                    enemies.Add(enemy);
                }
            }
            return enemies;
        }
        public Enemy CreateLowLevelEnemy()  // _ är default
        {
            return random.Next(2) switch
            {
                0 => new Mage("Apprentice Mage", 20),
                _ => new Assassin("Novice Assasin", 20)
            };
        }
        public Enemy CreateMidLevelEnemy()
        {
            return random.Next(3) switch
            {
                1 => new Mage("Occultist Mage", 30),
                2 => new Warrior("Orc Warrior", 30),
                _ => new Assassin("Shadow Assasin", 30)
            };
        }
        public Enemy CreateHighLevelEnemy() 
        {
            return random.Next(4) switch
            {
                1 => new Mage("Witch of Fire", 40),
                2 => new Warrior("Elite Warrior", 40),
                3 => new Assassin("Ninja of West", 40),
                _ => new Shaman("Shaman of the Forest", 40)
            };
        }
        // om det är 10, skapa boss, om det är 1-3 skapa low, 4-6 mid, 7-9 highlevel, måste ha <= => på båda sidorna.
        public Enemy CreateEnemyForFloor()  
        {
            Enemy enemy = CurrentFloor switch
            {
                10 => new Boss("Trangius"),
                <= 3 => CreateLowLevelEnemy(),
                <= 6 => CreateMidLevelEnemy(),
                _ => CreateHighLevelEnemy()
            };
            enemy.MakeEnemyStronger(CurrentFloor); //gör enemies starkare beroende på vilken nivå man är på
            return enemy;
        }
    }
} 