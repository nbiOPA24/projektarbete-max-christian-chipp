using System;
using TutorialTheGame.GameHandler;

namespace TutorialTheGame.Enemies
{
    class Mage : Enemy
    {
        // konstruktor för att skapa magikern:
        public Mage(string name, int xp) : base(name, xp)
        {
            BaseDamage = 10;
            Health = 40 + random.Next(0, 20);
            Mana = 20 + random.Next(0, 80);
            Armor = 5;
        }

        // Magikern attackerar
        // return value - denna metod returnerar ett heltal som är skadan som magikern gör
        public override int Attack()
        {
            int damage; // hur mycket skada ska magikern göra?

            // om magikern har mer än 10 mana, kan den kasta en eldboll
            // TODO: Lägg till fler attacker som vi slumpar emellan
            if (Mana > 10)
            {
                damage = BaseDamage + random.Next(0, 30);
                Console.WriteLine($"{Name} hurls a fireball at you for {damage} damage");
                Ui.SmallLine();

                Mana -= 10;
            }
            else // magikern har inte tillräckligt med mana för att kasta en eldboll
            {
                Ui.NoMana(Name);
                Mana += 5;
                damage = 0;
            }
            return damage;
        }
          public override void Defend(double damage)
        {
                base.Defend(damage);
        }
    }
}