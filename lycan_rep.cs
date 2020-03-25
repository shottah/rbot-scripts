/*
	Created by matabeitt
	This script farms for Lycan Rep Rank 10.

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
		
		bot.Skills.LoadSkills("./Skills/Generic.xml");
				
		while (!bot.ShouldExit()) {
			bot.Quests.EnsureAccept(537);
			bot.Player.Join("lycan");
			bot.Player.HuntForItem("Sanguine", "Sanguine Mask", 1, tempItem:true);
			bot.Quests.EnsureComplete(537);
		}
		
		bot.Exit();
	}
}
