using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TutorialTheGame
{
    class Boss : Enemy
    {
        public int Rage { get; set; }
        bool isGiant;
        public Boss(string name) : base(name, 25)
        {
            Random random = new Random();
            Name = name;
            Health = 500 + random.Next(0, 100);
            BaseDamage = 50;
            Armor = 100;
            Rage = 0;
            isGiant = true;  // vad är denna till för ? framtid tanke?
        }


        public override int Attack()
        {

            int damage;
            Random random = new Random();
            if (Rage >= 110)
            {
                damage = random.Next(100, 175);
                Console.WriteLine($"{Name} enters a furious spin, dealing {damage} damage!");
                Rage -= 110;
            }
            else
            {
                damage = BaseDamage + random.Next(20, 65);
                Console.WriteLine($"{Name} strikes you with his axe, dealing {damage} damage!");

            }
            Rage += random.Next(25, 50);
            Console.WriteLine("--------------------");
            return damage;

        }
          public override void Defend(double damage)
        {
            base.Defend(damage);
        }

    }
}