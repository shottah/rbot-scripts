/*
	Created by matabeitt
	This script farms for Roentgenium x1 for Void High Lord.
	There are many requirements, some are daily, so this is 
	essentially a daily script.
	

	Requirements:
		The player must have a number of free inventory 
		spaces.

		This version requires Bounty Hunter's Drone Pet 
		to gather some materials - as it is easier.
		
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
		Bounty(bot);

		// Nulgath Approval + Archfiend Favor (x300)
		Underworld (bot);
		
		// The Secret 1
		Secret (bot);
		
		// Aelita's Emerald
		Aelita(bot);
		
		// Bone Dust (x20)
		BoneDust(bot);
		
		// Nulgath Shaped Chocolate
		Chocolate(bot);
		
		
		
	}
	
	public void BlackKnightOrb(ScriptInterface bot) {
		if (bot.Bank.Contains("Black Knight Orb")) return;
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
		if (bot.Bank.Contains("Elders' Blood")) return;
		if (bot.Inventory.Contains("Elders' Blood")) return;
		
		bot.Player.Join("arcangrove");
		
		bot.Quests.EnsureAccept(802);
		
		bot.Player.HuntForItem("Gorillaphant", "Slain Gorillaphant", 50, true, true);
		
		bot.Quests.EnsureComplete(802);
		bot.Wait.ForDrop("Elders' Blood");
		bot.Player.Pickup("Elders' Blood");
		
		return;
	}
	
	public void DwakelDecoder(ScriptInterface bot) {
		if (bot.Bank.Contains("Dwakel Decoder")) return;
		if (bot.Inventory.Contains("Dwakel Decoder")) return;
		
		bot.Player.Join("crashsite");
		
		bot.Sleep(1000);
		
		bot.SendPacket("%xt%zm%getMapItem%0%106%-1%false%wvz%");
		
		bot.Wait.ForDrop("Dwakel Decoder");
		bot.Player.Pickup("Dwakel Decoder");
		
		return;
	}
	
	public void Bounty(ScriptInterface bot) {
		string [] bounties = {
			"Dark Crystal Shard", "Diamond of Nulgath",
			"Unidentified 13", "Tainted Gem", "Voucher of Nulgath", "Voucher of Nulgath (non-mem)",
			"Totem of Nulgath", "Gem of Nulgath", "Fiend Token", "Blood Gem of the Archfiend"
		};
		
		foreach (string s in bounties) {
			bot.Bank.ToInventory(s);
		}
		if (bot.Bank.Contains("Unidentified 13", 1) &&
			bot.Bank.Contains("Gem of Nulgath", 20) &&
			bot.Bank.Contains("Tainted Gem", 100)) return;
		
		if (bot.Inventory.Contains("Unidentified 13", 1) &&
			bot.Inventory.Contains("Gem of Nulgath", 20) &&
			bot.Inventory.Contains("Tainted Gem", 100)) return;
			
		
			
		bot.Quests.EnsureAccept(6183);
		
		bot.Player.Join("mobius", "Slugfit", "Bottom");
		
		bot.Player.KillForItem("*", "Slugfit Horn", 5, true, true);
		
		bot.Player.HuntForItem("Fire Imp", "Imp Flame", 3, true, true);
		
		bot.Player.Join("citadel", "m22", "Left");
		bot.Sleep(1000);
		bot.Player.Join("tercessuinotim");
		
		bot.Player.HuntForItem("Dark Makai", "Makai Fang", 5, true, true);
		
		bot.Player.Join("greenguardwest");
		
		bot.Player.HuntForItem("Big Bad Boar", "Wereboar Tusk", 2, true, true);
		
		return;
	}
	
	public void Underworld(ScriptInterface bot) {
		if (bot.Bank.Contains("Nulgath's Approval", 300)) return;
		if (bot.Bank.Contains("Archfiend's Favor", 300)) return;
		if (bot.Inventory.Contains("Nulgath's Approval", 300)) return;
		if (bot.Inventory.Contains("Archfiend's Favor", 300)) return;
		
		if (bot.Bank.Contains("Nulgath's Approval")) bot.Bank.ToInventory("Nulgath's Approval");
		if (bot.Bank.Contains("Archfiend's Approval")) bot.Bank.ToInventory("Archfiend's Favor");
		
		bot.Player.Join("evilwarnul");

		bot.Player.HuntForItems("Legion Fenrir", new string [] {"Archfiend's Favor", "Nulgath's Approval"}, new int [] {300, 300}, false, true);
		
		return;
	}
	
	public void Secret(ScriptInterface bot) {
		if (bot.Bank.Contains("The Secret 1")) return;
		if (bot.Inventory.Contains("The Secret 1")) return;
		
		bot.Player.Join("willowcreek");
		
		bot.Quests.EnsureAccept(623);
		bot.Player.HuntForItem("Hidden Spy", "The Secret 1", 1, false, true);
			
		return;
	}
	
	public void Aelita (ScriptInterface bot) {
		if (bot.Bank.Contains("Aelita's Emerald")) return;
		if (bot.Inventory.Contains("Aelita's Emerald")) return;
		
		bot.Player.Join("yulgar");
		
		bot.Sleep(1000);
		
		bot.Shops.BuyItem("Aelita's Emerald");
		
		return;
	}
	
	public void BoneDust (ScriptInterface bot) {
		if (bot.Bank.Contains("Bone Dust", 20)) return;
		if (bot.Inventory.Contains("Bone Dust", 20)) return;
		
		bot.Player.Join("battleunderb");
		
		bot.Player.HuntForItem("Skeleton Warrior", "Bone Dust", 20, false, true);
		
		return;
	}
	
	public void Chocolate (ScriptInterface bot) {
		if (bot.Bank.Contains("Nulgath Shaped Chocolate")) return;
		if (bot.Inventory.Contains("Nulgath Shaped Chocolate")) return;
		
		bot.Player.Join("citadel");
		
		bot.Sleep(1000);
		
		bot.Shops.BuyItem("Nulgath Shaped Chocolate");
		
		return;
	}
}
