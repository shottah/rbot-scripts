using RBot;

public class Script {

	public static string soloClass = "Lightcaster";
	public static string farmingClass = "Vampire Lord";

	public void ScriptMain(ScriptInterface bot){
		bot.Options.SafeTimings = true;
		bot.Options.RestPackets = true;
		bot.Options.InfiniteRange = true;
		bot.Options.ExitCombatBeforeQuest = true;
		
		bot.Skills.StartTimer();
		
		bot.Drops.Add("Exalted Crown");
		bot.Drops.Add("Legion Token");
		bot.Drops.Add("Dage's Favor");
		bot.Drops.Add("Diamond Token of Dage");
		bot.Drops.Add("Dark Token");
		bot.Drops.Add("Emblem of Dage");
		bot.Drops.Add("Defeated Makai");
		bot.Drops.Add("Legion Seal");
		bot.Drops.Add("Gem of Mastery");
		
		bot.Drops.RejectElse = true;
		bot.Drops.Start();
		
		bot.Player.LoadBank();
		bot.Inventory.BankAllCoinItems();
		
		bot.Bank.ToInventory("Exalted Crown");
		bot.Bank.ToInventory(soloClass);
		bot.Bank.ToInventory(farmingClass);
		
		while (bot.Inventory.GetQuantity("Exalted Crown") < 10) {
			bot.Quests.EnsureAccept(6899);
			LegionToken(bot, 50);
			DiamondToken(bot, 30);
			DageEmblem(bot, 1);
			LegionToken(bot, 4000);
			ItemRoutine(bot, "underworld-1e9", "Dark Makai|Undead Bruiser", "Dage's Favor", 300);
			DarkToken(bot, 100);
			
			bot.Player.Join("underworld");
			bot.Sleep(1200);
			if (!bot.Bank.Contains("Hooded Legion Cowl") && !bot.Inventory.Contains("Hooded Legion Cowl")) bot.Shops.BuyItem(216, "Hooded Legion Cowl");
			
			bot.Bank.ToInventory("Diamond Token of Dage");
			bot.Bank.ToInventory("Emblem of Dage");
			bot.Bank.ToInventory("Dark Token");
			bot.Bank.ToInventory("Hooded Legion Cowl");
			bot.Bank.ToInventory("Legion Token");
			
			bot.Quests.EnsureComplete(6899);
		}
		
		bot.Drops.Stop();
	}
	
	public void LegionToken (ScriptInterface bot, int q) {
		string item = "Legion Token";
		if (bot.Bank.Contains(item)) bot.Bank.ToInventory(item);
		if (bot.Bank.Contains("Infernal Caladbolg")) bot.Bank.ToInventory("Infernal Caladbolg");
		while (bot.Inventory.GetQuantity(item) < q) {
			bot.Quests.EnsureAccept(3722);
			TempItemRoutine(bot, "fotia-1e9", "Enter", "Spawn", "Fotia Elemental", "Betrayer Extinguished", 5);
			TempItemRoutine(bot, "evilwardage-1e9", "Enter", "Spawn", "Dreadfiend of Nulgath", "Fiend Felled", 2);
			bot.Quests.EnsureComplete(3722);
		}
		bot.Inventory.ToBank(item);
	}
	
	public void DarkToken (ScriptInterface bot, int q) {
		string item = "Dark Token";
		if (bot.Bank.Contains(item)) bot.Bank.ToInventory(item);
		if (bot.Map.Name != "seraphicwardage-1e9") bot.Player.Join("seraphicwardage-1e9");
		while (bot.Inventory.GetQuantity(item) < q) {
			bot.Quests.EnsureAccept(6249);
			bot.Quests.EnsureAccept(6251);
			while (!bot.Quests.CanComplete(6249) && !bot.Quests.CanComplete(6251)) {
				bot.Player.Hunt("Seraphic Commander");
			}
			if (bot.Quests.CanComplete(6249)) bot.Quests.EnsureComplete(6249);
			if (bot.Quests.CanComplete(6251)) bot.Quests.EnsureComplete(6251);
		}
	}
	
	public void DageEmblem (ScriptInterface bot, int q) {
		if (bot.Bank.Contains("Emblem of Dage")) return;
		if (bot.Bank.Contains("Legion Seal")) bot.Bank.ToInventory("Legion Seal");		
		if (!bot.Inventory.Contains("Emblem of Dage")) {
			if (bot.Map.Name != "shadowblast") bot.Player.Join("shadowblast", "r10", "Left");
			else bot.Player.Jump("r10", "Left");
			bot.Quests.EnsureAccept(4742);
			while (!bot.Quests.CanComplete(4742)) {
				bot.Player.Kill("*");			
			}
			bot.Quests.EnsureComplete(4742);
		}
		bot.Inventory.ToBank("Emblem of Dage");
	}
	
	public void DiamondToken(ScriptInterface bot, int q) {
		if (bot.Bank.Contains("Diamond Token of Dage")) bot.Bank.ToInventory("Diamond Token of Dage");
		if (bot.Bank.Contains("Legion Token")) bot.Bank.ToInventory("Legion Token");
		while (bot.Inventory.GetQuantity("Diamond Token of Dage") < 30 &&
			bot.Inventory.GetQuantity("Legion Token") >= 50) {
			bot.Quests.EnsureAccept(4743);
			TempItemRoutine(bot, "aqlesson", "Frame9", "Right", "Carnax", "Carnax Eye");
			TempItemRoutine(bot, "dflesson", "r12", "Right", "Fluffy the Dracolich", "Fluffy's Bones");
			TempItemRoutine(bot, "deepchaos", "Frame4", "Left", "Kathool", "Kathool Tentacle");
			TempItemRoutine(bot, "lair", "End", "Right", "Red Dragon", "Red Dragon's Fang");
			TempItemRoutine(bot, "bloodtitan", "Enter", "Spawn", "Blood Titan", "Blood Titan's Blade");
			bot.Player.Join("citadel", "m22", "Left");
			bot.Wait.ForMapLoad("citadel");
			ItemRoutine(bot, "tercessuinotlim-1e9", "Dark Makai", "Defeated Makai", quantity:25);
			bot.Quests.EnsureComplete(4743);
			bot.Player.Jump("Boss2", "Right");
			bot.Wait.ForCellChange("Boss2");
		}
		bot.Inventory.ToBank("Legion Token");
		bot.Inventory.ToBank("Diamond Token of Dage");
	}
	
	public void TempItemRoutine (ScriptInterface bot, string map, string cell, string pad, string enemy, string item, int quantity=1) {
		if (bot.Map.Name != map && bot.Inventory.GetTempQuantity(item) < quantity) bot.Player.Join(map, cell, pad);
		bot.Sleep(1200);
		bot.Player.EquipItem(soloClass);
		while (bot.Inventory.GetTempQuantity(item) < quantity) bot.Player.Hunt(enemy);
		bot.Player.Jump(bot.Player.Cell, bot.Player.Pad);
		bot.Sleep(1800);
	}
	
	public void ItemRoutine (ScriptInterface bot, string map, string enemy, string item, int quantity=1) {
		if (bot.Bank.Contains(item)) bot.Bank.ToInventory(item);
		if (bot.Map.Name != map && bot.Inventory.GetQuantity(item) < quantity) bot.Player.Join(map);
		bot.Sleep(1200);
		bot.Player.EquipItem(farmingClass);
		while (bot.Inventory.GetQuantity(item) < quantity) {
			bot.Player.Hunt(enemy);
		}
	}
}
