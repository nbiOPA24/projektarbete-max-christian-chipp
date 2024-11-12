using System;
using System.Collections.Generic;

namespace TutorialTheGame
{
    public class FloorHandler
    {
        public int CurrentFloor {get; set;} // = 1;
        public Random random = new Random();

        public FloorHandler()
        {
            CurrentFloor = 10;
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
        public Enemy CreateHighLevelEnemy()
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
            if (CurrentFloor == 10)
            {
                return new Boss("Trangius");
            }
            else if (CurrentFloor <= 3)
            {
                return CreateLowLevelEnemy();
            }
            else if (CurrentFloor <= 6)
            {
                return CreateMidLevelEnemy();
            }
            else  //(CurrentFloor <= 9)
            {
                return CreateHighLevelEnemy();
            }
        }
    }
}