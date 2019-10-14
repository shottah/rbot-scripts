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
		
		bot.Skills.LoadSkills("./Skills/Generic.xml");
		bot.Skills.StartTimer();
		
		bot.Player.LoadBank();
		
		foreach (string item in ITEMS) {
			if (bot.Bank.Contains(item)) bot.Bank.ToInventory(item);
			bot.Drops.Add(item);
		}
		
		bot.Drops.RejectElse = true;
		bot.Drops.Start();
		
		while (!bot.ShouldExit() && !bot.Inventory.Contains("Voucher of Nulgath (non-mem)")) {
			
			bot.Quests.EnsureAccept(6183);
			
			do {
				TempItemRoutine(bot, "mobius", "Slugfit", "Slugfit Horn", 5);
				TempItemRoutine(bot, "mobius", "Fire Imp", "Imp Flame", 3);
				TempItemRoutine(bot, "faerie", "Cyclops Warlord", "Cyclops Horn", 3);
				bot.Player.Join("citadel", "m22", "Left");
				TempItemRoutine(bot, "tercessuinotlim", "Dark Makai", "Makai Fang", 5);
				TempItemRoutine(bot, "greenguardwest", "Big Bad Boar", "Wereboar Tusk", 2);
			} while (!bot.Quests.CanComplete(6183));
			
			bot.Quests.EnsureComplete(6183);
		}
		
		bot.Drops.Stop();
	}
	
	public void TempItemRoutine (ScriptInterface bot, string map, string enemy, string item, int quantity) {
		if (bot.Inventory.ContainsTempItem(item, quantity)) return;
		if (bot.Map.Name != map) bot.Player.Join(map);
		bot.Player.Hunt(enemy);
	}
}
