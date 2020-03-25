using RBot;

public class Script {

	public void ScriptMain(ScriptInterface bot){
		bot.Options.SafeTimings = true;
		bot.Options.RestPackets = true;
		bot.Options.ExitCombatBeforeQuest = true;
		
		bot.Skills.LoadSkills("/Skills/Generic");
		bot.Skills.StartTimer();
		
		if (bot.Map.Name != "legionarena") bot.Player.Join("legionarena", "Boss", "Left");
		bot.Bank.ToInventory("Legion Token");
		while (!bot.ShouldExit()) {
			bot.Quests.EnsureAccept(6743);
			if (bot.Player.Cell != "Boss") bot.Player.Jump("Boss", "Left");
			if (!bot.Quests.CanComplete(6743) && bot.Monsters.Exists("Legion Fiend Rider")) bot.Player.Kill("Legion Fiend Rider");
			if (bot.Quests.CanComplete(6743)) {
				bot.Player.WalkTo(14, 402);
				bot.Quests.EnsureComplete(6743);
			}
			bot.Player.Pickup("Legion Token");
		}
	}
}
