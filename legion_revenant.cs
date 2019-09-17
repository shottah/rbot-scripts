using RBot;

public class Script {

	public void ScriptMain(ScriptInterface bot){
		bot.Skills.StartTimer();
		bot.Options.SafeTimings = true;
		bot.Options.RestPackets = true;
		
		bot.Skills.Add(1, 2f);
		bot.Skills.Add(2, 2f);
		bot.Skills.Add(3, 2f);
		bot.Skills.Add(4, 2f);
		
		bot.Log("Starting Legion Revenant's Spellscroll bot.");
		
		bot.Player.LoadBank();
		bot.Inventory.BankAllCoinItems();
		bot.Bank.ToInventory("Revenant's Spellscroll");
		
		if (bot.Inventory.GetQuantity("Revenant's Spellscroll") < 20) {
			bot.Log(bot.Player.Username + " has " + bot.Inventory.GetQuantity("Revenant's Spellscroll") + " Revenant Spellscroll.");
			bot.Log("Accepting the quest for Revenant's Spellscroll: Legion Fealty 1");
			bot.Quests.EnsureAccept(6897);
			SpellscrollRoutine(bot);
			bot.Log("Completing the quest for Revenant's Spellscroll: Legion Fealty 1");
			bot.Quests.EnsureComplete(6897);
			bot.Log("Picking up Revenant's Spellscroll");
			bot.Wait.ForDrop("Revenant's Spellscroll");
			bot.Player.Pickup("Revenant's Spellscroll");
		}
		bot.Inventory.ToBank("Revenant's Spellscroll");
		bot.Log("Legion Revenant's Spellscroll bot is finished.");
	}
	
	private double getEffectiveHeath (ScriptInterface bot) {
		return (double) bot.Player.Health / bot.Player.MaxHealth;
	}
	
	private void JoinGlitched (ScriptInterface bot, string map) {
		for (int i = 0; i < 10; i++) {
			if (bot.Map.Name != map) bot.Player.Join(map + "--9999");
			else break;
			bot.Sleep(2000);
		}
		
		if (bot.Map.Name != map) bot.Player.Join(map);
	}
	
	private void FarmRoutine (ScriptInterface bot, string map, string cell, string pad, string enemy, string item, int quantity) {
		bot.Log("Beginning the routine to farm for the " + item + " (" + quantity + ").");
		
		bot.Bank.ToInventory(item);
		while (bot.Inventory.GetQuantity(item) < quantity) {
			if (getEffectiveHeath(bot) < 0.7) {
				bot.Log("Health is low... resting.");
				bot.Player.Rest(full:true);
				bot.Wait.ForFullyRested();
			}
			
			if (bot.Player.Cell != "r22") bot.Player.Jump("r22", "Down");
			
			else bot.Player.Kill(enemy);
			
			if (bot.Player.DropExists(item)) {
				bot.Player.Pickup(item);
				bot.Player.RejectExcept(item);
			}
			
			if (bot.Inventory.GetQuantity(item) % 10 == 0 && bot.Inventory.GetQuantity(item) > 0) {
				bot.Log("Obtained: " + bot.Inventory.GetQuantity(item));
			}
		}
		bot.Inventory.ToBank(item);
		
		bot.Log("Ending the routine to farm for the " + item + "x" + quantity);
	}
	
	private void SpellscrollRoutine (ScriptInterface bot) {
		if (bot.Map.Name != "revenant") bot.Player.Join("revenant");
		FarmRoutine(bot, "revenant", "r2", "Left", "Ultra Aeacus", "Aeacus Empowered", 50);

		if (bot.Map.Name != "necrodungeon") JoinGlitched(bot, "necrodungeon");
		FarmRoutine(bot, "necrodungeon", "r22", "Down", "5 Headed Dracolich", "Dracolich Contract", 1000);
		
		if (bot.Map.Name != "revenant") bot.Player.Join("revenant");
		FarmRoutine(bot, "revenant", "r3", "Left", "Forgotten Soul", "Tethered Soul", 300);
		
		if (bot.Map.Name != "revenant") bot.Player.Join("shadowrealm");
		FarmRoutine(bot, "shadowrealm", "Enter", "Spawn", "*", "Darkened Essence", 500);
		
	}
}
