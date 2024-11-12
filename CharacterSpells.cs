
﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace TutorialTheGame
{
    class CharacterSpells
    {
        // kanske ha heal här med som en vanlig spell??
        private static Random random = new Random();
        public bool HasEnoughMana(Player player, int manaCost)
        {
            if (player.PlayerMana >= manaCost) 
            {
                return true;
            }
            Console.WriteLine("You don't have enough mana to cast this spell."); 
            return false; 
        }
        public double Fireball(Player player)
        {
            int BaseDamage = 30;
            int manaCost = 20;
            

            if (HasEnoughMana(player,manaCost))
            { //A basic fire spell that deals damage to a single enemy
                double damage = BaseDamage * player.PlayerStats.Intelligence + random.Next(0, 30);
                Console.WriteLine($"Fireball! has been cast for {damage} it's super effective!");
                Console.WriteLine("---------------------------");
                player.PlayerMana -= manaCost;
                return damage;
            }
            return 0;
        }
        public double LightningStrike(Player player)
        {  //A powerful spell that strikes an enemy with lightning, with a chance to stun.
            int BaseDamage = 30;
            int manaCost = 25;

            if (HasEnoughMana(player,manaCost))
            {
                double damage = BaseDamage * player.PlayerStats.Intelligence + random.Next(0, 30);
                Console.WriteLine($"Lightning Srike zaps you for {damage}!");
                Console.WriteLine("---------------------------");
                player.PlayerMana -= manaCost;
                return damage;
                //kanske lägga till en stun effekt?
            }
           
            return 0;
        }
        public double ArcaneBlast(Player player)
        {  // A focused blast of raw arcane energy that ignores armor and shields.
            int BaseDamage = 40;
            int manaCost = 30;

            if (HasEnoughMana(player,manaCost))
            {
                double damage = BaseDamage * player.PlayerStats.Intelligence + random.Next(0, 30);
                Console.WriteLine($"Casts Arcane Blast for {damage}!");
                Console.WriteLine("---------------------------");
                player.PlayerMana -= manaCost;
                return damage;
                //kanske lägga till en stun effekt?
            }
            return 0;

        }
        public void PoisonCloud(List<Enemy> enemies, Player player)
        {  //  summons a cloud of poisonous gas, dealing damage over time to all enemies
            int BaseDamage = 10;
            int manaCost = 15;

            if (HasEnoughMana(player,manaCost))
            {
                player.PlayerMana -= manaCost;
                foreach (Enemy enemy in enemies)
                {
                    double damage = BaseDamage * player.PlayerStats.Intelligence + random.Next(0, 30);
                    enemy.Health -= damage; 
                    Console.WriteLine($"The area erupts with poison, damaging every enemy for {damage}!");
                    Console.WriteLine("---------------------------");
                    
                }
            }
           // return damage;

        }
        public void IceShield(Player player)
        {  //  Creates a shield of ice around the player, reducing incoming damage for a few turns
            int baseShieldStrength = 40;
            int manaCost = 20;

            if (HasEnoughMana(player,manaCost))
            {
                double shieldStrength = baseShieldStrength * player.PlayerStats.Intelligence + random.Next(0, 30);
                player.shieldStrength = shieldStrength;
                Console.WriteLine($"You cast a ice shield that protects you for {shieldStrength}!");
                Console.WriteLine("---------------------------");
                player.PlayerMana -= manaCost;
            }
        }
    }
}