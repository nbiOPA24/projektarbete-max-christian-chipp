using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TutorialTheGame
{
    class Mage : Enemy
    {
        // Egenskaper
        public int Mana { get; set; }

        // konstruktor för att skapa magikern:
        public Mage(string name)
        {
            BaseDamage = 10;
            Random random = new Random();
            Health = 40 + random.Next(0, 20);
            Mana = 20 + random.Next(0, 80);
            Armor = 5;
            Name = name;
            Experience = 5;  // Får bestämma ;)
        }

        // Magikern attackerar
        // return value - denna metod returnerar ett heltal som är skadan som magikern gör
        public override int Attack()
        {
            int damage; // hur mycket skada ska magikern göra?

            // om magikern har mer än 10 mana, kan den kasta en eldboll
            // TODO: Lägg till fler attacker som vi slumpar emellan
            if (Mana > 10)
            {
                Random random = new Random();
                damage = BaseDamage + random.Next(0, 30);
                Console.WriteLine($"{Name} hurls a fireball at you for {damage} damage");
                Console.WriteLine("---------------------------");

                Mana -= 10;
            }
            else // magikern har inte tillräckligt med mana för att kasta en eldboll
            {
                Console.WriteLine($"{Name} has no mana for casting");
                Console.WriteLine("---------------------------");

                Mana += 5;
                damage = 0;
            }
            return damage;
        }
          public override void Defend(double damage)
        {
                base.Defend(damage);
        }
    }
}