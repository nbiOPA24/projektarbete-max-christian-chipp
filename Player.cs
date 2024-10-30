﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TutorialTheGame
{
    class Player
    {
        public string Name { get; set; }
        public double PlayerHealth { get; set; }
        public double PlayerDamage { get; set; }
        public double PlayerMana { get; set; }
        public int shieldStrength { get; set; }
        public double Armor { get; set; }
        public Stats PlayerStats {get; set;}

        public Player(string name, Stats stats)
        {
            Name = name;
            PlayerStats = stats;
            PlayerHealth = PlayerStats.CalculateStamina(PlayerStats.Stamina, 100);
            PlayerDamage = PlayerStats.CalculateStrength(PlayerStats.Strength, 20);
            PlayerMana = PlayerStats.CalculateIntelligence(PlayerStats.Intelligence, 5);
            Armor = 0;
            shieldStrength = 0;

        }
        public void TakeDamage(int damage)
        {
            //damage = Enemy.enemies[i].attack();

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