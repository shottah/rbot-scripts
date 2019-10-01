using RBot;

public class Script {

	public  int [] QUEST = {4510, 4511, 4512};
	public  string [] ITEM = {"Trapped Spirits", "Energy of Death", "Captured Time"};
	public  string [] ENEMY = {"Fallen Knight", "Underworld Hound", "Infernal Imp"};
	public  string [] REWARD = {"Guardian of Spirits' Blade", "Avatar of Death's Scythe", "Lance of Time"};
	
	public void ScriptMain(ScriptInterface bot){
		bot.Options.SafeTimings = true;
		bot.Options.RestPackets = true;
		bot.Options.PrivateRooms = true;
		bot.Options.InfiniteRange = true;
		bot.Options.ExitCombatBeforeQuest = true;
		
		bot.Skills.StartTimer();
		
		bot.Skills.Add(1, 2f);
		bot.Skills.Add(2, 2f);
		bot.Skills.Add(3, 2f);
		bot.Skills.Add(4, 2f);
		
		bot.Player.LoadBank();
		bot.Inventory.BankAllCoinItems();
		
		foreach (string r in REWARD) {
			if (bot.Bank.Contains(r)) bot.Bank.ToInventory(r);
		}
		
		bot.Player.Join("lostruins");
		
		while (!bot.ShouldExit()) {
			foreach (int q in QUEST) {
				bot.Quests.EnsureAccept(q);
			}
			while (!isFinished(bot)) {
				for (int i = 0; i < 3; i++) {
					if (bot.Inventory.GetQuantity(ITEM[i]) < 500) {	
						bot.Player.Hunt(ENEMY[i]);
					}
				}
			}
			foreach (int q in QUEST) {
				if (bot.Quests.CanComplete(q)) bot.Quests.EnsureComplete(q);
			}
		}
		bot.Exit();
	}
	
	public bool isFinished (ScriptInterface bot) {
		for (int i = 0; i < 3; i++) {
			if (bot.Inventory.GetQuantity(ITEM[i]) < 500) return false;
		}
		return true;
	}
}
