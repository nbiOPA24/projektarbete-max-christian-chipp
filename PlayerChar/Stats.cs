using System;
using TutorialTheGame.GameHandler;

namespace TutorialTheGame.PlayerChar
{
    public class Stats
    {   // Strength ökar dmg, stamina hp, int ökar mana + spell dmg
        public double Strength { get; set; }
        public double Stamina { get; set; }
        public double Intelligence { get; set; }
        public Stats (double strength, double stamina, double intelligence)
        {
            Strength = strength; 
            Stamina = stamina; 
            Intelligence = intelligence; 
        }
        // metoder för att räkna ut attackskada / stamina / mana
        public static double CalculateStrength(double strength)
        {
            return strength * 1;
        }
        public static double CalculateStamina(double stamina)
        {
            return stamina * 10;
        }

        public static double CalculateIntelligence(double intelligencee)
        {
            return intelligencee * 1;
        }
        // Metod för att öka statsen, du får olika alternativ sen ökar den på poängen
        public void IncreaseStats(int pointsAvailable, Player player)
        {
            while (pointsAvailable > 0)
            {
                Ui.DisplayStatsOptions(pointsAvailable); // Visar vilken stat du vill öka, strength, stamina, int
                string choice = Console.ReadLine();
                Console.Write("Enter how many points to add: ");

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
            Console.WriteLine("All points have been used!");
        }
    }
}
