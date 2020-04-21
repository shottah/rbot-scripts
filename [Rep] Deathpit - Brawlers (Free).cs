using RBot;

public class Script {

	public void ScriptMain(ScriptInterface bot){
		bot.Options.SafeTimings = true;
		bot.Options.RestPackets = true;
		bot.Options.ExitCombatBeforeQuest = true;
		
		if (bot.Skills.OverrideSkills == null) bot.Skills.StartSkills("Skills/Generic.xml");
		
		int QUEST = 5144;
		
		if (bot.Map.Name != "deathpit") bot.Player.Join("deathpit");
		
		while (!bot.ShouldExit()) {
			bot.Quests.EnsureAccept(QUEST);
			bot.Player.HuntForItem("Drakel Brawler", "Drakel Brawler Defeated", 3, tempItem:true);
			bot.Quests.EnsureComplete(QUEST);
		}
	}
}
