using RBot;

public class Script {

	public void ScriptMain(ScriptInterface bot){
		bot.Options.SafeTimings = true;
		bot.Options.RestPackets = true;
		bot.Options.PrivateRooms = true;
		bot.Options.InfiniteRange = true;
		
		bot.Skills.LoadSkills("./Skills/Generic.xml");
		bot.Skills.StartTimer();
		
		int q = 1070;
		
		bot.Player.Join("doomundead");
		
		while (!bot.ShouldExit()) {
			if (!bot.Quests.IsInProgress(q)) bot.Quests.EnsureAccept(q);
			bot.Player.HuntForItem("Light Knight", "Light Knight Lifeforce", 5, tempItem:true);
			if (!bot.Inventory.ContainsTempItem("Reached Keep"))bot.Player.Jump("Cut2", "Down");
			bot.Wait.ForQuestComplete(q, timeout:3000);
		}
	}
}
