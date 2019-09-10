/*
	Created by matabeitt
	This script farms for Bone Dust (max) from 
	Battleunder B.

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
		
		
		bot.Player.Join("battleunderb");
		string item = "Bone Dust";
		int q = 5100;
		
		bot.Bank.ToInventory(item);
		
		double hp;
		while (!bot.ShouldExit() || bot.Inventory.Contains(item, quantity:q)) {
			hp = (double) bot.Player.Health / bot.Player.MaxHealth;
			bot.Log("Current Health: " + hp.ToString());
			if (hp < 0.4) {
				bot.Player.Jump("r1", "Left");
				bot.Player.Rest(full:true);
				bot.Player.Jump("Enter", "Right");
			}
			bot.Player.Kill("Skeleton Warrior");
			if (bot.Player.DropExists(item)) bot.Player.Pickup(item);
		}
		bot.Exit();
	}
}
