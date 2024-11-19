using System;
using System.Collections.Generic;
using System.Data;
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
        //public double StatMultiplier {get ; set;} = 1.0; // TEST
        public Stats (double strength, double stamina, double intelligence)
        {
            Strength = strength; // 0.25;  //kanske göra om till heltal istället och gör
            Stamina = stamina; //* 0.3;
            Intelligence = intelligence; //* 0.3;
        }
        // metoder för att räkna ut attackskada / stamina / mana
        public static double CalculateStrength(double strength)//, double StatMultiplier) // test metod 
        {
            
            return strength * 1; //* StatMultiplier;
        }
        public double CalculateStrengthh(double strength, double playerDamage) // kanske försöka streamlina lite och göra en metod om det går? maybe baby
        {
            playerDamage = playerDamage * (strength * 0.3);
            return playerDamage;
        }
        public static double CalculateStamina(double stamina)//, double StatMultiplier)
        {
            /*double playerHealthh = playerHealth * (stamina * 0.3);
            return playerHealthh; */
            return stamina * 1; //* StatMultiplier;
        }

        public static double CalculateIntelligence(double intelligencee)//, double StatMultiplier) //double playerManaa)
        {
           // playerMana = playerMana * (intelligence * 0.3);
           /* double CalcMana = playerManaa * (intelligencee * 0.3);
            Console.WriteLine($"Calculating Intelligence: Base Mana = {playerManaa}, Intelligence Multiplier = {intelligencee}, Result = {CalcMana}");
            return CalcMana; */
            return intelligencee * 1; // * StatMultiplier;
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
                            //playerStats.Strength += points;
                            Console.WriteLine($"Strength increased by {points}. New Strength: {Strength}");
                            break;
                        case "2":
                            Stamina += points;
                            Console.WriteLine($"Stamina increased by {points}. New Stamina: {Stamina}");
                            break;
                        case "3":
                            //Intelligence += points;
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
            //player.UpdateStats();
            Console.WriteLine("All points have been used!");
        }
    }
}
