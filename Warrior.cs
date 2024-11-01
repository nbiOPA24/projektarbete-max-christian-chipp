using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TutorialTheGame
{

    class Warrior : Enemy
    { 
        public int Rage { get; set; }

        public Warrior(string name)
        {

            Random random = new Random();
            Name = name;
            Health = 30 + random.Next(0, 40);
            BaseDamage = 15;
            Armor = 20;
            Rage = 0;
            Experience = 5;
            //ExpReward = 5; // får bestämma ;)
        }

        public override int Attack()
        {//             damage = BaseDamage + random.Next(0, 30);

            int damage;
            Random random = new Random();
            damage = BaseDamage + random.Next(0, 35);
            if (Rage >= 50) // MORTAL STRIKE
            {
                damage = random.Next(30, 70);
                Console.WriteLine($"{Name} gets ragefueled and hits you with MORTAL STRIKE and damages you for {damage}");
                Console.WriteLine("---------------------------");

                Rage += random.Next(1, 15);
                Rage -= 50;
            }
            else
            {
                damage = BaseDamage + random.Next(0, 35);
                Rage += random.Next(1, 31);
                Console.WriteLine($"{Name} smacks you with his axe for {damage}");
                Console.WriteLine("---------------------------");

            }
                return damage;
        }
    }
}