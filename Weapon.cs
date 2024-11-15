using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace TutorialTheGame
{
    public class Weapon
    {  //Lägga till stats?
        public string Name {get; set;}
        public double Damage {get; set;}
        public eWeaponType Type {get; set;}
        public string Rarity {get; set;}  // tänker att vi kanske lägger till rarity 
        public Stats WeaponStats {get; set;}

        public Weapon(string name, double damage, eWeaponType type, string rarity, Stats weaponStats)
        {
            Name = name;
            Damage = damage;
            Type = type;
            Rarity = rarity;
            WeaponStats = weaponStats;

        }
        public enum eWeaponType
        {
            Sword,
            Axe,
            Mace,
            Shield,
            Staff,
            Wand,

        }
    }
}