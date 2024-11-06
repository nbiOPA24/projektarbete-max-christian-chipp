using System;
using System.Collections.Generic;
using System.Linq;

using System.Text;
using System.Threading.Tasks;

namespace TutorialTheGame
{
    // TODO, göra playermana, sätta mana cost på heal, få increasestats med stamina och intelligence.

    public class Stats
    {

       /* public string Name { get; set; }
        public double Health { get; set; }
        public int BaseDamage { get; set; }
        public int Armor { get; set; }
        public int Experience { get; set; }
        public int Level {get ; set;} */
        public double Strength { get; set; }
        public double Stamina { get; set; }
        public double Intelligence { get; set; }

        public Stats (double strength, double stamina, double intelligence)
        {
            Strength = strength * 0.25;
            Stamina = stamina * 0.3;
            Intelligence = intelligence * 0.3;
        }

        /*public Stats(string name, double health, int baseDamage, int armor, int level, int experience, double strength, double stamina, double intelligence)
        {
            Name = name;
            Health = health;
            BaseDamage = baseDamage;
            Armor = armor;
            Level = level;
            Experience = experience;
            Strength = strength * 0.25;
            Stamina = stamina * 0.3;
            Intelligence = intelligence * 0.3;
        }
        */
        /*public Stats(double strength, double stamina, double intelligence)
        {
            Strength = strength * 0.25;
            Stamina = stamina * 0.3;
            Intelligence = intelligence * 0.3;
        } */
        public double CalculateStrength(double strength, double playerDamage) 
        {
           
            playerDamage = playerDamage * strength;
        // playerHealth = playerHealth * Stamina;
        // playerMana = playerMana * intelligence;
        return playerDamage;// + playerHealth + playerMana;


        }
        public double CalculateStamina(double stamina, double playerHealth)
        {
            playerHealth = playerHealth * stamina;
            return playerHealth;
        }

        public double CalculateIntelligence(double intelligence, double playerMana)
        {
            playerMana = playerMana * intelligence;
            return playerMana;
        }

        public void IncreaseStats(int pointsAvailable)
        {
            while (pointsAvailable > 0)
            {
                Console.WriteLine($"You have {pointsAvailable} stat points to distribute.");
                Console.WriteLine("1. Increase Strength");
                Console.WriteLine("2. Increase Stamina");
                Console.WriteLine("3. Increase Intelligence");
                Console.Write("Choose where to allocate your points (1-3): ");
                
                string choice = Console.ReadLine();
                Console.Write("Enter how many points to allocate: ");
                int points;

                // kontroll för att distrubutera poängen
                if (int.TryParse(Console.ReadLine(), out points) && points > 0 && points <= pointsAvailable)
                {
                    switch (choice)
                    {
                        case "1":
                            Strength += points;
                            Console.WriteLine($"Strength increased by {points}. New Strength: {Strength}");
                            break;
                        case "2":
                            Stamina += points;
                            Console.WriteLine($"Stamina increased by {points}. New Stamina: {Stamina}");
                            break;
                        case "3":
                            Intelligence += points;
                            Console.WriteLine($"Intelligence increased by {points}. New Intelligence: {Intelligence}");
                            break;
                        default:
                            Console.WriteLine("Invalid choice. Please select 1, 2, or 3.");
                            continue; // Skippa resten av loopen och be om ett nytt val
                    }
                    pointsAvailable -= points;
                }
                else
                {
                    Console.WriteLine("Invalid number of points or input. Please try again.");
                }
            }

            Console.WriteLine("All points have been distributed!");
        }
    }
    

}
