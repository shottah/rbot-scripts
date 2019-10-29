/*
	Created by matabeitt
	This script gets the average health of all
	monsters in the room.
*/

using RBot;
using System;
using System.Collections.Generic;

public class Script{

	public void ScriptMain(ScriptInterface bot){
		bot.Skills.StartTimer();
		bot.Options.SafeTimings = true;
		bot.Options.RestPackets = true;
		
		string [] monsters = {
			"Sneevil", "Sneeviltron"
		};
		
		double avg = averageHealth(bot);
		bot.Log(avg.ToString());
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
}
