/*
	Created by matabeitt
	This script farms for Legion Tokens x2000
	using a Dreadrock map quest called "Undead 
	Champion Recruitment".
	
	Requirements:
		The player must have 1 (one) free inventory 
		space.

		The player must have Undead Warrior (Armor) 
		in their inventory.	

	Notes:
		The script can be modified to go longer
		than Legion Tokens x2000.

		The script uses a private room.

		The player will use a hunt mode - which 
		teleports between room/cell to find new 
		targets.
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
		
		int quest = 236;
		string item = "Berserker Bunny";
		
		int currGold = bot.Player.Gold;
		int deltaGold = currGold + 1;
		
		bot.Player.Join("greenguardwest", "West12", "Up");
		
		while (!bot.ShouldExit() || deltaGold == 0) {
			if (bot.Player.DropExists(item)) bot.Player.Pickup(new string[] {item});
			else if (bot.Quests.CanComplete(quest)) bot.Quests.EnsureComplete(quest);
			else if (!bot.Quests.IsInProgress(quest)) bot.Quests.EnsureAccept(quest);
			else if (bot.Inventory.Contains(item)) {
				bot.Shops.SellItem(item);
				deltaGold = bot.Player.Gold - currGold;
				currGold = bot.Player.Gold;
			}
			else {
				bot.Player.HuntForItem("Big Bad Boar", "Were Egg", 1, tempItem:true);
			}
		}
		
		bot.Exit();
	}
}
