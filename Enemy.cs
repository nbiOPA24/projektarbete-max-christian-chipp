using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TutorialTheGame
{
    public class Enemy
    {
        // Alla fiender ska ha detta gemensamt:
        public string Name { get; set; }
        public double Health { get; set; }
        public int BaseDamage { get; set; }
        public int Armor { get; set; }
        public int Experience { get; set; } 
        public double Level { get; set; }


        //public Enemy(double strength, double stamina, double intelligence) : base(strength, stamina, intelligence)
        //{
        //    strength = 0;
        //    stamina = 0;
        //    intelligence= 0;

        //}
       // public Stats EnemyStats {get; set;}
       /* public Enemy (Stats enemyStats)
        {
            EnemyStats = enemyStats;
        }
        */
        // Metoder som alla fiender ska ha. Dessa anropas med hjälp av polymorfism:

        // En metod för att ge info information om fienden
        public virtual string GetInfo() // ?
        {
            return $"{Name} have {Health} HP";
        }

        // Fienden attackerar.
        public virtual int Attack()
        {
            return 0;
        }

        // Fienden försvarar sig.
        public virtual void Defend(double damage)
        {
            double totalDamage = damage - Armor;
            System.Console.WriteLine($"{Name} takes {totalDamage} damage");
            Console.WriteLine("========================================");
            Console.WriteLine();
            Health -= totalDamage;
        }
    }
}
