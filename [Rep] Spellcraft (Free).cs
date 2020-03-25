using RBot;

public class Script {

	public void ScriptMain(ScriptInterface bot){
		bot.Options.SafeTimings = true;
		bot.Options.RestPackets = true;
		bot.Options.PrivateRooms = true;
		bot.Options.InfiniteRange = true;
		bot.Options.ExitCombatBeforeQuest = true;
		
		bot.Skills.LoadSkills("./Skills/Generic.xml");
		bot.Skills.StartTimer();
		
		bot.Player.LoadBank();
		bot.Bank.ToInventory("Mystic Parchment");
		bot.Drops.Add("Mystic Parchment");
		bot.Drops.Start();
		
		while (!bot.ShouldExit()) {
			/*
			while (bot.Inventory.GetQuantity("Mystic Quills") < 10) {
				bot.Player.Join("mobius");
				bot.Player.Hunt("Slugfit");
			}
			*/
			
			bot.Player.Join("underworld");
			while (bot.Inventory.GetQuantity("Mystic Parchment") < 10) {
				bot.Player.Hunt("Skull Warrior|Undead Infantry");
			}
			
			bot.Sleep(1500);
			
			bot.Player.Join("spellcraft");
			
			while (bot.Inventory.GetQuantity("Mystic Parchment") > 0 &&
			bot.Inventory.GetQuantity("Hallow Ink") < 50) {
				bot.Shops.BuyItem("Hallow Ink");
				bot.Sleep(800);
			}
			
			while (bot.Inventory.GetQuantity("Hallow Ink") > 0) {
				bot.SendPacket("%xt%zm%crafting%1%spellComplete%6%2325%Holy Flare%");
				bot.Sleep(800);
			}
		}
	}
}
