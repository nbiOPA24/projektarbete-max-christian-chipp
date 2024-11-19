﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Dynamic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Xml.XPath;

namespace TutorialTheGame
{
    public class Player
    {
        public string Name { get; set; }
        public double PlayerHealth { get; set; }
        public double PlayerDamage { get; set; }
        public double PlayerMana { get; set; }
        public double ShieldStrength { get; set; }
        public double Armor { get; set; }
        public Stats PlayerStats {get; set;}
        public InventoryHandler InventoryHandler {get; set;}
        public int Experience {get; set;}
        public int Level {get ; set;}


        public double BaseHealth { get; set; } // TEST
        public double BaseDamage { get; set; }
        public double BaseMana { get; set; }

        public Player(string name)
        {
            Name = name;
            PlayerStats = new Stats(10, 10, 10);
            InventoryHandler = new InventoryHandler(this);
           /* PlayerHealth = PlayerStats.CalculateStamina(PlayerStats.Stamina, 100); //1000
            PlayerDamage = 60;//PlayerStats.CalculateStrength(PlayerStats.Strength, 15); //2000
            PlayerMana = PlayerStats.CalculateIntelligence(PlayerStats.Intelligence, 10);//(PlayerStats.Intelligence, 0); // 500 */
            BaseHealth = 1000;
            BaseDamage = 100;   // TEST
            BaseMana = 100;
            Armor = 0;
            ShieldStrength = 0;
            Experience = 0;
            Level = 1;
            Console.WriteLine($"PlayerMana after calculation: {PlayerMana} dmg = {PlayerDamage} strength = {PlayerStats.Strength}, stam = {PlayerHealth}");
            UpdateStats(); // test
        }
        public void UpdateStats() //tillfällig skit för att räkna ut problemet
        {
            PlayerMana = BaseMana + Stats.CalculateIntelligence(PlayerStats.Intelligence);
            PlayerHealth = BaseHealth + Stats.CalculateStamina(PlayerStats.Stamina); //1000
            PlayerDamage = BaseDamage + (InventoryHandler.EquippedWeapon != null ? InventoryHandler.EquippedWeapon.Damage : 0) + Stats.CalculateStrength(PlayerStats.Strength);//InventoryHandler.EquippedWeapon.Damage + Stats.CalculateStrength(PlayerStats.Strength); //2000
            Console.WriteLine($"PlayerMana after calculation: {PlayerMana}, dmg = {PlayerDamage}, stam = {PlayerHealth}");
        }
        public void AddExperience(int xp)
        {
            Experience += xp;
            if (Experience >= GetExperienceForNextLevel())
            {
                Experience -= GetExperienceForNextLevel();
                LevelUp();
            }
        }
        public int GetExperienceForNextLevel()
        {
            return Level * 100; // första test med 100
        }
        private void LevelUp()
        {
            Level++;
            PlayerStats.IncreaseStats(5, this); // testar med att ge 5 stat points
            UpdateStats(); //tillfälle
            Console.WriteLine($"Congratulations! {Name} leveled up to level {Level}!");
        }
        public void TakeDamage(double damage)
        {
            bool isShieldBroken = false;
            if (ShieldStrength > 0)
            {
                double shieldAbsorbed = Math.Min(damage, ShieldStrength);
                damage -= shieldAbsorbed;
                ShieldStrength -= shieldAbsorbed;
                Console.WriteLine($"Your ice shield absorbed {shieldAbsorbed} damage! remaining shield: {ShieldStrength}");
               
                if (ShieldStrength <= 0 && !isShieldBroken)
                {
                    Console.WriteLine($"Ice shield is broken you take {damage} damage");
                    // onödig men ja. isShieldBroken = true;
                }
            }
            double totalDamage = damage - Armor;
            System.Console.WriteLine($"{Name} takes {totalDamage} damage");
            Ui.BigLine();
            Console.WriteLine();
            PlayerHealth -= totalDamage;
        }
        public void Heal(int amount)
        {
            PlayerHealth += amount;
            Console.WriteLine($"{Name} healed for {amount}. Current Health: {PlayerHealth}");
        }
        public void AttackEnemy(Enemy enemy)
        {
            Random random = new Random();
            double damage = PlayerDamage + random.Next(0, 10);
            Console.WriteLine($"You attack {enemy.Name} for {damage} damage");
            enemy.Defend(damage);
        }
    }
}