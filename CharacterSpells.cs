
﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace TutorialTheGame
{
    class ChracterSpells
    {
        int playerMana = 10;  // temporär, ska så klart vara riktiga intelligence från stats
        int intelligence = 10;  // temporär, ska såklart va riktiga intelligence från stats

        // kanske ha heal här med som en vanlig spell??
        public int Fireball()
        {
            int damage;

            if (playerMana >= 20)
            { //A basic fire spell that deals damage to a single enemy
                Random random = new Random();
                damage = 30 * intelligence + random.Next(0, 30);
                Console.WriteLine($"Fireball! has been cast for {damage} it's super effective!");
                Console.WriteLine("---------------------------");
                playerMana -= 20;
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
        public int LightningStrike()
        {  //A powerful spell that strikes an enemy with lightning, with a chance to stun.
            int damage;

            if (playerMana >= 25)
            {
                Random random = new Random();
                damage = 30 * intelligence + random.Next(0, 30);
                Console.WriteLine($"Lightning Srike zaps you for {damage}!");
                Console.WriteLine("---------------------------");
                playerMana -= 25;
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
        public int ArcaneBlast()
        {  // A focused blast of raw arcane energy that ignores armor and shields.
            int damage;

            if (playerMana >= 30)
            {
                Random random = new Random();
                damage = 40 * intelligence + random.Next(0, 30);
                Console.WriteLine($"Casts Arcane Blast for {damage}!");
                Console.WriteLine("---------------------------");
                playerMana -= 25;
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
        public void PoisonCloud(List<Enemy> enemies)
        {  //  summons a cloud of poisonous gas, dealing damage over time to all enemies
            int damage;
            if (playerMana >= 15)
            {
                playerMana -= 15;
                Random random = new Random();
                foreach (Enemy enemy in enemies)
                {
                    damage = 10 * intelligence + random.Next(0, 30);
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
        public void IceShield()
        {  //  Creates a shield of ice around the player, reducing incoming damage for a few turns
            int shieldStrength;

            if (playerMana >= 20)
            {
                Random random = new Random();
                shieldStrength = 20 * intelligence + random.Next(0, 30);
                Console.WriteLine($"You cast a ice shield that protects you for {shieldStrength}!");
                Console.WriteLine("---------------------------");
                playerMana -= 20;
                //lägga till så det är på alla enemies
            }
            else
            {
                Console.WriteLine("You don't have enough mana to cast");
                Console.WriteLine("---------------------------");
                shieldStrength = 0;
                // kanske ha en plus mana här men vet inte ifall den kommer bara ge massa mana tills man kan casta igen eller om det blir per turn.
            }
            
        }
    }
}