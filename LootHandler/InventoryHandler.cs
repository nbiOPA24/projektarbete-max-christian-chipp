using System;
using System.Collections.Generic;
using TutorialTheGame.GameHandler;
using TutorialTheGame.PlayerChar;

namespace TutorialTheGame.LootHandler
{
    public class InventoryHandler
    {
        public List<Weapon> Inventory {get; set;}
        public Weapon EquippedWeapon {get; set;}
        public Player player;

        public InventoryHandler(Player player)
        {
            Inventory = new List<Weapon>();
            this.player = player;
        }
        public void ShowInventory()
        {
            Console.WriteLine("Your Inventory:");
            for (int i = 0; i < Inventory.Count; i++)
            {
                Console.WriteLine($"[{i+1}] {Ui.DisplayWeaponInfo(Inventory[i])}");
            }
        }
        // equipar ett vapen och uppdaterar stats och dmg
        public void EquipWeapon(Player player, Weapon weapon)
        {
            if (EquippedWeapon != null)
            {
                UnequipWeapon(player);
            }
            EquippedWeapon = weapon;
            player.PlayerDamage += weapon.Damage; 
            player.PlayerStats.Strength += weapon.WeaponStats.Strength;
            player.PlayerStats.Stamina += weapon.WeaponStats.Stamina;
            player.PlayerStats.Intelligence += weapon.WeaponStats.Intelligence; 

            Console.WriteLine($"You have equipped {Ui.DisplayWeaponInfo(weapon)}");
            player.UpdateStats();
        }
        public void UnequipWeapon(Player player)
        {
            if (EquippedWeapon == null)
            {
                Console.WriteLine("No weapon equipped");
                return;
            }
            player.PlayerStats.Strength -= EquippedWeapon.WeaponStats.Strength;
            player.PlayerStats.Stamina -= EquippedWeapon.WeaponStats.Stamina;
            player.PlayerStats.Intelligence -= EquippedWeapon.WeaponStats.Intelligence;
            EquippedWeapon = null;
            Console.WriteLine($"You have unequipped your weapon");
            player.UpdateStats();
        }
        public void PickUpLoot(Weapon weapon)
        {
            Inventory.Add(weapon);
            Console.WriteLine($"You have picked up an {Ui.DisplayWeaponInfo(weapon)} Amazing!");
        }
    }
}