using RBot;

public class Script {

	public void ScriptMain(ScriptInterface bot){
		bot.Options.SafeTimings = true;
		bot.Options.RestPackets = true;
		bot.Options.ExitCombatBeforeQuest = true;
		
		bot.Skills.LoadSkills("/Skills/Generic");
		bot.Skills.StartTimer();
		
		while (!bot.ShouldExit() && bot.Inventory.GetQuantity("Blood Moon Token") < 300) {
			bot.Quests.EnsureAccept(6059);
			if (bot.Map.Name != "bloodmoon") bot.Player.Join("bloodmoon");
			bot.Player.Jump("r4a", "Left");
			bot.Options.AggroMonsters = true;
			bot.Player.KillForItem("Lycan Guard", "Moon Stone", 20);
			bot.Options.AggroMonsters = false;
			bot.Player.WalkTo(8, 272);
			bot.Player.Jump("r12a", "Left");
			bot.Player.KillForItem("Black Unicorn", "Black Blood Vial", 16);
			bot.Player.WalkTo(8, 274);
			while (bot.Quests.CanComplete(6059)) {
				bot.Quests.EnsureComplete(6059);
				bot.Quests.EnsureAccept(6059);
				bot.Sleep(1200);
			}
			bot.Player.Pickup("Blood Moon Token");
		}
	}
}
