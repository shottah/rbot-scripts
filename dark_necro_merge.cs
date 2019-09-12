/*
	Created by matabeitt
	This script farms for the items required to 
	merge the Dark Metal Necro Class from the 
	Korn Concert Event.

	Requirements:
		The player must have 7 (seven) free inventory 
		spaces.	

	Notes:
		The script uses a private room.
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
		bot.Options.InfiniteRange = true;
		for (int i = 1; i <= 4; i++) {
			bot.Skills.Add(i, 1f);
		}
		
		bot.Player.Join("kornconcert");
		
		string [] tier1 =  {"Dark Metal Ore"};
		string [] tier2 =  {"Bronze Arena Coin", "Silver Arena Coin",
			"Gold Arena Coin", "Platinum Arena Coin"};
		
		while (!bot.ShouldExit() || (
			bot.Inventory.GetQuantity("Dark Metal Ore") < 200 &&
			bot.Inventory.GetQuantity("Rock Token") < 200 &&
			bot.Inventory.GetQuantity("Metal Badge") < 200)) {
			// If we do not have all the merge items, get them.
			if (bot.Inventory.GetQuantity("Dark Metal Ore") < 200) {
				bot.Player.HuntForItem("Fetid Beast|Eternal Mosher|Megadread|Toxic Beast", "Dark Metal Ore", 200, false, false);
				bot.Player.Pickup(tier2);
			}
			bot.Player.HuntForItems("Megadead|Giant Treasure Chest|Thrash|Toxic Beast|Large Toxic Beast", tier2, new int [] {600,600,600,600}, false, true);
		}
		bot.Exit();
	}
}
