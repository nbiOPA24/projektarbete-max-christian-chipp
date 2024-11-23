using TutorialTheGame.PlayerChar;

namespace TutorialTheGame.LootHandler
{
    public class Weapon
    {  
        public string Name {get; set;}
        public double Damage {get; set;}
        public eWeaponType Type {get; set;}
        public string Rarity {get; set;} 
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