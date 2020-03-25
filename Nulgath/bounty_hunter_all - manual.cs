/*
	Created by matabeitt
	This script farms for the rewards of Bounty Hunter pet 
	quest: New Worlds New Opportunities
	
	Requirements:
		- Own "Bounty Hunter's Drone Pet" pet.
*/

using RBot;
using System;

public class Script {

	private static int DELAY = 2000;
	
	public static string [] ITEMS = {
		"Dark Crystal Shard", "Diamond of Nulgath",
		"Unidentified 13", "Tainted Gem", "Voucher of Nulgath",
		"Voucher of Nulgath (non-mem)", "Totem of Nulgath",
		"Gem of Nulgath", "Fend Token", "Blood Gem of the Archfiend",
		"Fiend Token", "Essence of Nulgath", "Defeated Makai"
	};
	
	public void ScriptMain(ScriptInterface bot){
		bot.Options.SafeTimings = true;
		bot.Options.RestPackets = true;
		bot.Options.PrivateRooms = true;
		bot.Options.InfiniteRange = true;
		bot.Options.ExitCombatBeforeQuest = true;
		
		bot.Player.LoadBank();
		
		foreach (string item in ITEMS) {
			if (bot.Bank.Contains(item)) bot.Bank.ToInventory(item);
			bot.Drops.Add(item);
		}
		
		while (!bot.ShouldExit()) {
			bot.Quests.EnsureAccept(6183);
			
			while (!bot.Inventory.ContainsTempItem("Slugfit Horn", 5)) {
				bot.Player.Join("mobius", "Slugfit", "Bottom");
				bot.Sleep(DELAY);
				bot.Wait.ForCombatExit(DELAY);
			}
			
			while (!bot.Inventory.ContainsTempItem("Imp Flame", 3)) {
				if (bot.Map.Name != "mobius") bot.Player.Join("mobius", "Slugfit", "Bottom");
				bot.Sleep(DELAY);
			}
			
			if (!bot.Inventory.ContainsTempItem("Makai Fang", 5)) {
				bot.Player.Join("citadel");
				bot.Wait.ForCellChange("m22");
				bot.Player.Join("tercessuinotlim-1e9");
				while (!bot.Inventory.ContainsTempItem("Makai Fang", 5)) bot.Sleep(1000);
			}
			
			while (!bot.Inventory.ContainsTempItem("Cyclops Horn", 3)) {
				bot.Player.Join("faerie", "End", "Bottom");
				for (int i = 1; i < 3; i++) {
					if (bot.Inventory.ContainsTempItem("Cyclops Horn", i)
				}
				bot.Wait.ForCombatExit(1000);
			}
			
			while (!bot.Inventory.ContainsTempItem("Wereboar Tusk", 2)) {
				bot.Player.Join("greenguardwest", "West12", "Left");
			}
			
			bot.Quests.EnsureComplete(6183);
		}
	}
}
