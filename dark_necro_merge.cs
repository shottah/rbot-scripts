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
		
		string [] tier2 =  {"Bronze Arena Coin", "Silver Arena Coin",
			"Gold Arena Coin", "Platinum Arena Coin"};
		
		while (!bot.ShouldExit() || (
			bot.Inventory.GetQuantity("Dark Metal Ore") < 200 &&
			bot.Inventory.GetQuantity("Rock Token") < 200 &&
			bot.Inventory.GetQuantity("Metal Badge") < 200)) {
			
			// Hunt for 200 Dark Metal Ores
			if (bot.Inventory.GetQuantity("Dark Metal Ore") < 200) {
			
				bot.Player.HuntForItem("Fetid Beast|Eternal Mosher|Megadread|Toxic Beast", "Dark Metal Ore", 200, false, false);
				bot.Player.Pickup(tier2);
			}
			
			// Perform this routine while the user does not have 200 Rock Tokens
			while (bot.Inventory.GetQuantity("Rock Token") < 200) {
				
				// Hunt for (60, 60) merge materials => 20 Rock Tokens
				while (bot.Inventory.GetQuantity("Platinum Arena Coin") < 60) {
					bot.Player.Join("kornthrash");
					bot.Player.Kill("*");
					bot.Player.Pickup("Platinum Arena Coin");
					bot.Sleep(1200);
				}
				
				bot.Player.Join("korntoxic");
				while (bot.Inventory.GetQuantity("Gold Arena Coin") < 60) {
					bot.Player.Kill("*");
					bot.Player.Pickup("Gold Arena Coin");
				}
				
				// Leave battle
				bot.Player.Jump("Enter", "Spawn");
				bot.Shops.Load(1749);
				
				// Perform this routine while the user has enough materials to buy up to 200 Rock Tokens
				while (bot.Inventory.GetQuantity("Platinum Arena Coin") > 3 &&
					bot.Inventory.GetQuantity("Gold Arena Coin") > 3 && 
					bot.Inventory.GetQuantity("Rock Token") < 200) {
					
					bot.Shops.BuyItem("Rock Token");
					bot.Sleep(1200);
				}
			}
			
			// Perform this routine while the user does not have 200 Meta Badges
			while (bot.Inventory.GetQuantity("Metal Badge") < 200) {
				
				// Hunt for (60, 60) merge materials => 20 Metal Badges
				bot.Player.HuntForItems("Megadead|Giant Treasure Chest|Eternal Rocker|Toxic Beast",
					new string [] {"Silver Arena Coin", "Bronze Arena Coin"}, new int [] {60, 60}, false, true);
				
				// Leave battle
				bot.Player.Jump("Enter", "Spawn");
				bot.Shops.Load(1749);
				
				// Perform this routine while the user has enough materials to buy up to 200 Metal Badges
				while (bot.Inventory.GetQuantity("Silver Arena Coin") > 3 &&
					bot.Inventory.GetQuantity("Bronze Arena Coin") > 3 && 
					bot.Inventory.GetQuantity("Metal Badge") < 200) {
					
					bot.Shops.BuyItem("Metal Badge");
					bot.Sleep(1200);
				} // while
			} // while
		} //while
		bot.Exit();
	} // ScriptMain
} // Script
