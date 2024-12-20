﻿﻿using System;
using TutorialTheGame.Enemies;
using TutorialTheGame.GameHandler;
using TutorialTheGame.LootHandler;

namespace TutorialTheGame.PlayerChar
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
        public double MaxMana { get; set; } //test
        public double MaxHealth { get; set; } //test

        public Player(string name)
        {
            Name = name;
            PlayerStats = new Stats(10, 10, 10);
            InventoryHandler = new InventoryHandler(this);
            BaseHealth = 400;
            BaseDamage = 30;   // sätter grund värden här så att vi kan starta spelet och beräkna senare med stats.
            BaseMana = 100;
            Armor = 0;
            ShieldStrength = 0;
            Experience = 0;
            Level = 1;
            UpdateStats();
            PlayerHealth = MaxHealth; //sätter grund värden för spelstart här så spelet startas.
            PlayerMana = MaxMana;
        }
        public void UpdateStats() //Updaterar statsen, utan denna så ökas inte statsen som tänkt.
        { 
            MaxMana = BaseMana + Stats.CalculateIntelligence(PlayerStats.Intelligence);
            MaxHealth = BaseHealth + Stats.CalculateStamina(PlayerStats.Stamina); //1000
            PlayerMana = Math.Min(PlayerMana, MaxMana);
            PlayerHealth = Math.Min(PlayerHealth,MaxHealth); //använder math.Min för att få ett värde som inte överskrider max hp
            PlayerDamage = BaseDamage 
             + (InventoryHandler.EquippedWeapon != null ? InventoryHandler.EquippedWeapon.Damage : 0) // kontroll för vapen stats (+ om du inte har vapen), utan den så kraschar spelet
             + Stats.CalculateStrength(PlayerStats.Strength);
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
            return Level * 100; 
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
            if (ShieldStrength > 0)
            {
                double shieldAbsorbed = Math.Min(damage, ShieldStrength);
                damage -= shieldAbsorbed;
                ShieldStrength -= shieldAbsorbed;
                Console.WriteLine($"Your ice shield absorbed {shieldAbsorbed} damage! remaining shield: {ShieldStrength}");
               
                if (ShieldStrength <= 0)
                {
                    Console.WriteLine($"Ice shield is broken you take {damage} damage");
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
            if (PlayerHealth > MaxHealth)
            {
                PlayerHealth = MaxHealth;
            }
            Console.WriteLine($"{Name} healed for {amount}. Current Health: {PlayerHealth}/{MaxHealth}");
            PlayerMana -= 15;
        }
        public void ManaRegeneration() //bus enkel metod för att öka mana, körs efter varje enemy action
        {
            PlayerMana += 5;
            if (PlayerMana > MaxMana)
            {
                PlayerMana = MaxMana;
            }
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