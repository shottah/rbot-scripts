using RBot;

public class Script {

	public string [] enemy = {
		"Mummy", 
		"Fishbones|Bone Terror|Dark Fire|Mutineer|Undead Pirate",
		"Undead Mage|Undead Minion",
		"Rotting Darkblood|Ghastly Darkblood",
		"Ghoul Minion",
		"Escaped Ghostly Zardman|Escaped Wendighost",
		"Bone Dragonling|Dark Fire",
		"Doomwood Bonemuncher|Doomwood Ectomancer|Doomwood Treeant",
		"Grim Soldier|Grim Fighter|Grim Fire Mage"
	};

	public string [] map = {
		"mummies", "wrath", "brightfall", "deathpits", "maxius", "curseshore",
		"dragonbone", "doomwood", "doomvault"
	};
	
	public string [] item = {
		"Ancient Cohort Conquered", "Pirate Cohort Conquered",
		"Mirror Cohort Conquered", "Darkblood Cohort Conquered", "Vampire Cohort Conquered",
		"Spirit Cohort Conquered", "Dragon Cohort Conquered", "Doomwood Cohort Conquered",
		"Grim Cohort Conquered"
	};
	
	public int q = 500;
	
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
		
		bot.Bank.ToInventory("Conquest Wreath");
		
		foreach (string i in item) {
			bot.Bank.ToInventory(i);
		}
		// Ignore the next two lines if you only want to use one class
		bot.Bank.ToInventory("Lightcaster"); // Put your soloing class here 
		bot.Bank.ToInventory("Infinite Legion Dark Caster"); // Put farming class here
		bot.Bank.ToInventory("Battleon Cohort Conquered"); 
		
		while (bot.Inventory.GetQuantity("Conquest Wreath") < 6) {
			bot.Log("Currently earned Conquest Wreath (" + bot.Inventory.GetQuantity("Conquest Wreath") + ")");
			bot.Quests.EnsureAccept(6898);
			for (int i = 0; i < 9; i++) {
				if (bot.Inventory.GetQuantity(item[i]) < q) {
					// Getting this item (500) part of the quest
					// and join the appropriate map
					bot.Log("Hunting for " + item[i] + " (" + q + ")");
					bot.Player.Join(map[i]);
					
					// Do a health check on monsters to modify farming style
					if (avgHealth(bot) > 6000) bot.Player.EquipItem("LightCaster"); // Solo class for strong units
					else bot.Player.EquipItem("Infinite Legion Dark Caster");		// Farming class for weak units
					
					// Hunt for the enemy(s) in the map
					bot.Player.HuntForItem(enemy[i], item[i], q);
					
					// Exit combat to prepare for next step
					bot.Player.Jump("Enter", "Spawn");
					bot.Sleep(1200);
					bot.Log("Done.");
				}
			}
			
			// Explicit routie for DoomHaven (I was getting halting issues 
			// when this was part of main procedure above.
			if (bot.Inventory.GetQuantity("Battleon Cohort Conquered") < q) {
				bot.Player.Join("doomhaven", "r16", "Left");
				bot.Player.HuntForItem("Zombie Knight|Zombie", "Battleon Cohort Conquered", q);
				bot.Player.Jump("Enter", "Spawn");
				bot.Sleep(1200);
			}
			
			bot.Quests.EnsureComplete(6898);
			bot.Wait.ForDrop("Conquest Wreath");
			bot.Player.Pickup("Conquest Wreath");
		}
		
		bot.Player.Join("yulgar");
	
	}
	
	public double avgHealth (ScriptInterface bot) {
		double avg = 0;
		int count = bot.Monsters.CurrentMonsters.Count;
		foreach (Monster m in bot.Monsters.CurrentMonsters) {
			avg = m.HP;
		}
		avg /= count;
		bot.Log(avg.ToString());
		return avg;
	}
}
