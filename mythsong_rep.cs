using RBot;

public class Script {

	public void ScriptMain(ScriptInterface bot){
		bot.Options.SafeTimings = true;
		bot.Options.RestPackets = true;
		bot.Options.PrivateRooms = true;
		bot.Options.InfiniteRange = true;
		bot.Options.ExitCombatBeforeQuest = true;
		
		bot.Skills.Add(1, 2f);
		bot.Skills.Add(2, 2f);
		bot.Skills.Add(3, 2f);
		bot.Skills.Add(4, 2f);
		
		bot.Skills.StartTimer();
		
		int q = 4829;
		
		bot.Player.Join("beehive");
		
		while (!bot.ShouldExit()) {
			bot.Quests.EnsureAccept(q);
			bot.Player.HuntForItem("Stinger", "Honey Gathered", 10, tempItem:true);
			bot.Quests.EnsureComplete(q);
		}
	}
}
