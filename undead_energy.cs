using RBot;

public class Script {

	private string item = "Undead Energy";
	private int quantity = 1800;

	public void ScriptMain(ScriptInterface bot){
		bot.Options.SafeTimings = true;
		bot.Options.RestPackets = true;

		bot.Skills.StartTimer();
		
		bot.Skills.Add(1, 2f);
		bot.Skills.Add(2, 2f);
		bot.Skills.Add(3, 2f);
		bot.Skills.Add(4, 2f);
		
		if (bot.Map.Name != "battleunderb") bot.Player.Join("battleunderb-9999");
		
		bot.Options.AggroMonsters = true;
		while (!bot.ShouldExit()) {
			bot.Player.KillForItem("*", item, quantity);
		}
		bot.Options.AggroMonsters = false;
	}
}
