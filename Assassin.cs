using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TutorialTheGame
{
    class Assassin : Enemy
    {
        // Förutom de egenskaper som finns i basklassen Enemy, har lönnmördaren möjligheten
        // att vara osynlig
        bool isVisible;
        private Random random = new Random();

        // Konstruktor
        public Assassin(string name, int xp) : base(name, xp)
        {
            Name = name;
            Health = 35 + random.Next(0, 30);
            BaseDamage = 20;
            Armor = 15;
            isVisible = false; // TODO: random på/av???
           // xp = 5; //Får bestämma ;) får ha det i konstruktorn istället
            //Level = Level * 0.3;
            
             
        }

        // Ger information om lönnmördaren.
        public override string GetInfo() /*?*/
        {
            if (isVisible)
                return base.GetInfo();
            else
                return null;
        }

        // lönnmördaren attackerar olika beroende på om den är synlig eller inte:
        public override int Attack()
        {
            int damage;
            // if it is visible, it will attack
            //Random random = new Random();

            if (isVisible)
            {
                damage = BaseDamage + random.Next(0, 30);
                Console.WriteLine($"{Name} Stabs you with it's dagger for {damage} damage");
                Console.WriteLine("---------------------------");

                isVisible = false;   
                return damage;
            }
            else
            {
                damage = BaseDamage + random.Next(0, 10);
                Console.WriteLine($"A arrow shoots from somewhere for {damage} damage");
                Console.WriteLine("---------------------------");

                isVisible = true;
                return damage;
            }
        }

        // lönnmördaren försvarar sig men bara om den är synlig, annars tar den ingen skada 
        public override void Defend(double damage)
        {
            if (isVisible)
            {
                base.Defend(damage);
            }
        }
    }
}