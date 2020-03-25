using RBot;

public class Script {

	private static int combat_delay = 1500;

	public static string [] requirements = {
		"Unidentified 13",
		"Tainted Gem",
		"Dark Crystal Shard",
		"Diamond of Nulgath",
		"Unidentified 27",
		"Voucher of Nulgath",
		"Gem of Nulgath",
		"Blood Gem of the Archfiend",
		"Golden Hanzo Void",
		"Unidentified 34"
	};

	public static string [] larvae_drops = {
		"Unidentified 10", 
		"Unidentified 13", 
		"Tainted Gem",
		"Dark Crystal Shard", 
		"Diamond of Nulgath",
		"Voucher of NUlgath (non-mem)",
		"Totem of Nulgath",
		"Gem of Nulgath"
	};
	
	public static string [] assistant_drops = {
		"Unidentified 10", 
		"Unidentified 13",
		"Unidentified 26",
		"Primal Dread Fang",
		"Tainted Gem",
		"Dark Crystal Shard", 
		"Diamond of Nulgath",
		"Voucher of Nulgath (non-mem)",
		"Totem of Nulgath",
		"Gem of Nulgath"
	};
	
	public static string [] bounty_drops = {
		"Dark Crystal Shard", "Diamond of Nulgath",
		"Unidentified 13", "Tainted Gem", "Voucher of Nulgath",
		"Voucher of Nulgath (non-mem)", "Totem of Nulgath",
		"Gem of Nulgath", "Fend Token", "Blood Gem of the Archfiend",
		"Fiend Token", "Essence of Nulgath", "Defeated Makai"
	};

	//Inventory space required: 22
	public void ScriptMain(ScriptInterface bot){
		bot.Options.SafeTimings = true;
		bot.Options.RestPackets = true;
		bot.Options.SkipCutsenes = true;
		//bot.Options.PrivateRooms = true;
		
		bot.Skills.LoadSkills("./Skills/Generic.xml");
		bot.Skills.StartTimer();
		
		bot.Player.LoadBank();
		bot.Inventory.BankAllCoinItems();
		
		foreach (string core in requirements) {
			if (bot.Bank.Contains(core)) bot.Bank.ToInventory(core);
		}
		
		bot.Quests.EnsureAccept(5259);
		
		if (!bot.Inventory.Contains("Golden Hanzo Void")) {
			Escherion(bot, "Primal Dread Fang", 1);
			bot.Log("Farming Bounty Hunter for Golden Hanzo Void merge items...");
			BountyHunter(bot, "Diamond of Nuglath", 20);
			BountyHunter(bot, "Unidentified 13", 1);
			BountyHunter(bot, "Totem of Nulgath", 1);
			BountyHunter(bot, "Tainted Gem", 20);
			bot.Player.Join("evilwarnul");
			bot.Shops.Load(456);
			bot.Shops.BuyItem(456, "Golden Hanzo Void");
		}
		
		Nulgath(bot, "Unidentified 26", 1);
		
		if (!bot.Inventory.Contains("Ordinary Cape")) {
			bot.Log("Farming for Ordinary Cape items ...");
			bot.Quests.EnsureAccept(584);
			Nulgath(bot, "Tainted Gem", 5);
			bot.Player.Join("citadel", "m22", "Left");
			bot.Player.Join("tercessuinotlim-1e9");
			bot.Player.HuntForItem("Dark Makai", "Nulgath Rune 9", 1, tempItem:true);
			bot.Wait.ForCombatExit(2000);
			bot.Quests.EnsureComplete(584);
			bot.Player.Pickup("Ordinary Cape");
		}
		
		Nulgath(bot, "Unidentified 13", 1);
		Nulgath(bot, "Tainted Gem", 15);
		Nulgath(bot, "Dark Crystal Shard", 30);
		Nulgath(bot, "Diamond of Nulgath", 10);
		Nulgath(bot, "Voucher of Nulgath", 1);
		Nulgath(bot, "Gem of Nulgath", 15);
		
		BountyHunter(bot, "Blood Gem of the Archfiend", 2);
		
		bot.Wait.ForCombatExit(2000);
		if (bot.Quests.CanComplete(5259)) bot.Quests.EnsureComplete(5259);		
	}
	
	public void Totem(ScriptInterface bot, int quantity) {
		if (bot.Inventory.Contains("Voucher of Nulgath (non-mem)") == false) return;
		if (bot.Bank.Contains("Totem of Nulgath")) bot.Bank.ToInventory("Totem of Nulgath"); 
		if (bot.Inventory.Contains("Totem of Nulgath", quantity)) return;
		
		if (bot.Map.Name != "tercessuinotlim") bot.Player.Join("citadel", "m22", "Left");
		bot.Player.Join("tercessuinotlim-1e9");
		while (bot.Inventory.Contains("Totem of Nulgath", quantity) == false) {
			bot.Quests.EnsureAccept(4778);
			bot.Monsters.HuntCellBlacklist.Add("Boss2");
			bot.Player.HuntForItem("Dark Makai", "Essence of Nulgath", 60);
			bot.Player.Jump("Taro", "Left");
			bot.Quests.EnsureComplete(4778, 5357);
			PickUp_Util(bot, new string [] {"Totem of Nulgath"});
		}
	}
	
