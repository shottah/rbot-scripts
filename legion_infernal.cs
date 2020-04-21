using RBot;

public class Script {

	public void ScriptMain(ScriptInterface bot){
		bot.Options.SafeTimings = true;
		bot.Options.RestPackets = true;
		bot.Options.PrivateRooms = true;
		bot.Options.Magnetise = true;
		bot.Options.ExitCombatBeforeQuest = true;
		
		if (bot.Skills.OverrideSkills == null) bot.Skills.StartSkills("Skills/Generic.xml");
		else bot.Skills.StartTimer();
		
		bot.Player.LoadBank();

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
			bot.Player.Pickup("Legion Token");
			
			bot.Sleep(1200);
		}
	}
}
