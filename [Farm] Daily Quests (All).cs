using RBot;

// Farm all Daily Quests.

public class Script {

	public void ScriptMain(ScriptInterface bot){
		bot.Options.SafeTimings = true;
		bot.Options.RestPackets = true;
		bot.Options.PrivateRooms = true;
		bot.Options.InfiniteRange = true;
		bot.Options.ExitCombatBeforeQuest = true;

		bot.Skills.LoadSkills("./Skills/VoidHighLord.xml");
		bot.Skills.StartTimer();
				
		bot.Player.LoadBank();
		bot.Inventory.BankAllCoinItems();
		
		// Shurpu Blaze Tokens for Pyromancer
		DailyRoutine(bot, "xancave", 2209, "Shurpu Ring Guardian", "Shurpu Blaze Token", 300);
		
		// Seals of Light and Darkness for Bright Knight Armor
		DailyRoutine(bot, "sepulchurebattle", 3825, "Ultra Sepulchure", "Seal of Darkness", 50);
		DailyRoutine(bot, "alteonbattle", 3826, "Ultra Alteon", "Seal of Light", 50);
		
		// Moglin MEAL for Moglin Pet.
		DailyRoutine(bot, "nexus", 4159, "Frogzard", "Moglin MEAL", 30);
		
		// Dage Scroll Fragment for Drakath's Armor
		DailyRoutine(bot, "mountdoomskull", 3596, "Chaos Spider", "Dage's Scroll Fragment", 30);
	
		// Shadow Shield for Shadowscythe General
		DailyRoutine(bot, "lightguardwar", 3828, "Lightguard Paladin|Citadel Crusader|Scorching Flame", "Shadow Shield", 500);
	
		// Various Ores from Necropolis
		// Change "false" to "true" if you have unlocked this quest.
		if (!bot.Quests.IsDailyComplete(2091)) {
			string [] ores = {"Aluminum", "Barium", "Gold", "Iron", "Copper", "Silver", "Platinum"};
			bot.Quests.EnsureAccept(2091);
			bot.Player.Join("stalagbite");
			bot.Player.HuntForItem("Balboa", "Axe of the Prospector", 1);
			bot.Player.HuntForItem("Balboa", "Raw Ore", 30, tempItem:true);
			bot.Quests.EnsureComplete(2091);
			
			foreach (string o in ores) {
				if (bot.Player.DropExists(o)) {
					bot.Bank.ToInventory(o);
					bot.Player.Pickup(o);
					bot.Inventory.ToBank(o);
				}
			}
		}
		
		bot.Player.Join("yulgar");
	}
	
	public void DailyRoutine (ScriptInterface bot, string map, int quest, string enemy, string item, int quantity) {
		bot.Sleep(1200);
		if (bot.Quests.IsDailyComplete(quest)) return;
		if (bot.Map.Name != map) bot.Player.Join(map);
		if (bot.Bank.Contains(item)) bot.Bank.ToInventory(item);
		if (!bot.Inventory.Contains(item, quantity)) {
			bot.Quests.EnsureAccept(quest);
			while (!bot.Quests.CanComplete(quest)) bot.Player.Hunt(enemy);
			bot.Quests.EnsureComplete(quest);
			bot.Wait.ForDrop(item);
			bot.Player.Pickup(item);
		}
		bot.Log(item + " (" + bot.Inventory.GetQuantity(item) + ")");
		bot.Inventory.ToBank(item);
	}
}
