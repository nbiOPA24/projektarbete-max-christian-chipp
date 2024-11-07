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
            damage = BaseDamage + random.Next(0, 100);
            if (Rage >= 110)
            {
                damage = random.Next(75, 175);
                Console.WriteLine($"{Name}You spin me right round, Baby, right round like barb in d2 swinging his axe, right round {damage}");
                Console.WriteLine("--------------------");

                Rage += random.Next(25, 50);
                Rage -= 110;
            }
            else
            {
                damage = BaseDamage + random.Next(20, 65);
                Console.WriteLine($"{Name} Stay Focus, Pointer hits you in the face, point taken {damage}");
                Console.WriteLine("((==))-------------------*   (-.*)");

            }
            return damage;

        }
          public override void Defend(double damage)
        {
            base.Defend(damage);
        }

    }
}