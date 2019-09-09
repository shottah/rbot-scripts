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

		The script uses all skills indescriminately.

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
		
		bot.Inventory.BankAllCoinItems();
		
		bot.Bank.ToInventory("Legion Token");
		bot.Bank.ToInventory("Legion Soul Collector");
		bot.Bank.ToInventory("Undead Champion");
		
		bot.Player.Join("dreadrock");
		
		int targetQuest = 4849;
		string targetEnemies = "Fallen Hero|Hollow Wraith|Legion Sentinel|Shadowknight|Void Mercenary";
		string targetItem = "Dreadrock Enemy Recruited";
		int targetItemQuantity = 6;
		bool targetIsTemp = true;
		string targetGoal = "Legion Token";
		int targetAmount = 2000;
		
		bot.Drops.Add(targetGoal);
		bot.Drops.RejectElse = true;
		bot.Drops.Start();
		
		while (!bot.ShouldExit() || bot.Inventory.Contains(targetGoal, targetAmount)) {
			bot.Quests.EnsureAccept(targetQuest);
			if (bot.Quests.CanComplete(targetQuest)) bot.Quests.EnsureComplete(targetQuest);
			else if (!bot.Quests.CanComplete(targetQuest)) 
				bot.Player.HuntForItem(targetEnemies, targetItem, targetItemQuantity, targetIsTemp);
		}

		bot.Drops.Stop();
		bot.Exit();
	}
}
