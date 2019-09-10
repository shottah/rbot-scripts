/*
	Created by matabeitt
	This script farms for Spirit Orbs by collecting Bone Dust 
	and Undead Essence from undead in Battleunder B.

	Requirements:
		The player must have 3 (three) free inventory 
		spaces.
			+ Bone Dust
			+ Undead Essence
			+ Spirit Orb
		
		Must have completed the previous quests from 
		Artix:
			+ Reforging the Blinding Light
			+ Secret Order of Undead Slayer

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
		
		string item = "Spirit Orb";
		int q = 65000;
		
		// Prelim Item Check
		bot.Bank.ToInventory("Bone Dust");
		bot.Bank.ToInventory("Undead Essence");
		bot.Bank.ToInventory("Spirit Orb");
		
		// Join the target map.
		bot.Player.Join("battleunderb", "Enter", "Right");
		int k = 1;
		while (!bot.ShouldExit() || bot.Inventory.Contains(item, quantity:q)) {
			bot.Player.Kill("*");
			bot.Player.Pickup(new string[] {"Bone Dust", "Undead Essence", "Spirit Orb"});
			handleQuest(bot, 2082);
			handleQuest(bot, 2083);
		}
		
		// Get Quests 
		// Essential Essences, Bone some Dust (2082, 2083)
		
		
	}
	
	public bool handleQuest (ScriptInterface bot, int qid) {
		if (!bot.Quests.IsInProgress(qid)) return bot.Quests.EnsureAccept(qid);
		if (bot.Quests.CanComplete(qid)) return bot.Quests.EnsureComplete(qid);
		return false;
	}
}
