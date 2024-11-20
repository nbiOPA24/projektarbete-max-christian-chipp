﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TutorialTheGame
{
    class Boss : Enemy
    {
        bool isGiant;
        // konstruktor för bossen, anropar basklassen och sätter initiala värden.
        public Boss(string name) : base(name, 25) 
        {
            Health = 500 + random.Next(0, 100);
            BaseDamage = 50;
            Armor = 30;
            Mana = 0;
            isGiant = true;  // vad är denna till för ? framtid tanke? kanske öka defense/armor när den är aktiv, men är det värt det?
        }


        // Boss attack, bygger up mana gör sen en special ability om den får tillräckligt med mana som sen nollställs.    
        public override int Attack()
        {
            int damage;
            if (Mana >= 110)
            {
                damage = random.Next(100, 175);
                Console.WriteLine($"{Name} enters a furious spin, dealing {damage} damage!");
                Mana = 0;
            }
            else
            {
                damage = BaseDamage + random.Next(20, 65);
                Console.WriteLine($"{Name} strikes you with his axe, dealing {damage} damage!");
                Mana += random.Next(25, 50);
            }
            Ui.SmallLine();
            return damage;
        }
        public override void Defend(double damage)
        {
            base.Defend(damage);
        }
    }
}