using RBot;

public class Script {

	private static int Q_ID = 3722;
	private static string [] Q_ITEMS = {"Betrayer Extinguished", "Fiend Felled"};
	private static string [] Q_ENEMIES = {"Fotia Elemental", "Dreadfiend of Nulgath"};
	private static int [] Q_AMTS = {5, 2};
	private static string [] Q_MAPS = {"Fotia", "Underworld"};

	public void ScriptMain(ScriptInterface bot){
		bot.Options.SafeTimings = true;
		bot.Options.RestPackets = true;
		bot.Options.PrivateRooms = true;
		bot.Options.InfiniteRange = true;
		bot.Options.SkipCutsenes = true;
		bot.Options.ExitCombatBeforeQuest = true;
		
		bot.Skills.StartTimer();
		
		bot.Skills.Add(1, 2f);
		bot.Skills.Add(2, 2f);
		bot.Skills.Add(3, 2f);
		bot.Skills.Add(4, 2f);
		
		bot.Bank.ToInventory("Legion Token");
		bot.Bank.ToInventory("Infernal Caladbolg");
		
		if (!bot.Inventory.Contains("Infernal Caladbolg")) bot.Exit();
		
		while (!bot.ShouldExit()) {
			bot.Quests.EnsureAccept(Q_ID);
			for (int i = 0; i < 2; i++) {
				bot.Player.Join(Q_MAPS[i]);
				bot.Player.HuntForItem(Q_ENEMIES[i], Q_ITEMS[i], Q_AMTS[i], tempItem:true);
				bot.Player.Jump("Enter", "Spawn");
			}
			bot.Quests.EnsureComplete(Q_ID);
			bot.Player.Pickup("Legion Token");
		}
	}
}
