using System;
using TutorialTheGame.GameHandler;

namespace TutorialTheGame.Enemies
{
    class Assassin : Enemy
    {
        // Förutom de egenskaper som finns i basklassen Enemy, har assassin möjligheten att vara osynlig
        bool isVisible;
        // Konstruktor
        public Assassin(string name, int xp) : base(name, xp)
        {
            Health = 35 + random.Next(0, 30);
            BaseDamage = 20;
            Armor = 15;
            isVisible = false; // TODO: random på/av???
        }

        // Ger information om Assasin.
        public override string GetInfo() /*?*/
        {
            if (isVisible)
                return base.GetInfo();
            else
                return null;
        }
        // Assasin attackerar olika beroende på om den är synlig eller inte:
        public override int Attack()
        {
            int damage;
            // if it is visible, it will attack
            if (isVisible)
            {
                damage = BaseDamage + random.Next(0, 30);
                Console.WriteLine($"{Name} Stabs you with it's dagger for {damage} damage");
                Ui.SmallLine();

                isVisible = false;   
                return damage;
            }
            else // stealth attack
            {
                damage = BaseDamage + random.Next(0, 10);
                Console.WriteLine($"A arrow shoots from somewhere for {damage} damage");
                Ui.SmallLine();

                isVisible = true;
                return damage;
            }
        }

        // assasin försvarar sig men bara om den är synlig, annars tar den ingen skada 
        public override void Defend(double damage)
        {
            if (isVisible)
            {
                base.Defend(damage);
            }
        }
    }
}