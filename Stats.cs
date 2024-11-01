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
    }

}
