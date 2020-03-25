using RBot;

public class Script {

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
			bot.Quests.EnsureAccept(3722);
			
			bot.Player.Join("fotia");
			bot.Player.HuntForItem("Fotia Elemental", "Betrayer Extinguished", 5, tempItem:true);
			bot.Player.Jump("Enter", "Spawn");
			
			bot.Player.Join("evilwardage");
			bot.Player.HuntForItem("Dreadfiend of Nulgath", "Fiend Felled", 2, tempItem:true);
			
			bot.Quests.EnsureComplete(3722);
			bot.Wait.ForDrop("Legion Token");
			bot.Player.Pickup("Legion Token");
			
			bot.Sleep(1200);
		}
	}
}
