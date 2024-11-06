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


        public Loot()
        {
            Weapons = new List<Weapon>();
        }
        public void AddWeapon(Weapon weapon)
        {
            Weapons.Add(weapon);
        }
    }
   
}

