/*
	Created by matabeitt
	This script farms for Evil Rep Rank 10.
	
	Requirements:
		The player must have 1 (one) free inventory 
		space.	

	Notes:
		The script uses a private room.
*/

using RBot;
using System.Timers;

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
		
		int quest = 367;
		string [] item = {"Replacement Tibia", "Phalanges"};
		int  [] q = {6, 3};
		
		bot.Player.Join("castleundead");
		
		while (!bot.ShouldExit()) {
			bot.Quests.EnsureAccept(quest);
			bot.Player.HuntForItems("Skeletal Warrior|Skeletal Viking", item, q, tempItems:true);
			bot.Quests.EnsureComplete(quest);
		}
		
		bot.Exit();
	}
}
