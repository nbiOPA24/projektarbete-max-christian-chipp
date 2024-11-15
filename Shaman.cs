using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TutorialTheGame
{
    class Shaman : Enemy
    {
        // Egenskaper, denna klassen har en BaseHeal istället för Damage.
        public int BaseHeal { get; set; }

        // konstruktor för shamanen:
        public Shaman(string name, int xp): base(name, xp)
        {
            BaseHeal = 10;
            Health = 35 + random.Next(0, 30);
            Mana = 20 + random.Next(0, 80);
            Armor = 5;
        }

        //Heal metod istället för attack, returnerar ett heltal som är hur mycket hp den healar
        public int Heal(List<Enemy> enemies)
        {
            int Heal; 
            if (Mana > 10)
            {
                Enemy enemyToHeal = FindEnemyLowHp(enemies);
                Heal = BaseHeal + random.Next(0, 20);
                if (enemyToHeal != null)
                {
                    enemyToHeal.Health += Heal;
                    Console.WriteLine($"{Name} heals {enemyToHeal.Name} for {Heal} health");
                    Ui.SmallLine();
                    Mana -= 10;
                }
            }
            else 
            {
                Mana += 5;
                Ui.NoMana(Name);
                Heal = 0;
            }
            return Heal;
        }
       // Metod för att hitta enemy med lägst HP
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
