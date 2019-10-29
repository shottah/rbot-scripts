/*
	Created by matabeitt
	This script gets the average health of all
	monsters in the room.
*/

using RBot;
using System;
using System.Collections.Generic;

public class Script{

	public string SOLO_CLASS = "Void Highlord";
	public string FARM_CLASS = "Shaman";

	public void ScriptMain(ScriptInterface bot){
		bot.Skills.StartTimer();
		bot.Options.SafeTimings = true;
		bot.Options.RestPackets = true;
		
		string [] monsters = {
			"Sneevil", "Sneeviltron"
		};
		
		double avg = averageHealth(bot);
		bot.Log(avg.ToString());
		combatClassHandler(bot, avg);
		bot.Exit();
	}
	
	public double averageHealth(ScriptInterface bot, string [] monsters=null) {
		// Get all monsters in all cells.
		Dictionary<string, List<Monster>> d = bot.Monsters.GetCellMonsters();
		
		double average = 0;
		int count = 0;
		
		// For each ROOM/Monster[] Entry
		foreach(KeyValuePair<string, List<Monster>> entry in d) {
			// For each Monster in Monsters[]
			bot.Log("Current Room");
			bot.Log(entry.ToString());
			foreach (Monster m in entry.Value) {
				bot.Log("Checking Monster ...");
				bot.Log(m.Name);
				// If there are no target monsters
				if (monsters == null ) {
					average += m.HP;
					count += 1;
				}
				// If the Monster is a target monster
				else (monsters.Contains(m.Name)) {
					bot.Log("Monster valid");
					// Tally HP
					average += m.HP;
					count += 1;
					bot.Log("Total HP:" + average);
				}
			}
		}
		return average/count;
	}
	
	public void combatClassHandler(ScriptInterface bot, double health) {
		if (!(bot.Inventory.Contains(FARM_CLASS) && bot.Bank.Contains(FARM_CLASS))) return;
		if (!(bot.Inventory.Contains(SOLO_CLASS) && bot.Bank.Contains(SOLO_CLASS))) return;
		
		bot.Skills.StopTimer();
		bot.Skills.Clear();
		
		string nextClass = "";
		if (health > 6000) {
			nextClass = SOLO_CLASS;
		} else {
			nextClass = FARM_CLASS;
		}
		
		bot.Player.EquipItem(nextClass);
		bot.Skills.LoadSkills("./Skills/" + nextClass.Replace(" ", "") + ".xml");
		bot.Skills.StartTimer();
	}
	
	
}
