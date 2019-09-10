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
		
		// Black Knight Orb
		BlackKnightOrb(bot);
		
		// Dwakel Decoder
		
		// Nulgath Approval + Archfiend Favor
		Underworld (bot);
		
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
	
	public void Underworld(ScriptInterface bot) {
		if (bot.Bank.Contains("Nulgath's Approval", 300)) return;
		if (bot.Bank.Contains("Archfiend's Favor", 300)) return;
		if (bot.Inventory.Contains("Nulgath's Approval", 300)) return;
		if (bot.Inventory.Contains("Archfiend's Favor", 300)) return;
		
		bot.Bank.ToInventory("Nulgath's Approval");
		bot.Bank.ToInventory("Archfiend's Favor");
		
		bot.Player.Join("underworld");

		bot.Player.HuntForItems("Legion Fenrir", new string [] {"Archfiend's Favor", "Nulgath's Approval"}, new int [] {300, 300}, false, true);
		
		return;
	}
}
