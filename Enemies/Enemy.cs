using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TutorialTheGame       // to do, defend för arcane damage? med tanke på att den ska ignorera armor.
{   //skapa entities istället och ha subklasser för enemies?
    public abstract class Enemy  //abstrakt klass för alla andra enemies ärver från denna och har gemensamma egenskaper och metoder
    {
        // Alla fiender ska ha detta gemensamt:
        protected Random random;  // skapar en protected random som gör att alla enemies kan ha den.
        public string Name { get; set; }
        public double Health { get; set; }
        public int Mana {get; set;}
        public int BaseDamage { get; set; }
        public int Armor { get; set; }
        public double Level { get; set; }  // lägga till så levels ökar kanske, om vi inte kör krister lösning med json?
        public int ExperienceValue {get ; set;}
        public Loot loot {get; init;} // init betyder att man inte sätta / ge mer loot efter man skapat fienden, init står för intialisering så den sätter värdet bara i start

        public Enemy(string name, int xp)
        {
            random = new Random();
            Name = name;
            ExperienceValue = xp;
            loot = new Loot();
        }
        // En metod som gör så att enemies blir lite starkare beroende på vilken nivå man är på, ökar HP,Dmg och Armor med 10%.
        public void MakeEnemyStronger(int floorLevel)
        {
            double levelPower = 1 + (0.1 * (floorLevel -1));
            Health *= levelPower;
            BaseDamage = (int)(BaseDamage * levelPower);
            Armor = (int)(Armor * levelPower);
            Level = floorLevel;
        }
        // genererar loot (vapen) som droppas när enemy dör
        public Weapon DropLoot()
        {
            return loot.GenerateLoot();
        } 
            // En metod för att ge info information om fienden
        public virtual string GetInfo() // ?
        {
            return $"{Name} have {Health:F0} HP"; // avrundar till närmsta heltal med :F0
        }

            // Fienden attackerar.
        public virtual int Attack()  //kanske en default attack
        {
            return 0;
        }

            // Fienden försvarar sig. har även en kontroll ifall man skulle göra mindre skada än 0, annars får enemies HP istället.
        public virtual void Defend(double damage)
        {
            double totalDamage = damage - Armor;
            if (totalDamage < 0)
            {
                totalDamage = 0;
            }
            System.Console.WriteLine($"{Name} takes {totalDamage} damage"); // göra till en metod def om vi lägger till arcaneDefend
            Ui.BigLine();
            Console.WriteLine();
            Health -= totalDamage;
        }
    }
}
