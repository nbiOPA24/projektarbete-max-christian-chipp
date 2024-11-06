﻿﻿using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
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
        public int shieldStrength { get; set; }
        public double Armor { get; set; }
        public Stats PlayerStats {get; set;}
        public List<Weapon> Inventory {get; set;}
        public Weapon EquippedWeapon {get;set;}
        public int Experience {get; set;}
        public int Level {get ; set;}

        public Player(string name, Stats stats)
        {
            Name = name;
            PlayerStats = stats;
            PlayerHealth = PlayerStats.CalculateStamina(PlayerStats.Stamina, 100);
            PlayerDamage = PlayerStats.CalculateStrength(PlayerStats.Strength, 20);
            PlayerMana = PlayerStats.CalculateIntelligence(PlayerStats.Intelligence, 500);
            Armor = 0;
            shieldStrength = 0;
            Inventory = new List<Weapon>();
            Experience = 0;
            Level = 1;

        }
        public void AddExperience(int xp)
        {
            Experience += xp;
            if (Experience >= GetExperienceForNextLevel())
            {
                LevelUp();
            }
        }

        private int GetExperienceForNextLevel()
        {
            return Level * 100; // exempel: 100 XP per level
        }

        private void LevelUp()
        {
            Level++;
            Experience = 0;
           // PlayerStats.IncreaseStats(5); // Exempel på att ge 5 extra stat-poäng
            Console.WriteLine($"Congratulations! {Name} leveled up to level {Level}!");
        }

       
        public void EquipWeapon(Weapon weapon)
        {
            EquippedWeapon = weapon;
            Console.WriteLine($"{Name} has equipped {weapon.Name}");
        }
        public void TakeDamage(int damage)
        {
            //damage = Enemy.enemies[i].attack();
            
            // lägga till bool isShieldBroken så vi inte får samma meddelande hela tiden.
            if (shieldStrength > 0)
            {
                int shieldAbsorbed = Math.Min(damage, shieldStrength);
                damage -= shieldAbsorbed;
                shieldStrength -= shieldAbsorbed;
                Console.WriteLine($"Your ice shield absorbed {shieldAbsorbed} damage! remaining shield: {shieldStrength}");
               
            }
            else if (damage > 0)
            {
                Console.WriteLine($"Ice shield is broken you take {damage} damage");
            }
           
            double totalDamage = damage - Armor;
            System.Console.WriteLine($"{Name} takes {totalDamage} damage");
            Console.WriteLine("========================================");
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