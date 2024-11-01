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
    {
        public string Name {get; set;}
        public double Damage {get; set;}
        public string Type {get; set;}
        public string Rarity {get; set;}  // tänker att vi kanske lägger till rarity

        public Weapon(string name, double damage, string type, string rarity)
        {
            Name = name;
            Damage = damage;
            Type = type;
            Rarity = rarity;
        }
    }
}