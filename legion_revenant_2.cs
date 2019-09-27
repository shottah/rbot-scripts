using RBot;

public class Script {

	public string [] enemy = {
		"Mummy", 
		"Fishbones|Bone Terror|Dark Fire|Mutineer|Undead Pirate",
		"Undead Mage|Undead Minion",
		"Rotting Darkblood|Ghastly Darkblood",
		"Ghoul Minion",
		"Escaped Ghostly Zard|Escaped Wendighost",
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
		
		bot.Bank.ToInventory("Conquest Wreath");
		
		foreach (string i in item) {
			bot.Bank.ToInventory(i);
		}
		
		while (bot.Inventory.GetQuantity("Conquest Wreath") < 6) {
			bot.Log("Currently earned Conquest Wreath (" + bot.Inventory.GetQuantity("Conquest Wreath") + ")");
			bot.Quests.EnsureAccept(6898);
			for (int i = 0; i < 9; i++) {
				if (bot.Inventory.GetQuantity(item[i]) < q) {
					bot.Log("Hunting for " + item[i] + " (" + q + ")");
					bot.Player.Join(map[i]);
					bot.Player.HuntForItem(enemy[i], item[i], q);
					bot.Sleep(500);
					bot.Player.Jump("Enter", "Spawn");
					bot.Sleep(1500);
					bot.Log("Done.");
				}
			}
			
			
			if (bot.Inventory.GetQuantity("Battleon Cohort Conquered") < q) {
				bot.Sleep(4000);
				bot.Player.Join("doomhaven", "r16", "Left");
				bot.Player.HuntForItem("Zombie Knight|Zombie", "Battleon Cohort Conquered", q);
				bot.Player.Jump("Enter", "Spawn");
			}
			
			bot.Quests.EnsureComplete(6898);
			bot.Wait.ForDrop("Conquest Wreath");
			bot.Player.Pickup("Conquest Wreath");
		}
		
		bot.Player.Join("yulgar");
	
	}
}