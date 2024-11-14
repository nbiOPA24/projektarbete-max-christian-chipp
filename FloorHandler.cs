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
            CurrentFloor = 7;
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
        public Enemy CreateLowLevelEnemy()
        {
            int type = random.Next(2);

            if (type == 0)
            {
                return new Mage("Apprentice Mage", 10);
            }
            else
            {
                return new Assassin("Novice Assasin", 10);
            }
        }
        public Enemy CreateMidLevelEnemy()
        {
            int type = random.Next(3);
            if (type == 0)
            {
                return new Mage("Occultist Mage", 20);
            }
            else if (type == 1)
            {
                return new Warrior("Orc Warrior", 20);
            }
            else 
            {
                return new Assassin("Shadow Assasin", 20);
            }
        }
        public Enemy CreateHighLevelEnemy()  //switch istället, kanske nya switch med lambda
        {
            int type = random.Next(4);

            if (type == 0)
            {
                return new Mage("Witch of Fire", 30);
            }
            else if (type == 1)
            {
                return new Warrior("Elite Warrior", 30);
            }
            else if (type == 2)
            {
                return new Assassin("Ninja of West", 30);
            }
            else
            {
                return new Shaman("Shaman of the Forest", 30);
            }            
            
        }
    
        public Enemy CreateEnemyForFloor()
        {
            Enemy enemy;
            if (CurrentFloor == 10)
            {
                enemy = new Boss("Trangius");
            }
            else if (CurrentFloor <= 3)
            {
                enemy = CreateLowLevelEnemy();
            }
            else if (CurrentFloor <= 6)
            {
                enemy = CreateMidLevelEnemy();
            }
            else  //(CurrentFloor <= 9)
            {
                enemy = CreateHighLevelEnemy();
            }
            enemy.MakeEnemyStronger(CurrentFloor);
            return enemy;
        }
    }
}  // istället för olika metoder, lite mer generell, lite kortare, en enda metod? en lista med alla tänkbara fienderna,
// i listan finns t.ex dom finns på level 1,2 etc, max / min level,
// inte så många if satser, utan som mer en databas med olika enemies som kan slumpa fram, t.ex 1-2 enemies från level 1-4, 
//slumpar ut dom som har minimum level 2 och max floor typ 4, (inklusive)

// generisk metod, polymorfism?