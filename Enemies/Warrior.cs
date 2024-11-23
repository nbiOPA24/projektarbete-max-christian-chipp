using System;
using TutorialTheGame.GameHandler;

namespace TutorialTheGame.Enemies
{
    class Warrior : Enemy
    { 
        public Warrior(string name, int xp) : base(name, xp)
        {
            Health = 30 + random.Next(0, 40);
            BaseDamage = 15;
            Armor = 20;
            Mana = 0;
        }
        // Attackmetod för warrior, bygger upp mana genom vanliga attacker och gör sedan en special attack
        public override int Attack()
        {
            int damage;
            damage = BaseDamage + random.Next(0, 35);
            if (Mana >= 50) // MORTAL STRIKE
            {
                damage = random.Next(30, 70);
                Console.WriteLine($"{Name} gets ragefueled and hits you with MORTAL STRIKE and damages you for {damage}");
                Ui.SmallLine();
                Mana += random.Next(1, 15);
                Mana -= 50;
            }
            else
            {
                Mana += random.Next(1, 31);
                Console.WriteLine($"{Name} smacks you with his axe for {damage}");
                Ui.SmallLine();
            }
            return damage;
        }
        public override void Defend(double damage)
        {
            base.Defend(damage);
        }
    }
}