	public void PickUp_Util(ScriptInterface bot, string [] items) {
		bool banked = false;
		foreach (string item in items) {
			if (bot.Player.DropExists(item)) {
				if (bot.Bank.Contains(item)) {
					banked = true;
					bot.Bank.ToInventory(item);
				}
				bot.Player.Pickup(item);
				if (banked) bot.Inventory.ToBank(item);
			}
		}
	}
	
	public void TempItemRoutine (ScriptInterface bot, string map, string enemy, string item, int quantity) {
		if (bot.Inventory.ContainsTempItem(item, quantity)) return;
		if (bot.Map.Name != map) bot.Player.Join(map);
		bot.Sleep(1500);
		while (!bot.Inventory.ContainsTempItem(item,quantity)) {
			bot.Player.Hunt(enemy);
		}
		bot.Player.Jump("Enter", "Spawn");
		bot.Wait.ForFullyRested();
	}
	
	public void Nulgath(ScriptInterface bot, string item, int quantity=1) {
		bot.Log("Farming Nulgath larvae for " + item + " x"+ quantity);
		if (bot.Bank.Contains(item)) bot.Bank.ToInventory(item);
		if (bot.Inventory.Contains(item, quantity)) return;
		
		while (bot.Inventory.Contains(item, quantity) == false) {
			bot.Quests.EnsureAccept(2566);
			
			bot.Player.Join("elemental");
			bot.Player.HuntForItem("Mana Golem", "Mana Energy for Nulgath", 13, tempItem:true);
			
			bot.Player.Join("gilead");
			while (bot.Inventory.ContainsTempItem("Mana Energy for Nulgath")) {
				bot.Player.HuntForItem("Mana Elemental", "Charged mana Energy for Nulgath", 5, tempItem:true);
				bot.Wait.ForCombatExit(combat_delay);
				bot.Quests.EnsureComplete(2566);
				PickUp_Util(bot, larvae_drops);
				bot.Quests.EnsureAccept(2566);
			}
		}
	}
	
	public void Escherion(ScriptInterface bot, string item, int quantity=1) {
		bot.Log("Farming  Escherion for " + item + " x"+ quantity);
		if (bot.Inventory.Contains(item, quantity)) return;
		
		bot.Player.Join("escherion");
		
		while (bot.Inventory.Contains(item, quantity) == false) {
			bot.Quests.EnsureAccept(555);
			
			bot.Player.HuntForItem("Escherion", "Escherion's Helm", 1);
			
			bot.Wait.ForCombatExit(combat_delay);
			bot.Quests.EnsureComplete(555);
			
			PickUp_Util(bot, assistant_drops);
		}
	}
	
	public void BountyHunter (ScriptInterface bot, string item, int quantity=1) {
		if (bot.Bank.Contains(item)) bot.Bank.ToInventory(item);
		if (bot.Inventory.Contains(item, quantity)) return;
		while (bot.Inventory.Contains(item, quantity) == false) {
			bot.Quests.EnsureAccept(6183);
			do {
				bot.Log("Farming Bounty Hunter for " + item + " x"+ quantity);
				TempItemRoutine(bot, "mobius-1e9", "Slugfit", "Slugfit Horn", 5);
				TempItemRoutine(bot, "mobius-1e9", "Fire Imp", "Imp Flame", 3);
				TempItemRoutine(bot, "faerie-1e9", "Cyclops Warlord", "Cyclops Horn", 3);
				if (bot.Map.Name != "tercessuinotlim") bot.Player.Join("citadel-1e9", "m22", "Left");
				bot.Bank.ToInventory("Defeated Makai");
				TempItemRoutine(bot, "tercessuinotlim-1e9", "Dark Makai", "Makai Fang", 5);
				bot.Inventory.ToBank("Defeated Makai");
				TempItemRoutine(bot, "greenguardwest-1e9", "Big Bad Boar", "Wereboar Tusk", 2);
			} while (bot.Quests.CanComplete(6183) == false);
			bot.Wait.ForCombatExit(combat_delay);
			bot.Quests.EnsureComplete(6183);
			PickUp_Util(bot, bounty_drops);
		}
	}
	
	public void Assistant(ScriptInterface bot, string item, int quantity=1) {
		bot.Log("Farming Blood Cloak for " + item + " x"+ quantity);
		if (bot.Player.Gold < 100000 || bot.Inventory.Contains(item, quantity)) return;
		
		while (bot.Inventory.Contains(item, quantity) == false && bot.Player.Gold > 100000) {
			bot.Quests.EnsureAccept(2859);
			
			if (bot.Inventory.ContainsTempItem("Nulgath Rune 3") == false) {
				bot.Player.Join("underworld-1e9");
				bot.Player.HuntForItem("Legion Fenrir", "Nulgath Rune 3", 1, tempItem:true);
				bot.Player.Jump("Enter","Spawn");
			}
			
			bot.Player.Join("yulgar-1e9");
			bot.Shops.Load(16);
			if (bot.Inventory.Contains("Blood Cloak") == false) {
				bot.Shops.BuyItem(16, "Blood Cloak");
			}
			
			if (bot.Quests.CanComplete(2859)) bot.Quests.EnsureComplete(2859);
			
			PickUp_Util(bot, assistant_drops);
		}
	}
}
