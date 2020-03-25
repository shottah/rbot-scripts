using RBot;

public class Script {

	public static string [] requirements = {
		"Shadow Lich",
		"DragonFire of Nulgath",
		"Unidentified 19",
		"Essence of Nulgath",
		"Mystic Tribal Sword",
		"Doomatter",
		"Necrot",
		"Chaoroot",
		"Archfiend's Favor",
		"Mortality Cape of Revontheus",
		"King Klunk's Crown",
		"Facebreaker of Nulgath",
		"SightBlinder Axe of Nulgath"
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
		"Unidentified 19",
		"Tainted Gem",
		"Dark Crystal Shard", 
		"Diamond of Nulgath",
		"Voucher of Nulgath (non-mem)",
		"Totem of Nulgath",
		"Gem of Nulgath"
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
		
		// Accept quest: Willpower Extraction 5258
		bot.Quests.EnsureAccept(5258);
		
		if (bot.Inventory.Contains("Shadow Lich") == false) {
			bot.Player.Join("shadowfall", "Inside", "Right");
			bot.Shops.Load(89);
			bot.Shops.BuyItem(89, "Shadow Lich");
		}
		
		if (bot.Inventory.Contains("Mystic Tribal Sword") == false) {
			bot.Player.Join("arcangrove");
			bot.Shops.Load(214);
			bot.Shops.BuyItem(214, "Mystic Tribal Sword");
		}
		
		if (bot.Inventory.Contains("DragonFire of Nulgath") == false) {
			bot.Quests.EnsureAccept(765);
			Totem(bot, 3);
			Nulgath(bot, "Totem of Nulgath", 3);
			bot.Player.Join("evilwardage-1e9");
			bot.Player.HuntForItem("Skull Warrior", "Nulgath Rune 4", 1, tempItem:true);
			bot.Player.Jump("Enter", "Spawn");
			bot.Quests.EnsureComplete(765, 1316);
			bot.Player.Pickup("DragonFire of Nulgath");
		}
		
		if (bot.Inventory.Contains("Unidentified 19") == false) {
			Assistant(bot, "Unidentified 19");
		}
		
		if (bot.Inventory.Contains("Unidentified 19") == false) {
			Escherion(bot, "Unidentified 19");
		}
		
		if (bot.Inventory.Contains("Chaoroot", 5) == false) {
			bot.Player.Join("hydra");
			bot.Player.HuntForItem("Hydra Head", "Chaoroot", 5);
			bot.Player.Jump("Enter", "Spawn");
		}
		
		if (bot.Inventory.Contains("Necrot", 5) == false) {
			bot.Player.Join("deathsrealm-1e9");
			bot.Player.HuntForItem("Skeleton Fighter", "Necrot", 5);
			bot.Player.Jump("Enter", "Spawn");
		}
		
		if (bot.Inventory.Contains("Doomatter", 5) == false) {
			bot.Player.Join("vordredboss-1e9");
			bot.Player.HuntForItem("Vordred|Ultra Vordred|Enraged Vordred", "Doomatter", 5);
			bot.Player.Jump("Enter", "Spawn");
		}
		
		if (bot.Inventory.Contains("Mortality Cape of Revontheus") == false) {
			bot.Player.Join("underworld-1e9");
			bot.Player.HuntForItem("Skull Warrior|Legion Fenrir", "Archfiend's Favor", 35);
			bot.Shops.Load(452);
			bot.Shops.BuyItem(452, "Mortality Cape of Revontheus");
		}
		
		if (bot.Inventory.Contains("Archfiend's Favor", 50) == false) {
			bot.Player.Join("underworld-1e9");
			bot.Player.HuntForItem("Skull Warrior|Legion Fenrir", "Archfiend's Favor", 50);
			bot.Player.Jump("Enter", "Spawn");
		}
		
		if (bot.Inventory.Contains("King Klunk's Crown") == false) {
			bot.Player.Join("underworld");
			bot.Player.HuntForItem("Legion Fenrir|Laken", "King Klunk's Crown", 1);
			bot.Player.Jump("Enter", "Spawn");
		}
		
		if (bot.Inventory.Contains("Essence of Nulgath", 10) == false) {
			if (bot.Map.Name != "tercessuinotlim") {
				bot.Player.Join("citadel", "m22", "Left");
			}
			bot.Player.Join("tercessuinotlim", "Boss2", "Down");
			bot.Player.HuntForItem("Dark Makai", "Essenceof Nulgath", 10);
		}
		
		while (bot.Inventory.Contains("Facebreaker of Nulgath") == false) {
			bot.Quests.EnsureAccept(3046);
			
			if (bot.Inventory.Contains("Golden Shadow Breaker") == false) {
				bot.Player.Join("citadel");
				bot.Player.HuntForItem("Grand Inquisitor", "Golden Shadow Breaker", 1);
			}
			
			if (bot.Inventory.Contains("Shadow Terror Axe") == false) {
				bot.Player.Join("battleundera");
				bot.Player.HuntForItem("Bone Terror", "Shadow Terror Axe", 1);
			}
			
			Nulgath(bot, "Unidentified 13", 1);
			Nulgath(bot, "Tainted Gem", 5);
			Nulgath(bot, "Dark Crystal Shard", 5);
			Nulgath(bot, "Diamond of Nulgath");
			
			bot.Quests.EnsureComplete(3046);
			bot.Player.Pickup("Facebreaker of Nulgath");
		}
		
		// Complete quest: Willpower Extraction
		bot.Quests.EnsureComplete(5258);
		bot.Player.Pickup("Unidentified 34");
	}
	
