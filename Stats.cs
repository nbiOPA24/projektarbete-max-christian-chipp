using System;
using System.Collections.Generic;
using System.Linq;

using System.Text;
using System.Threading.Tasks;

namespace TutorialTheGame
{
    public class Stats
    {
        public double Strength { get; set; }
        public double Stamina { get; set; }
        public double Intelligence { get; set; }

        public Stats (double strength, double stamina, double intelligence)
        {
            Strength = strength * 0.25;
            Stamina = stamina * 0.3;
            Intelligence = intelligence * 0.3;
        }
        public double CalculateStrength(double strength, double playerDamage) // kanske försöka streamlina lite och göra en metod om det går? maybe baby
        {
           
            playerDamage = playerDamage * strength;
            return playerDamage;


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
                Console.WriteLine($"You have {pointsAvailable} stat points to distribute.");  //göra en metod för detta också, statisk klass, statiska metoder
                Console.WriteLine("1. Increase Strength");
                Console.WriteLine("2. Increase Stamina");
                Console.WriteLine("3. Increase Intelligence");
                Console.Write("Choose where to allocate your points (1-3): ");
                
                string choice = Console.ReadLine();
                Console.Write("Enter how many points to allocate: ");

                // kontroll för att lägga till stats
                if (int.TryParse(Console.ReadLine(), out int points) && points > 0 && points <= pointsAvailable)
                {
                    switch (choice)  //kanske kan göra en metod för alla console.writelines med {stat} istället ?
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
                            Console.WriteLine("Invalid choice. Please select 1, 2, or 3."); //kanske invalid här med?
                            continue; // Skippa resten av loopen och be om ett nytt val
                    }
                    pointsAvailable -= points;
                }
                else
                {
                    Ui.InvalidInput();
                }
            }
            Console.WriteLine("All points have been distributed!");
        }
    }
}
