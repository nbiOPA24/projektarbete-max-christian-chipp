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

        public InventoryHandler()
        {
            Inventory = new List<Weapon>();
        }
        public void ShowInventory()
        {
            Console.WriteLine("Your Inventory:");
            foreach (var weapon in Inventory)
            {
                Console.WriteLine(Ui.DisplayWeaponInfo(weapon));
            }
        }
        public void EquipWeapon(Weapon weapon)
        {
            EquippedWeapon = weapon;
            Console.WriteLine($"You have equipped {Ui.DisplayWeaponInfo(weapon)}");
        }
        public void PickUpLoot(Weapon weapon)
        {
            Inventory.Add(weapon);
            Console.WriteLine($"You have picked up an {Ui.DisplayWeaponInfo(weapon)} Amazing!");
        }
    }
}