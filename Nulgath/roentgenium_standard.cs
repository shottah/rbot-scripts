/*
	Created by matabeitt
	This script farms for Roentgenium x1 for Void High Lord.
	There are many requirements, some are daily, so this is 
	essentially a daily script.
	

	Requirements:
		The player must have a number of free inventory 
		spaces.
		
		Must be able to do Nation Recruits: Seal Your Fate 
		quest in Shadowblast Arena.

	Notes:
		The script uses private rooms throughout the 
		farming process.
*/

using RBot;
using System;

public class Script{

	public void ScriptMain(ScriptInterface bot){
		bot.Skills.StartTimer();
		bot.Options.SafeTimings = true;
		bot.Options.RestPackets = true;
		bot.Options.PrivateRooms = true;
		bot.Options.ExitCombatBeforeQuest = true;
		
		for (int i = 1; i <= 4; i++) {
			bot.Skills.Add(i, 1f);
		}
		
		// Initialisation
		bot.Player.LoadBank();
		bot.Inventory.BankAllCoinItems();
		
		bot.Quests.EnsureAccept(5660);
		
		// Black Knight Orb
		BlackKnightOrb(bot);
		
		// Elder's Blood
		EldersBlood(bot);
		
		// Dwakel Decoder
		DwakelDecoder(bot);
		
		// Bounty Hunter
			// Unidentified 13 (x1)
			// Gem of Nulgath (x20)
			// Tainted Gem (x100)
			
		bot.Options.PrivateRooms = false;
		Nulgath (bot);
		bot.Options.PrivateRooms = true;
		
		// Nulgath Approval + Archfiend Favor (x300)
		Underworld (bot);
		
		// Emblem of Nulgath
		Emblem (bot);
		
		// Elemental Ink (x10)
		ElementalInk (bot);
		
		// The Secret 1
		Secret (bot);
		
		// Aelita's Emerald
		Aelita(bot);
		
		// Bone Dust (x20)
		BoneDust(bot);
		
		// Nulgath Shaped Chocolate
		Chocolate(bot);		
		
		// Essence of Nulgath (x50)
		Essence(bot);
		
	}
	
	public void BlackKnightOrb(ScriptInterface bot) {
		bot.Bank.ToInventory("Black Knight Orb");
		if (bot.Inventory.Contains("Black Knight Orb")) return;
		
		bot.Quests.EnsureAccept(318);
		
		bot.Player.Join("well");
		bot.Player.HuntForItem("Gell Oh No", "Black Knight Leg Piece", 1, true, true);
		
		bot.Player.Join("greendragon");
		bot.Player.HuntForItem("Greenguard Dragon", "Black Knight Chest Piece", 1, true, true);
		
		bot.Player.Join("deathgazer");
		bot.Player.HuntForItem("DeathGazer", "Black Knight Shoulder Piece", 1, true, true);
		
		bot.Player.Join("trunk");
		bot.Player.HuntForItem("Greenguard Basilisk", "Black Knight Arm Piece", 1, true, true);
		
		bot.Quests.EnsureComplete(318);
		
		bot.Wait.ForDrop("Black Knight Orb");
		bot.Player.Pickup("Black Knight Orb");
		
		return;
	}
	
	public void EldersBlood (ScriptInterface bot) {
		bot.Bank.ToInventory("Elders' Blood");
		if (bot.Inventory.Contains("Elders' Blood", 3)) return;
		
		bot.Player.Join("arcangrove");
		
		if (bot.Quests.IsDailyComplete(802)) return;
		
		bot.Player.HuntForItem("Gorillaphant", "Slain Gorillaphant", 50, true, true);
		
		bot.Quests.EnsureComplete(802);
		bot.Wait.ForDrop("Elders' Blood");
		bot.Player.Pickup("Elders' Blood");
		
		return;
	}
	