	public void Totem(ScriptInterface bot, int quantity) {
		if (bot.Inventory.Contains("Voucher of Nulgath (non-mem)") == false) return;
		if (bot.Bank.Contains("Totem of Nulgath")) bot.Bank.ToInventory("Totem of Nulgath"); 
		if (bot.Inventory.Contains("Totem of Nulgath", 3)) return;
		
		if (bot.Map.Name != "tercessuinotlim") bot.Player.Join("citadel", "m22", "Left");
		bot.Player.Join("tercessuinotlim-1e9");
		while (bot.Inventory.Contains("Totem of Nulgath", 3) == false) {
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
	
	public void Nulgath(ScriptInterface bot, string item, int quantity=1) {
		if (bot.Bank.Contains(item)) bot.Bank.ToInventory(item);
		if (bot.Inventory.Contains(item, quantity)) return;
		
		while (bot.Inventory.Contains(item, quantity) == false) {
			bot.Quests.EnsureAccept(2566);
			
			bot.Player.Join("elemental");
			bot.Player.HuntForItem("Mana Golem", "Mana Energy for Nulgath", 13, tempItem:true);
			
			bot.Player.Join("gilead");
			while (bot.Inventory.ContainsTempItem("Mana Energy for Nulgath")) {
				bot.Player.HuntForItem("Mana Elemental", "Charged mana Energy for Nulgath", 5, tempItem:true);
				bot.Quests.EnsureComplete(2566);
				PickUp_Util(bot, larvae_drops);
				bot.Quests.EnsureAccept(2566);
			}
		}
	}
	
	public void Escherion(ScriptInterface bot, string item, int quantity=1) {
		if (bot.Inventory.Contains(item, quantity)) return;
		
		bot.Player.Join("escherion");
		
		while (bot.Inventory.Contains(item, quantity) == false) {
			bot.Quests.EnsureAccept(555);
			
			bot.Player.HuntForItem("Escherion", "Escherion's Helm", 1);
			
			bot.Wait.ForCombatExit(1000);
			bot.Quests.EnsureComplete(555);
			
			PickUp_Util(bot, assistant_drops);
		}
	}
	
	public void Assistant(ScriptInterface bot, string item, int quantity=1) {
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
