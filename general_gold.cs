/*
	Created by matabeitt
	This script farms for gold by completing the 
	Berserker Bunny Armor quest and selling it 
	continuously. The script will finish when 
	there is no more gold income.
	
	Requirements:
		The player must have 1 (one) free inventory 
		space.	

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
