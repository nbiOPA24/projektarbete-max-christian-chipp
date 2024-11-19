using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Dynamic;
namespace TutorialTheGame
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
            foreach (var weapon in Inventory)
            {
                Console.WriteLine(Ui.DisplayWeaponInfo(weapon));
            }
        }
        public void EquipWeapon(Player player, Weapon weapon)  //möjligtvis lägga till en kontroll för med equippedweapon == weapon, men verkar inte behövas, måste dubbelchecka med 2 av samma vapen.
        {
            if (EquippedWeapon != null)
            {
                UnequipWeapon(player);//, weapon);
            }
           //player.PlayerDamage += weapon.Damage;
            EquippedWeapon = weapon;
            player.PlayerStats.Strength += weapon.WeaponStats.Strength;
            player.PlayerStats.Stamina += weapon.WeaponStats.Stamina;
            player.PlayerStats.Intelligence += weapon.WeaponStats.Intelligence; 

            Console.WriteLine($"You have equipped {Ui.DisplayWeaponInfo(weapon)}");
            player.UpdateStats(); //test
            Console.WriteLine($"PlayerMana after calculation: {player.PlayerMana}");
        }
        public void UnequipWeapon(Player player)//,Weapon weapon)
        {
            if (EquippedWeapon == null)
            {
                Console.WriteLine("No weapon equipped");
                return;
            }
            //player.PlayerDamage -= EquippedWeapon.Damage;
            player.PlayerStats.Strength -= EquippedWeapon.WeaponStats.Strength;
            player.PlayerStats.Stamina -= EquippedWeapon.WeaponStats.Stamina;
            player.PlayerStats.Intelligence -= EquippedWeapon.WeaponStats.Intelligence;
            EquippedWeapon = null;  //eventuellt behöva ta bort från listan så det inte är duplicerat? eller kanske inte spelar roll */
            Console.WriteLine($"You have unequipped your weapon"); //{Ui.DisplayWeaponInfo(weapon)}");
            player.UpdateStats(); // test
            Console.WriteLine($"PlayerMana after calculation: {player.PlayerMana}");
        }
        public void PickUpLoot(Weapon weapon)
        {
            Inventory.Add(weapon);
            Console.WriteLine($"You have picked up an {Ui.DisplayWeaponInfo(weapon)} Amazing!");
        }
    }
}