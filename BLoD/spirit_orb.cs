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

	private static int TARGET_AMOUNT = 1000;
	
	public void ScriptMain(ScriptInterface bot){
	
		bot.Skills.StartTimer();
		bot.Options.SafeTimings = true;
		bot.Options.RestPackets = true;
		bot.Options.PrivateRooms = true;
		bot.Options.InfiniteRange = true;
		bot.Options.ExitCombatBeforeQuest = true;
		
		bot.Drops.Add("Bone Dust");
		bot.Drops.Add("Undead Essence");
		bot.Drops.Add("Undead Energy");
		bot.Drops.Add("Spirit Orb");
		
		bot.Drops.RejectElse = true;
		bot.Drops.Start();
		
		if (bot.Map.Name != "battleunderb") {
			bot.Sleep(2000);
			bot.Player.Join("battleunderb--9999", "Enter", "Right");
		} else {
			bot.Player.Jump("Enter", "Right");
		}
		
		bot.Player.WalkTo(268, 367);
		
		while (bot.Inventory.GetQuantity("Spirit Orb") < TARGET_AMOUNT) {
			//AcceptRoutine(bot, new int [] {2082, 2083});
			CompleteRoutine(bot, new int [] {2082, 2083});
			bot.Player.Kill("*");
		}
	}
	
	public void AcceptRoutine(ScriptInterface bot, int [] qid) {
		if (!bot.Quests.IsInProgress(qid[0])) bot.Quests.EnsureAccept(qid[0]);
		else if (!bot.Quests.IsInProgress(qid[1])) bot.Quests.EnsureAccept(qid[1]);
		else return;
	}
	
	public void CompleteRoutine(ScriptInterface bot, int [] qid) {
		if (!bot.Quests.IsInProgress(qid[0])) bot.Quests.EnsureAccept(qid[0]);
		if (!bot.Quests.IsInProgress(qid[1])) bot.Quests.EnsureAccept(qid[1]);
		if (!(bot.Quests.CanComplete(qid[0]) || bot.Quests.CanComplete(qid[1]))) return;
		bot.Player.WalkTo(921, 473);
		for (int i = 0; i < qid.Length; i++) {
			while (bot.Quests.CanComplete(qid[i])) {
				bot.Quests.EnsureComplete(qid[i]);
				bot.Quests.EnsureAccept(qid[i]);
			}
		}
		bot.Player.WalkTo(269, 97);
		bot.Player.WalkTo(268, 367);
	}
}
