
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
        //int playerMana = 10;  // temporär, ska så klart vara riktiga intelligence från stats
        //int intelligence = 10;  // temporär, ska såklart va riktiga intelligence från stats

        // kanske ha heal här med som en vanlig spell??
        
        public double Fireball(Player player)
        {
            double damage;
            

            if (player.PlayerMana >= 20)
            { //A basic fire spell that deals damage to a single enemy
                Random random = new Random();
                damage = 30 * player.PlayerStats.Intelligence + random.Next(0, 30);
                Console.WriteLine($"Fireball! has been cast for {damage} it's super effective!");
                Console.WriteLine("---------------------------");
                player.PlayerMana -= 20;
            }
            else
            {
                Console.WriteLine("You don't have enough mana to cast");
                Console.WriteLine("---------------------------");
                damage = 0;
                // kanske ha en plus mana här men vet inte ifall den kommer bara ge massa mana tills man kan casta igen eller om det blir per turn.
            }
            return damage;
        }
        public double LightningStrike(Player player)
        {  //A powerful spell that strikes an enemy with lightning, with a chance to stun.
            double damage;

            if (player.PlayerMana >= 25)
            {
                Random random = new Random();
                damage = 30 * player.PlayerStats.Intelligence + random.Next(0, 30);
                Console.WriteLine($"Lightning Srike zaps you for {damage}!");
                Console.WriteLine("---------------------------");
                player.PlayerMana -= 25;
                //kanske lägga till en stun effekt?
            }
            else
            {
                Console.WriteLine("You don't have enough mana to cast");
                Console.WriteLine("---------------------------");
                damage = 0;
                // kanske ha en plus mana här men vet inte ifall den kommer bara ge massa mana tills man kan casta igen eller om det blir per turn.
            }
            return damage;
        }
        public double ArcaneBlast(Player player)
        {  // A focused blast of raw arcane energy that ignores armor and shields.
            double damage;

            if (player.PlayerMana >= 30)
            {
                Random random = new Random();
                damage = 40 * player.PlayerStats.Intelligence + random.Next(0, 30);
                Console.WriteLine($"Casts Arcane Blast for {damage}!");
                Console.WriteLine("---------------------------");
                player.PlayerMana -= 25;
                //kanske lägga till en stun effekt?
            }
            else
            {
                Console.WriteLine("You don't have enough mana to cast");
                Console.WriteLine("---------------------------");
                damage = 0;
                // kanske ha en plus mana här men vet inte ifall den kommer bara ge massa mana tills man kan casta igen eller om det blir per turn.
            }
            return damage;

        }
        public void PoisonCloud(List<Enemy> enemies, Player player)
        {  //  summons a cloud of poisonous gas, dealing damage over time to all enemies
            double damage;
            if (player.PlayerMana >= 15)
            {
                player.PlayerMana -= 15;
                Random random = new Random();
                foreach (Enemy enemy in enemies)
                {
                    damage = 10 * player.PlayerStats.Intelligence + random.Next(0, 30);
                    enemy.Health -= damage; 
                    Console.WriteLine($"The area erupts with poison, damaging every enemy for {damage}!");
                    Console.WriteLine("---------------------------");
                    
                }
            }
            else
            {
                Console.WriteLine("You don't have enough mana to cast");
                Console.WriteLine("---------------------------");
                damage = 0;
                
            }
           // return damage;

        }
        public void IceShield(Player player)
        {  //  Creates a shield of ice around the player, reducing incoming damage for a few turns
            int shieldStrength;

            if (player.PlayerMana >= 20)
            {
                Random random = new Random();
                shieldStrength = (int) (40 * player.PlayerStats.Intelligence + random.Next(0, 30));
                player.shieldStrength = shieldStrength;
                Console.WriteLine($"You cast a ice shield that protects you for {shieldStrength}!");
                Console.WriteLine("---------------------------");
                player.PlayerMana -= 20;
                //lägga till så det är på alla enemies
            }
            else
            {
                Console.WriteLine("You don't have enough mana to cast");
                Console.WriteLine("---------------------------");
                //shieldStrength = 0;
                // kanske ha en plus mana här men vet inte ifall den kommer bara ge massa mana tills man kan casta igen eller om det blir per turn.
            }
            
        }
    }
}