	public void DwakelDecoder(ScriptInterface bot) {
		bot.Bank.ToInventory("Dwakel Decoder");
		if (bot.Inventory.Contains("Dwakel Decoder")) return;
		
		bot.Player.Join("crashsite");
		
		bot.Sleep(1000);
		
		bot.SendPacket("%xt%zm%getMapItem%0%106%-1%false%wvz%");
		
		bot.Wait.ForDrop("Dwakel Decoder");
		bot.Player.Pickup("Dwakel Decoder");
		
		return;
	}
	
	public void Nulgath (ScriptInterface bot) {
		string [] larvae_items = {
			"Unidentified 10",
			"Unidentified 13", "Tainted Gem", "Gem of Nulgath", "Dark Crystal Shard", "Diamond of Nulgath",
			"Voucher of Nulgath", "Voucher of Nulgath (non-mem)", "Totem of Nulgath"
		};
		
		while (bot.Inventory.GetQuantity("Unidentified 13") < 1) {
			bot.Quests.EnsureAccept(2566);
			
			bot.Player.Join("gilead", "r8", "Down");
			bot.Player.HuntForItem("Mana Elemental", "Charged Mana Energy for Nulgath", 5, true, true);
			
			bot.Player.Join("elemental", "r5", "Down");
			bot.Player.HuntForItem("Mana Golem", "Mana Energy for Nulgath", 1, true, true);
			
			bot.Quests.EnsureComplete(2566);
			
			bot.Wait.ForDrop("Unidentified 10");
			bot.Player.Pickup(larvae_items);
			
			Sell (bot, larvae_items[6]);
			Sell (bot, larvae_items[7]);
			
			bot.Player.RejectExcept(larvae_items);
		}
		
		NulgathGem (bot);
	}
	
	public void Sell (ScriptInterface bot, string item) {
		if (bot.Inventory.Contains(item)) {
			if (bot.Map.Name != "yulgar") bot.Player.Join("yulgar");
			bot.Shops.Load(16);
			bot.Shops.SellItem(item);
		}
		return;
	}
	
	public void NulgathGem (ScriptInterface bot) {
		return;
	}
	
	public void TaintedGem (ScriptInterface bot) {
		bot.Bank.ToInventory("Tainted Gem");
		if (bot.Inventory.GetQuantity("Tainted Gem") >= 100) return;
		
		bot.Player.Join("battleunderb");
		
		bot.Bank.ToInventory("Bone Dust");
		bot.Bank.ToInventory("Tainted Gem");
		
		while (!bot.Inventory.Contains("Tainted Gem", 100)) {
			bot.Quests.EnsureAccept(568);
			bot.Player.HuntForItem("Skeleton Warrior", "Bone Dust", 25, false, false);
			bot.Player.Pickup("Undead Essence");
			bot.Quests.EnsureComplete(568);
			bot.Wait.ForDrop("Tainted Gem");
			bot.Player.Pickup("Tainted Gem");
		}
		
		return;
	}
	
	public void Underworld(ScriptInterface bot) {
		bot.Bank.ToInventory("Nulgath's Approval");
		bot.Bank.ToInventory("Archfiend's Favor");
		if (bot.Inventory.Contains("Nulgath's Approval", 300)) return;
		if (bot.Inventory.Contains("Archfiend's Favor", 300)) return;
		
		if (bot.Bank.Contains("Nulgath's Approval")) bot.Bank.ToInventory("Nulgath's Approval");
		if (bot.Bank.Contains("Archfiend's Approval")) bot.Bank.ToInventory("Archfiend's Favor");
		
		bot.Player.Join("evilwarnul");

		bot.Player.HuntForItems("Legion Fenrir", new string [] {"Archfiend's Favor", "Nulgath's Approval"}, new int [] {300, 300}, false, true);
		
		return;
	}
	
