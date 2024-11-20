using System;
using System.Collections.Generic;
using System.ComponentModel;
namespace TutorialTheGame
{
    public class PlayerActionHandler
    {
        private CharacterSpells spells = new CharacterSpells();  //behöver vara private annars funkar inte spelet
        public Random random = new Random(); 
        public List<int> invisibleEnemyIndexes = new List<int>();

        // Här väljer du vad du vill göra, Attack, Heal, casta spell, spara spelet, avsluta, inventory.   kanske lägga till display stats?
        public void PlayerAction(Player player, List<Enemy> enemies)
        {   
            AddInvisibleEnemies(enemies); // uppdaterar osynliga fiende listan här så du inte kan attackera dom
            string input = Console.ReadLine();

            switch (input)
            {
               case "1":
                    AttackAction(player, enemies);
                    break;
                case "2":
                    player.Heal(50 + random.Next(0, 30));
                    break;

                case "3":
                    SpellAction(player, enemies);
                    break;

                case "4":
                    GameDataManager.SaveGame(player, "savegame.json");
                    Console.WriteLine("Game saved!");
                    break;

                case "5":
                    Console.WriteLine("End the game");
                    Environment.Exit(0); // Avslutar spelet
                    break;

                case "6":
                    player.InventoryHandler.ShowInventory();
                    inventoryAction(player);
                    break;
               
                default:
                    Ui.InvalidInput();
                    break;
            }
        }
        // bröt ut denna till en egen metod, Uppdaterar och lägger till i listan om fienden är osynlig.
        public void AddInvisibleEnemies(List<Enemy> enemies)
        {
            invisibleEnemyIndexes.Clear(); // om jag inte har denna så går det inte att attackera dom.
            for (int i = 0; i < enemies.Count; i++)
            {
                if (enemies[i].GetInfo() == null)
                {
                    invisibleEnemyIndexes.Add(i);
                }
            }
        }
        // Separat metod för att hantera basic attacken, check för att kolla så ingen är osynlig annars gör attacken
        public void AttackAction(Player player, List<Enemy> enemies)
        {
            Ui.ChooseTarget();
            int enemyIndex = int.Parse(Console.ReadLine()) - 1;

            if (enemyIndex < 0 || enemyIndex >= enemies.Count || invisibleEnemyIndexes.Contains(enemyIndex))
            {
                Ui.InvalidTarget();
                return;
            }

            Enemy e = enemies[enemyIndex];
            player.AttackEnemy(e);
        }
        // Enemys turn, Loopar igenom, om fienden är död så ge loot + xp sen ta bort från listan, logik för shaman, annars bli attackerad
        public void EnemiesAction(Player player, List<Enemy> enemies)
        {
            for (int i = 0; i < enemies.Count; i++)
            {
                if (enemies[i].Health <= 0)
                {
                    Ui.EnemyDiedMessage(enemies[i]);
                    player.AddExperience(enemies[i].ExperienceValue); // Lägg till XP
                    Weapon droppedLoot = enemies[i].DropLoot();
                    
                    if (droppedLoot != null)
                    {
                        player.InventoryHandler.PickUpLoot(droppedLoot);
                    }

                    enemies.RemoveAt(i);
                    i--;
                }
                else if (enemies[i] is Shaman shaman)
                {
                    shaman.Heal(enemies);
                }
                else
                {
                    player.TakeDamage(enemies[i].Attack());
                }
            }
            player.ManaRegeneration(); // kör enkel metod för att öka players mana med 10.
        }

        // Visar först val för spells, sen beroende på vilka sifra du väljer så får du single target eller aoe/shield metoden. 
        public void SpellAction(Player player, List<Enemy> enemies)
        {
            Ui.SpellOptions();
            string spellInput = Console.ReadLine();

            if (spellInput == "1" || spellInput == "2" || spellInput == "3")
            {
                Ui.ChooseTarget();
                int enemyIndex = int.Parse(Console.ReadLine()) - 1;

                if (enemyIndex < 0 || enemyIndex >= enemies.Count || invisibleEnemyIndexes.Contains(enemyIndex))
                {
                    Ui.InvalidTarget();
                    return;
                }

                Enemy e = enemies[enemyIndex];
                CastSingleTargetSpell(player, e, spellInput);
            }
            else
            {
                CastOtherSpell(player, enemies, spellInput);
            }
        }

        // Metod för single target spells, gör ett val sen körs attacken du väljer
        public void CastSingleTargetSpell(Player player, Enemy enemy, string spellInput)
        {
            double damage = 0;

            switch (spellInput)
            {
                case "1":
                    damage = spells.Fireball(player);
                    break;

                case "2":
                    damage = spells.LightningStrike(player);
                    break;

                case "3":
                    damage = spells.ArcaneBlast(player);
                    break;
            }

            if (damage > 0)
            {
                enemy.Defend(damage);
            }
        }

        // Övriga spells, poison är aoe spell och shield ger player en shield, dessa ignorerar armor så behöver ingen variabel för damage
        public void CastOtherSpell(Player player, List<Enemy> enemies, string spellInput)
        {
            switch (spellInput)
            {
                case "4":
                    spells.PoisonCloud(enemies, player);
                    break;

                case "5":
                    spells.IceShield(player);
                    break;

                default:
                    Ui.InvalidInput();
                    break;
            }
        }
        public void inventoryAction(Player player)
        {
            bool checkInventory = true;

            while (checkInventory)
            {
                Ui.DisplayInventoryMenu();
                string input = Console.ReadLine();

                switch(input)
                {
                    case "1":  // Equip (använd) ett vapen
                        player.InventoryHandler.ShowInventory();
                        Console.WriteLine("Choose a weapon to equip by entering its number:"); // lägga till så man ser nummer också....
                        string equipInput = Console.ReadLine();

                        bool isEquipIndex = int.TryParse(equipInput, out int equipIndex); //felkontroll 
                        bool isEquipInRange = equipIndex > 0 && equipIndex <= player.InventoryHandler.Inventory.Count;

                        if (isEquipIndex && isEquipInRange)
                        {
                            Weapon weaponToEquip = player.InventoryHandler.Inventory[equipIndex - 1];
                            player.InventoryHandler.EquipWeapon(player, weaponToEquip);
                        }
                        else
                        {
                            Ui.InvalidInput();
                        }
                        break;

                    case "2":  // Tar bort (unequippar) nuvarande vapen
                        player.InventoryHandler.UnequipWeapon(player);
                        break;
                    case "3":  // visar vilket vapen man har equippat
                        if (player.InventoryHandler.EquippedWeapon != null)
                        {
                            Console.WriteLine($"Currently equipped weapon: {Ui.DisplayWeaponInfo(player.InventoryHandler.EquippedWeapon)}");
                        }
                        else
                        {
                            Console.WriteLine("No weapon is currently equipped.");
                        }
                        break;
                    case "4": // visar hela inventoryn
                        player.InventoryHandler.ShowInventory();
                        break;
                    case "5": // avslutar loopen / exit inventory
                        checkInventory = false;
                         break;

                    default:
                        Ui.InvalidInput();
                        break;
                }
                Console.ReadKey();
            }
        }
    }
}