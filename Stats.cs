using System;
using System.Collections.Generic;
using System.Linq;

using System.Text;
using System.Threading.Tasks;

namespace TutorialTheGame
{
    public class Stats
    {   // Strength ökar dmg, stamina hp, int ökar mana + spell dmg
        public double Strength { get; set; }
        public double Stamina { get; set; }
        public double Intelligence { get; set; }
        public Stats (double strength, double stamina, double intelligence)
        {
            Strength = strength * 0.25;
            Stamina = stamina * 0.3;
            Intelligence = intelligence * 0.3;
        }
        // metoder för att räkna ut attackskada / stamina / mana
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

        // Metod för att öka statsen, du får olika alternativ sen ökar den på poängen
        public void IncreaseStats(int pointsAvailable)
        {
            while (pointsAvailable > 0)
            {
                Ui.DisplayStatsOptions(pointsAvailable); // Visar vilken stat du vill öka, strength, stamina, int
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
                            Ui.InvalidInput();
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
