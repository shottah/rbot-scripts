using RBot;

public class Script {
	
	public void ScriptMain(ScriptInterface bot){
		bot.Options.SafeTimings = true;
		bot.Options.RestPackets = true;
		bot.Options.ExitCombatBeforeQuest = true;
		bot.Options.InfiniteRange = true;
		bot.Options.PrivateRooms = true;
		
		bot.Skills.StartTimer();
		
		
		while (true) {
			if (bot.Quests.CanComplete(6777)) {
				bot.Quests.EnsureComplete(6777);
			}
			
			bot.Quests.EnsureAccept(6777);
			
			if (bot.Map.Name != "battleontown") bot.Player.Join("battleontown");
			
			while (!bot.Quests.CanComplete(6777)) {
				bot.Player.Hunt("Chickencow");
			}
		}
	}
}
