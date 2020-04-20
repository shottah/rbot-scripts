using RBot;

public class Script {

	public void ScriptMain(ScriptInterface bot){
		bot.Options.SafeTimings = true;
		bot.Options.RestPackets = true;
		bot.Options.PrivateRooms = true;
		
		bot.Skills.StartSkills("Skills/Generic.xml");
		bot.Player.LoadBank();
		
		EggshellRoutine(bot, "Wolf", "greenguardeast", "Chocolate Eggshells", 15);
		EggshellRoutine(bot, "Frogzard", "greenguardwest", "Caramel Eggshells", 15);
		EggshellRoutine(bot, "Grenwog", "grenwog", "Chocolate Eggshells", 15);
		
		bot.Player.Join("yulgar");
		bot.Exit();
	}
	
	public void EggshellRoutine(ScriptInterface bot, string target, string map, string flavor, int quantity=150) {
		if (bot.Bank.Contains(flavor)) bot.Bank.ToInventory(flavor);
		if (bot.Inventory.Contains(flavor, quantity)) return;
		if (bot.Map.Name != map) bot.Player.Join(map);
		bot.Player.HuntForItem(target, flavor, quantity);
	}
}
