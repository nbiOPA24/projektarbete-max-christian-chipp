using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TutorialTheGame
{
    public class Loot
    {
        public List<Weapon> Weapons {get; set;}
        private static Random random = new Random();


        public Loot()
        {
            Weapons = new List<Weapon> // ska såklart ha stats
            {
              new Weapon("Sword of power", 15, "Sword", "Normal"), 
              new Weapon("Axe of fury", 20, "Axe", "Normal") 
            };
            
        }
        public Weapon GenerateLoot()
        {
            if (Weapons.Count == 0) return null;

            int index = random.Next(Weapons.Count);
            return Weapons[index];
        } 
        public void AddWeapon(Weapon weapon)
        {
            Weapons.Add(weapon);
        }
        
    }
   
}

