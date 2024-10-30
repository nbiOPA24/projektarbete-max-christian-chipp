using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TutorialTheGame
{
    class Shaman : Enemy
    {
        // Egenskaper
        public int Mana { get; set; }
        public int BaseHeal { get; set; }

        // konstruktor för att skapa magikern:
        public Shaman(string name)
        {
            //BaseDamage = 10;
            BaseHeal = 5;
            Random random = new Random();
            Health = 35 + random.Next(0, 30);
            Mana = 20 + random.Next(0, 80);
            Armor = 5;
            Name = name;
            Experience = 5;
            // ExpReward = 5; // Får bestämma :D
        }

        // Magikern attackerar
        // return value - denna metod returnerar ett heltal som är skadan som magikern gör
       // public override int Attack()
        public int Heal(List<Enemy> enemies)
        {
            int Heal; // hur mycket skada ska magikern göra?

            // om magikern har mer än 10 mana, kan den kasta en eldboll
            // TODO: Lägg till fler attacker som vi slumpar emellan
            if (Mana > 10)
            {
                Enemy enemyToHeal = FindEnemyLowHp(enemies);
                Random random = new Random();
                Heal = BaseHeal + random.Next(0, 20);

                if (enemyToHeal != null)
                {
                enemyToHeal.Health += Heal;
                Console.WriteLine($"{Name} heals {enemyToHeal.Name} for {Heal} health");
                Console.WriteLine("---------------------------");

                    Mana -= 10;
                }
            }
            else 
            {
                Mana += 5;
                Console.WriteLine($"{Name} has no mana");
                Console.WriteLine("---------------------------");

                Heal = 0;
            }
            return Heal;
        }
       
        static Enemy FindEnemyLowHp(List<Enemy> enemies)
        {
            Enemy lowestHp = enemies[0];

            foreach(var enemy in enemies)
            {
                if (enemy.Health < lowestHp.Health)
                {
                    lowestHp = enemy;
                }
            }
            return lowestHp;
        }
    }
}
