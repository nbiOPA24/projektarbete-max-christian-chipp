using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TutorialTheGame       // to do, defend för arcane damage? med tanke på att den ska ignorera armor.
{   
    public class Enemy
    {
        // Alla fiender ska ha detta gemensamt:
        public string Name { get; set; }
        public double Health { get; set; }
        public int BaseDamage { get; set; }
        public int Armor { get; set; }
        public double Level { get; set; }  // lägga till så levels ökar kanske, om vi inte kör krister lösning med json?
        public int ExperienceValue {get ; set;}
        public Loot loot;
        public List<Weapon> Lootable {get ; set;}


        public Enemy(string name, int xp)
        {
            Name = name;
            ExperienceValue = xp;
            //Lootable = GenerateLoot();
            loot = new Loot();
        }

        public Weapon DropLoot()
        {
            return loot.GenerateLoot();
        } 
    
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
                if (totalDamage < 0)
                {
                    totalDamage = 0;
                }
                System.Console.WriteLine($"{Name} takes {totalDamage} damage");
                Console.WriteLine("========================================");
                Console.WriteLine();
                Health -= totalDamage;
        }
    }
}
