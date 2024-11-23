using System;
using System.Collections.Generic;
using TutorialTheGame.PlayerChar;

namespace TutorialTheGame.LootHandler
{
    public class Loot
    {
        public List<Weapon> Weapons {get; set;}
        private Random random = new Random();

        // skapar vapen och lägger in dom i en lista
        public Loot()
        {
            Weapons = new List<Weapon> //ja alla fattar att jag inte skrivit alla vapen för hand, har ingen fantasi för olika namn och själv skriva stats
            {                           // love you big time slavegpt
                // Swords, Medelhög Strength, Mellan stamina, ingen intelligence.
                new Weapon("Knight's Sword", 10, Weapon.eWeaponType.Sword, "Normal", new Stats(5, 2, 0)),
                new Weapon("Blade of Valor", 15, Weapon.eWeaponType.Sword, "Rare", new Stats(7, 3, 0)),
                new Weapon("Guardian's Edge", 20, Weapon.eWeaponType.Sword, "Epic", new Stats(8, 4, 1)),
                new Weapon("Excalibur", 25, Weapon.eWeaponType.Sword, "Legendary", new Stats(12, 6, 2)),

                // Axes, Hög Strength, Lite stamina, ingen intelligence.
                new Weapon("Lumberjack Axe", 12, Weapon.eWeaponType.Axe, "Normal", new Stats(7, 1, 0)),
                new Weapon("Berserker Cleaver", 18, Weapon.eWeaponType.Axe, "Rare", new Stats(10, 2, 0)),
                new Weapon("Battle Axe of Rage", 22, Weapon.eWeaponType.Axe, "Epic", new Stats(13, 3, 1)),
                new Weapon("Destroyer's Hatchet", 28, Weapon.eWeaponType.Axe, "Legendary", new Stats(16, 4, 2)),

                // Maces, Blandade stats från alla.
                new Weapon("Iron Mace", 8, Weapon.eWeaponType.Mace, "Normal", new Stats(3, 3, 2)),
                new Weapon("Hammer of Justice", 12, Weapon.eWeaponType.Mace, "Rare", new Stats(5, 4, 3)),
                new Weapon("War Mace", 15, Weapon.eWeaponType.Mace, "Epic", new Stats(6, 5, 4)),
                new Weapon("Thunderstrike", 20, Weapon.eWeaponType.Mace, "Legendary", new Stats(7, 6, 5)),

                // Shields, Hög stamina, Låg Strength och Intelligence.
                new Weapon("Iron Shield", 5, Weapon.eWeaponType.Shield, "Normal", new Stats(2, 8, 1)),
                new Weapon("Aegis of Hope", 8, Weapon.eWeaponType.Shield, "Rare", new Stats(3, 10, 1)),
                new Weapon("Protector's Bulwark", 12, Weapon.eWeaponType.Shield, "Epic", new Stats(4, 12, 2)),
                new Weapon("Dragon Shield", 15, Weapon.eWeaponType.Shield, "Legendary", new Stats(5, 15, 3)),

                // Staves, Balanserad stamina och intelligence.
                new Weapon("Apprentice's Staff", 7, Weapon.eWeaponType.Staff, "Normal", new Stats(2, 5, 5)),
                new Weapon("Sorcerer's Rod", 10, Weapon.eWeaponType.Staff, "Rare", new Stats(3, 7, 7)),
                new Weapon("Archmage Staff", 14, Weapon.eWeaponType.Staff, "Epic", new Stats(4, 9, 10)),
                new Weapon("Celestial Staff", 18, Weapon.eWeaponType.Staff, "Legendary", new Stats(5, 12, 12)),

                // Wands, Hög intelligence, Lite lägre stamina.
                new Weapon("Wizard's Wand", 5, Weapon.eWeaponType.Wand, "Normal", new Stats(0, 4, 8)),
                new Weapon("Mystic Wand", 8, Weapon.eWeaponType.Wand, "Rare", new Stats(0, 5, 10)),
                new Weapon("Enchanter's Wand", 10, Weapon.eWeaponType.Wand, "Epic", new Stats(1, 6, 12)),
                new Weapon("Wand of the Arcane", 15, Weapon.eWeaponType.Wand, "Legendary", new Stats(2, 8, 15))
            };
            
        }
        // Genererar vapen
        public Weapon GenerateLoot()
        {
            if (Weapons.Count == 0) return null;

            int index = random.Next(Weapons.Count);
            return Weapons[index];
        } 
    }
}