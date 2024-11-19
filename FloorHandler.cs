using System;
using System.Collections.Generic;

namespace TutorialTheGame
{
    public class FloorHandler
    {
        public int CurrentFloor {get; set;} // = 1;
        private Random random = new Random();

        public FloorHandler()
        {
            CurrentFloor = 1; //7
        }
        public void AdvanceFloor()
        {
            CurrentFloor++;
            Console.WriteLine($"You've defeated all your enemies and embark in the tower to {CurrentFloor} floor!");
        }
        public List<Enemy> CreateEnemies()
        {
            List<Enemy> enemies = new List<Enemy>();
            if (CurrentFloor == 10)
            {
                enemies.Add(new Boss("Trangius"));
            }
            else
            {
                int numberOfEnemies = random.Next(1,2 + CurrentFloor);
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
                0 => new Mage("Apprentice Mage", 200),
                _ => new Assassin("Novice Assasin", 200)
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
        public Enemy CreateEnemyForFloor()  // om det är 10, skapa boss, om det är 1-3 skapa low, 4-6 mid, 7-9 highlevel, måste ha <= => på båda sidorna.
        {
            Enemy enemy = CurrentFloor switch
            {
                10 => new Boss("Trangius"),
                <= 3 => CreateLowLevelEnemy(),
                <= 6 => CreateMidLevelEnemy(),
                _ => CreateHighLevelEnemy()
            };
            enemy.MakeEnemyStronger(CurrentFloor);
            return enemy;
        }
    }
}  // istället för olika metoder, lite mer generell, lite kortare, en enda metod? en lista med alla tänkbara fienderna,
// i listan finns t.ex dom finns på level 1,2 etc, max / min level,
// inte så många if satser, utan som mer en databas med olika enemies som kan slumpa fram, t.ex 1-2 enemies från level 1-4, 
//slumpar ut dom som har minimum level 2 och max floor typ 4, (inklusive)

// generisk metod, polymorfism?