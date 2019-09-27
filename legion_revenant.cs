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
		
		bot.Bank.ToInventory("Infinite Legion Dark Caster");
		
		while (bot.Inventory.GetQuantity("Revenant's Spellscroll") < 20) {
			
			bot.Log(bot.Player.Username + " has " + bot.Inventory.GetQuantity("Revenant's Spellscroll") + " Revenant Spellscroll.");
			bot.Log("Accepting the quest for Revenant's Spellscroll: Legion Fealty 1");
			
			bot.Quests.EnsureAccept(6897);
			
			SpellscrollRoutine(bot);
			
			ToInventoryRoutine(bot);
			
			bot.Log("Completing the quest for Revenant's Spellscroll: Legion Fealty 1");
			
			bot.Quests.EnsureComplete(6897);
			
			bot.Log("Picking up Revenant's Spellscroll");
			
			bot.Wait.ForDrop("Revenant's Spellscroll");
			bot.Player.Pickup("Revenant's Spellscroll");
			
			ToBankRoutine(bot);
		}
		
		bot.Inventory.ToBank("Revenant's Spellscroll");
		bot.Log("Legion Revenant's Spellscroll bot is finished.");
	}
	
	private void ToBankRoutine (ScriptInterface bot) {
		bot.Log("Sending quest items to bank to conserve space");
		bot.Inventory.ToBank("Aeacus Empowered");
		bot.Inventory.ToBank("Tethered Soul");
		bot.Inventory.ToBank("Darkened Essence");
		bot.Inventory.ToBank("Dracolich Contract");
	}
	
	private void ToInventoryRoutine (ScriptInterface bot) {
		bot.Log("Sending quest items to inventory for quest completion...");
		bot.Bank.ToInventory("Aeacus Empowered");
		bot.Bank.ToInventory("Tethered Soul");
		bot.Bank.ToInventory("Darkened Essence");
		bot.Bank.ToInventory("Dracolich Contract");
	}
	
	private double getEffectiveHealth (ScriptInterface bot) {
		return (double) bot.Player.Health / bot.Player.MaxHealth;
	}
	
	private bool shouldRest (ScriptInterface bot) {
		return getEffectiveHealth(bot) < 0.7 && getEffectiveHealth(bot) > 0 ? true: false;
	}
	
	private void JoinGlitched (ScriptInterface bot, string map, string cell, string pad) {
		for (int i = 9; i > 0; i--) {
			if (bot.Map.Name != map) bot.Player.Join(map + "--999" + i, cell, pad);
			else break;
			bot.Sleep(2000);
		}
		bot.Sleep(2000);
		
		if (bot.Map.Name != map) bot.Player.Join(map, cell, pad);
		else if (bot.Map.PlayerCount <= 1) bot.Player.Join(map, cell, pad);
	}
	
	private void FarmRoutine (ScriptInterface bot, string map, string cell, string pad, string enemy, string item, int quantity) {
		
		bot.Log("Beginning the routine to farm for the " + item + " (" + quantity + ").");
		
		bot.Bank.ToInventory(item);
		
		if (bot.Inventory.GetQuantity(item) < quantity) JoinGlitched(bot, map, cell, pad);
		
		while (bot.Inventory.GetQuantity(item) < quantity) {
			if (shouldRest(bot)) {
				bot.Player.Jump(cell, pad);
				bot.Log("Health is low... resting.");
				bot.Player.Rest(full:true);
				bot.Wait.ForFullyRested();
			}
			
			if (bot.Player.Cell != cell) bot.Player.Jump(cell, pad);
			
			else bot.Player.Kill(enemy);
			
			if (bot.Player.DropExists(item)) {
				bot.Player.Pickup(item);
				bot.Player.RejectExcept(item);
			}
			
			if (bot.Inventory.GetQuantity(item) % 10 == 0 && bot.Inventory.GetQuantity(item) > 0) {
				bot.Log("Obtained: " + bot.Inventory.GetQuantity(item) + " " + item);
			}
		}
		
		bot.Options.AggroMonsters = false;
		
		bot.Player.Jump("Enter", "Spawn");
		
		bot.Inventory.ToBank(item);
		
		bot.Log("Ending the routine to farm for the " + item + " (" + quantity +")");
	}
	
	private void SpellscrollRoutine (ScriptInterface bot) {
		
		//if (bot.Map.Name != "revenant") JoinGlitched(bot, "revenant");
		FarmRoutine(bot, "revenant", "r2", "Left", "Forgotten Soul", "Tethered Soul", 300);
	
		//if (bot.Map.Name != "judgement") JoinGlitched(bot, "judgement");
		FarmRoutine(bot, "judgement", "r10a", "Left", "Ultra Aeacus", "Aeacus Empowered", 50);
		
		//if (bot.Map.Name != "shadowrealm") JoinGlitched(bot, "shadowrealm");
		FarmRoutine(bot, "shadowrealm", "Enter", "Spawn", "*", "Darkened Essence", 500);
		
		//if (bot.Map.Name != "necrodungeon") JoinGlitched(bot, "necrodungeon");
		bot.Options.AggroMonsters = true;
		FarmRoutine(bot, "necrodungeon", "r22", "Down", "5 Headed Dracolich", "Dracolich Contract", 1000);
	}
}