	public void Emblem (ScriptInterface bot) {
		bot.Bank.ToInventory("Nation Round 4 Medal");
		bot.Bank.ToInventory("Emblem of Nulgath");
		if (bot.Inventory.GetQuantity("Emblem of Nulgath") >= 20) return;
		
		bot.Player.Join("shadowblast");
		
		bot.Bank.ToInventory("Fiend Seal");
		bot.Bank.ToInventory("Gem of Domination");
		
		while (bot.Inventory.GetQuantity("Emblem of Nulgath") < 20) {
			bot.Quests.EnsureAccept(4748);
			bot.Player.Jump("r14", "Left");
			bot.Player.HuntForItems("Legion Airstrike|Paragon|Doombringer|Doomknight Prime", 
				new string [] {"Fiend Seal", "Gem of Domination"}, 
				new int [] {25, 1}, false, true);
			bot.Player.Jump("Enter", "Spawn");
			bot.Quests.EnsureComplete(4748);
			bot.Wait.ForDrop("Emblem of Nulgath");
			bot.Player.Pickup("Emblem of Nulgath");
		}
		bot.Sleep(2000);
		bot.Player.RejectAll();
		
		return;
	}
	
	public void ElementalInk(ScriptInterface bot) {
		bot.Bank.ToInventory("Elemental Ink");
		if (bot.Inventory.GetQuantity("Elemental Ink") >= 10) return;
		
		bot.Player.Join("mobius", "Slugfit", "Bottom");
		
		while (bot.Inventory.GetQuantity("Mystic Quills") < 20) {
			bot.Player.Kill("*");
			bot.Player.Pickup("Mystic Quills");
		}
		
		bot.Player.Join("spellcraft");
		
		while (bot.Inventory.GetQuantity("Mystic Quills") > 2) {
			bot.SendPacket("%xt%zm%buyItem%671975%13284%549%1637%");
        	bot.Sleep(1500);
		}
	}
	
	public void Secret(ScriptInterface bot) {
		bot.Bank.ToInventory("The Secret 1");
		if (bot.Inventory.Contains("The Secret 1")) return;
		
		bot.Player.Join("willowcreek");
		
		bot.Quests.EnsureAccept(623);
		bot.Player.HuntForItem("Hidden Spy", "The Secret 1", 1, false, true);
			
		return;
	}
	
	public void Aelita (ScriptInterface bot) {
		bot.Bank.ToInventory("Aelita's Emerald");
		if (bot.Inventory.Contains("Aelita's Emerald")) return;
		
		bot.Player.Join("yulgar");
		
		bot.Sleep(1000);
		
		bot.Shops.BuyItem("Aelita's Emerald");
		
		return;
	}
	
	public void BoneDust (ScriptInterface bot) {
		bot.Bank.ToInventory("Bone Dust");
		if (bot.Inventory.Contains("Bone Dust", 20)) return;
		
		bot.Player.Join("battleunderb");
		
		bot.Player.HuntForItem("Skeleton Warrior", "Bone Dust", 20, false, true);
		
		return;
	}
	
	public void Chocolate (ScriptInterface bot) {
		bot.Bank.ToInventory("Nulgath Shaped Chocolate");
		if (bot.Inventory.Contains("Nulgath Shaped Chocolate")) return;
		
		bot.Player.Join("citadel");
		
		bot.Sleep(1000);
		
		bot.Shops.BuyItem("Nulgath Shaped Chocolate");
		
		return;
	}
	
	public void Essence (ScriptInterface bot) {
		bot.Bank.ToInventory("Essence of Nulgath");
		bot.Bank.ToInventory("Defeated Makai");
		if (bot.Inventory.GetQuantity("Essence of Nulgath") >= 50) return;
		
		bot.Player.Join("citadel", "m22", "Left");
		bot.Sleep(1000);
		bot.Player.Join("tercessuinotlim");
		
		bot.Player.HuntForItem("Dark Makai", "Essence of Nulgath", 50, false, false);
		
		bot.Player.Pickup("Defeated Makai");
		
		return;
	}
}
