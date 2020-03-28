/*
	Created by matabeitt
	This script farms for Good Rep Rank 10.
	
	Requirements:
		The player must have 1 (one) free inventory 
		space.	

	Notes:
		The script uses a private room.
*/

using RBot;
using System.Timers;

public class Script{

	public static int 	QUEST = 3445;
	public static string TARGET = "ChaosWeaver Mage|ChaosWeaver Magi|ChaosWeaver Warrior";

	public void ScriptMain(ScriptInterface bot){
		bot.Skills.StartTimer();
		bot.Options.SafeTimings = true;
		bot.Options.RestPackets = true;
		bot.Options.PrivateRooms = true;
		bot.Options.ExitCombatBeforeQuest = true;
		
		if (bot.Skills.OverrideSkills == null) bot.Skills.LoadSkills("./Skills/Generic.xml");
		bot.Skills.StartTimer();
		
		while (!bot.ShouldExit()) {
			if (bot.Map.Name != "weaverwar") bot.Player.Join("weaverwar");
			if (!bot.Quests.IsInProgress(QUEST)) bot.Quests.EnsureAccept(QUEST);
			bot.Player.HuntForItem(TARGET, "Chaosweaver Slain", quantity:10, tempItem:true);
			if (bot.Quests.CanComplete(QUEST)) bot.Quests.EnsureComplete(QUEST);
		}
		
		bot.Exit();
	}
}
