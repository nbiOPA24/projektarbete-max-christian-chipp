
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
        // kanske ha heal här med som en vanlig spell?? sätta level requirements
        private Random random = new Random();
        // bool för att se om playern har tillräckligt med mana för att attackera, så vi kan använda den på alla spells
        public bool HasEnoughMana(Player player, int manaCost)
        {
            if (player.PlayerMana >= manaCost) 
            {
                return true;
            }
            Console.WriteLine("You don't have enough mana to cast this spell."); 
            return false; 
        }
        public bool HasRequiredLevel(Player player, int requiredLevel)
        {
            if (player.Level >= requiredLevel)
            {
                return true;
            }
            Console.WriteLine($"You need to be at least level {requiredLevel} to cast this spell.");
            return false;
        }
        public double Fireball(Player player)
        {
            int BaseDamage = 30;
            int manaCost = 20;

            if (HasEnoughMana(player,manaCost) && HasRequiredLevel(player, 1))
            { //A basic fire spell that deals damage to a single enemy
                double damage = BaseDamage * (0.1 *player.PlayerStats.Intelligence) + random.Next(0, 30);
                Console.WriteLine($"Fireball! has been cast for {damage} it's super effective!");
                Ui.SmallLine();
                player.PlayerMana -= manaCost;
                return damage;
            }
            return 0;
        }
        // Sätter bas variabler, gör en mana kontroll, sen gör vi en Var på skadan baserat på Int och random, tar bort mana och skriver ut damagen.
        public double LightningStrike(Player player)
        {  //A powerful spell that strikes an enemy with lightning
            int BaseDamage = 30;
            int manaCost = 25;

            if (HasEnoughMana(player,manaCost) && HasRequiredLevel(player, 4))
            {
                double damage = BaseDamage * player.PlayerStats.Intelligence + random.Next(0, 30);
                Console.WriteLine($"Lightning Srike zaps you for {damage}!");
                Ui.SmallLine();
                player.PlayerMana -= manaCost;
                return damage;
                //kanske lägga till en stun effekt?
            }
           
            return 0;
        }
        public double ArcaneBlast(Player player)
        {  // A focused blast of raw arcane energy that ignores armor and shields. göra om så den ignorerar armor.
            int BaseDamage = 40;
            int manaCost = 30;

            if (HasEnoughMana(player,manaCost) && HasRequiredLevel(player, 5))
            {
                double damage = BaseDamage * player.PlayerStats.Intelligence + random.Next(0, 30);
                Console.WriteLine($"Casts Arcane Blast for {damage}!");
                Ui.SmallLine();
                player.PlayerMana -= manaCost;
                return damage;
            }
            return 0;

        }
        public void PoisonCloud(List<Enemy> enemies, Player player)
        {  //  summons a cloud of poisonous gas, dealing damage to all enemies
            int BaseDamage = 10;
            int manaCost = 15;

            if (HasEnoughMana(player,manaCost) && HasRequiredLevel(player, 3))
            {
                player.PlayerMana -= manaCost;
                foreach (Enemy enemy in enemies)
                {
                    double damage = BaseDamage * player.PlayerStats.Intelligence + random.Next(0, 30);
                    enemy.Health -= damage; 
                    Console.WriteLine($"The area erupts with poison, damaging every enemy for {damage}!"); // TODO skriva ut vilken enemy som tar skada
                    Ui.SmallLine();
                }
            }

        }
        public void IceShield(Player player)
        {  //  Creates a shield of ice around the player, reducing incoming damage
            int baseShieldStrength = 40;
            int manaCost = 20;

            if (HasEnoughMana(player,manaCost) && HasRequiredLevel(player, 2))
            {
                double shieldStrength = baseShieldStrength * player.PlayerStats.Intelligence + random.Next(0, 30);
                player.ShieldStrength = shieldStrength;
                Console.WriteLine($"You cast a ice shield that protects you for {shieldStrength}!");
                Ui.SmallLine();
                player.PlayerMana -= manaCost;
            }
        }
    }
}