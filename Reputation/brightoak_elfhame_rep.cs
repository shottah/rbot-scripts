using RBot;

public class Script {

	public void ScriptMain(ScriptInterface bot){
		bot.Options.SafeTimings = true;
		bot.Options.RestPackets = true;
		bot.Options.SkipCutsenes = true;
		bot.Options.PrivateRooms = true;
		
		while (!bot.ShouldExit()) {
		
			bot.Player.Join("elfhame");
			bot.Quests.EnsureAccept(4667);
			bot.Sleep(500);
			bot.Map.GetMapItem(3984);
			bot.Wait.ForQuestComplete(4667);
			bot.Quests.EnsureComplete(4667);
			bot.Sleep(500);
		}
		
	}
}
