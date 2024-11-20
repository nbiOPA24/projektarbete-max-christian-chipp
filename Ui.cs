using System;
using System.Collections.Generic;
using System.Threading;
namespace TutorialTheGame
{
    public static class Ui
    {
        //Enemy enemy = new Enemy();
        // kanske lägga till en "Press anykey to continue" för console.reeadkey
        public static void MenuOptions()
        {
            Console.WriteLine("Your options:");
            Console.WriteLine("1. Attack");
            Console.WriteLine("2. Heal");
            Console.WriteLine("3. Cast Spell");
            Console.WriteLine("4. Save Game");
            Console.WriteLine("5. End The Game");
            Console.WriteLine("6. Show Inventory");
            Console.Write("Choose 1-6:");
           // string input = Console.ReadLine();
        }
        public static void SpellOptions()
        {
            Console.WriteLine("Choose a spell: Required Level.");
            Console.WriteLine("1. Fireball, 1");
            Console.WriteLine("2. Lightning Strike, 4");
            Console.WriteLine("3. Arcane Blast, 5");
            Console.WriteLine("4. Poison Cloud, 3");
            Console.WriteLine("5. Ice Shield, 2");
            Console.Write("Choose 1-5: ");
        }
        public static void DisplayInfo(Player player, FloorHandler floorHandler)
        {
            BigLine();
            WriteColoredStat("You have ", $"{player.PlayerHealth}", ConsoleColor.Red, " / ", $"{player.MaxHealth}", " HP left");
            WriteColoredStat("You have ", $"{player.PlayerMana}", ConsoleColor.Cyan, " / ", $"{player.MaxMana}", " Mana left");
            WriteColoredStat("You have ", $"{player.Experience}", ConsoleColor.Yellow, " / ", $"{player.GetExperienceForNextLevel()}", " XP");
            Console.WriteLine($"Dmg = {player.PlayerDamage}, Str = {player.PlayerStats.Strength}, Stam = {player.PlayerStats.Stamina}, Int = {player.PlayerStats.Intelligence}");
            Console.WriteLine($"You are on floor {floorHandler.CurrentFloor}");
            BigLine();
        }
        public static void DisplayEnemies(List<Enemy> enemies)
        {
            Console.WriteLine("Existing enemies:");
            for (int i = 0; i < enemies.Count; i++)
            {
                if (enemies[i].GetInfo() != null) // kontrollerar så att fienden är synlig
                {
                    Console.WriteLine($"{(i + 1)}.  {enemies[i].GetInfo()}"); // skriv ut den synliga fienden
                }
            }
        }
        public static void ChooseTarget()
        {
            Console.Write("Who do you want to attack:");
        }
        public static void SmallLine()
        {
            Console.WriteLine("----------------------------------------");
        }
        public static void BigLine()
        {
            Console.WriteLine("========================================");
        }
        public static void WriteColoredStat(string prefix, string value1, ConsoleColor color, string middleText, string value2, string suffix)
        {  // slaveGPT metod för att få lite roliga färger, kände inte att det va så spännade/lärorikt att göra själv
            // hade jag gjort det själv så hade det nog varit olika färgade strings + strings = big string
            // Skriver prefixet i standardfärg (vit)
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write(prefix);

            // Skriver första värdet i vald färg
            Console.ForegroundColor = color;
            Console.Write(value1);

            // Skriver mellantexten i standardfärg (vit)
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write(middleText);

            // Skriver andra värdet i vald färg
            Console.ForegroundColor = color;
            Console.Write(value2);

            // Skriver suffixet i standardfärg (vit)
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine(suffix);

            // Återställ färg
            Console.ResetColor();
        }
        public static void DeathMessage()
        {
            SmallLine();
            Console.WriteLine("***** You died try again *****");
            SmallLine();
        }
        public static void GameClearMessage()
        {
            Console.WriteLine("--------You have cleared the Tutorial!--------");
        }
        public static void EnemyDiedMessage(Enemy enemy)
        {
            SmallLine();
            Console.WriteLine($"***  {enemy.Name} died  ***");
            SmallLine();
        }
        public static void InvalidInput()
        {
            Console.WriteLine("Invalid input, please try again");
        }
        public static void InvalidTarget()
        {
            Console.WriteLine("There was no enemy to attack, you strike air");
        }
        public static void NoMana(string enemyName)
        {
            Console.WriteLine($"{enemyName} has no mana for casting");
            SmallLine();
        }
        public static void DisplayStatsOptions(int pointsAvailable)
        {
            Console.WriteLine($"You have {pointsAvailable} stat points to distribute.");  //göra en metod för detta också, statisk klass, statiska metoder
            Console.WriteLine("1. Increase Strength");
            Console.WriteLine("2. Increase Stamina");
            Console.WriteLine("3. Increase Intelligence");
            Console.Write("Choose where to add your points (1-3): ");
        }
        public static string DisplayWeaponInfo(Weapon weapon)
        {
            return $"{weapon.Rarity} weapon: {weapon.Name} with {weapon.Damage} damage, Strength: {weapon.WeaponStats.Strength}, Stamina: {weapon.WeaponStats.Stamina}, Intelligence: {weapon.WeaponStats.Intelligence}";
        }
        public static void DisplayInventoryMenu()
        {
            Console.Clear();
            BigLine();
            Console.WriteLine("=== Inventory Menu ===");
            BigLine();
            Console.WriteLine("1. Equip Weapon");
            Console.WriteLine("2. Remove Weapon");
            Console.WriteLine("3. Display Current Weapon");
            Console.WriteLine("4. Show inventory");
            Console.WriteLine("5. Exit Inventory");
            Console.Write("Choose an option: ");
        }
        public static void PressKeyContinue()
        {
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
            Console.Clear();
        }
        public static void AskForName()
        {
            Console.WriteLine("What's your name Hero?");
        }
        public static void IntroStory(Player player)
        {   
            Console.WriteLine("The night sky was a cloak of darkness that stretched endlessly, as the ominous silhouette of the Tower of Trials loomed over the horizon.");
            Thread.Sleep(1200);
            Console.WriteLine("It was said that the tower appeared only to those brave or foolish enough to challenge its mysteries—a beacon for heroes and adventurers seeking fortune, glory, or escape.");
            Thread.Sleep(1200);
            Console.WriteLine($"You,{player.Name}, found yourself at its foot, staring up into the abyss above.");
            Thread.Sleep(1200);
            Console.WriteLine("The entrance was a cold archway carved with the runes of an ancient language.");
            Thread.Sleep(1200);
            Console.WriteLine("You couldn't help but feel that it judged you, weighing your worth as a challenger.");
            Thread.Sleep(1200);
            Console.WriteLine("But you did not hesitate, for you had your reasons to be here.");
            Thread.Sleep(1200);
            Console.WriteLine("Maybe it was to prove something to yourself, maybe to someone else—whatever it was, you knew that the tower held the answers you sought.");
            Thread.Sleep(1200);
        }
        public static void FirstFloorStory()
        {
            Console.WriteLine("The early floors were a test of will.");
            Thread.Sleep(1200);
            Console.WriteLine("The Apprentice Mages, frail but unpredictable, hurled fireballs that threatened to catch you off guard.");
            Thread.Sleep(1200);
            Console.WriteLine("You fought through novice assassins—quick and cunning—who ambushed from the shadows, testing your reflexes and resolve.");
            Thread.Sleep(1200);
            Console.WriteLine("These initial trials were meant to weed out the unworthy.");
            Thread.Sleep(1200);
            Console.WriteLine("The stones underfoot seemed to groan with the souls of past adventurers who hadn’t made it beyond the first few levels.");
            Thread.Sleep(1200);
            Console.WriteLine("But you knew, this was only the beginning.");
            Thread.Sleep(1200);
        }
        public static void MidLevelStory()
        {
            Console.WriteLine("As you climbed higher, the air grew thick with tension.");
            Thread.Sleep(1200);
            Console.WriteLine("Orc Warriors stomped down the narrow hallways, their weapons capable of shattering bones in one fell swing.");
            Thread.Sleep(1200);
            Console.WriteLine("Occultist Mages conjured dark spells that drained your strength, forcing you to think strategically.");
            Thread.Sleep(1200);
            Console.WriteLine("It wasn’t enough just to fight anymore—it was about survival.");
            Thread.Sleep(1200);
            Console.WriteLine("You had to decide when to use your spells, when to conserve mana, and when to call upon the potions you'd collected.");
            Thread.Sleep(1200);
            Console.WriteLine("With every enemy that fell, you grew stronger, the weight of your weapon becoming more familiar in your hand, and the power within you awakening.");
            Thread.Sleep(1200);
        }
        public static void HighLevelStory()
        {
            Console.WriteLine("Here, the tower revealed its true cruelty.");
            Thread.Sleep(1200);
            Console.WriteLine("The Elite Warriors stood as guardians, testing your every move, forcing you into prolonged battles that tested your endurance.");
            Thread.Sleep(1200);
            Console.WriteLine("The Witch of Fire scorched the very ground beneath you, her laughter echoing through the halls as her flames sought to consume you whole.");
            Thread.Sleep(1200);
            Console.WriteLine("The Shamans were perhaps the cruelest.");
            Thread.Sleep(1200);
            Console.WriteLine("Healing their allies, prolonging fights until you were on the brink of exhaustion.");
            Thread.Sleep(1200);
            Console.WriteLine("Every victory felt like it was paid for in blood, and every rest moment was fleeting, as you steeled yourself for what lay ahead.");
            Thread.Sleep(1200);
        }
        public static void BossLevelStory()
        {
            Console.WriteLine("You reached the final floor of this part of the tower, a vast circular chamber bathed in eerie light.");
            Thread.Sleep(1200);
            Console.WriteLine("The floor was etched with ancient sigils, and at its center stood Trangius, the Keeper of the Tower—a massive figure wielding an axe that seemed forged from nightmares.");
            Thread.Sleep(1200);
            Console.WriteLine("His roar shook the walls as he stepped forward, and you felt your courage tested to its very limits.");
            Thread.Sleep(1200);
            Console.WriteLine("This wasn’t just another battle—this was a reckoning.");
            Thread.Sleep(1200);
            Console.WriteLine("Trangius had seen countless challengers before you, and he did not intend to fall.");
            Thread.Sleep(1200);
            Console.WriteLine("The air crackled with tension as you prepared your spells, gripped your weapon tightly, and charged.");
            Thread.Sleep(1200);
        }
    }

}

/* foreach (var enemy in enemies)  // TEST FÖR ATT SE ATT VAPEN FUNGERAR 
        {
            if (enemy.loot == null)
            {
                Console.WriteLine($"Warning: {enemy.Name} does not have a loot instance.");
                continue;
            }

            Weapon testLoot = enemy.DropLoot();
            if (testLoot == null)
            {
                Console.WriteLine($"Warning: {enemy.Name} could not drop loot (loot list might be empty).");
            }
            else
            {
                Console.WriteLine($"Test: {enemy.Name} can drop {testLoot.Name} with {testLoot.Damage} damage.");
            }
        }
        Loot lootTest = new Loot();
        Weapon testWeapon = lootTest.GenerateLoot();

        if (testWeapon != null)
        {
            Console.WriteLine($"Test loot: {testWeapon.Name} with {testWeapon.Damage} damage and rarity {testWeapon.Rarity}.");
        }
        else
        {
            Console.WriteLine("Test loot failed to generate a weapon.");
        } */ 