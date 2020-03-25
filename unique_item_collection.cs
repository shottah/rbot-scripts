/*
	Created by matabeitt
	This script farms for Unique items used to get 
	Necrotic Sword of Doom.
*/

using RBot;
using System;

public class Script{
	
	public void ScriptMain(ScriptInterface bot){
	
		bot.Skills.StartTimer();
		bot.Options.SafeTimings = true;
		bot.Options.RestPackets = true;
		bot.Options.PrivateRooms = true;
		bot.Options.InfiniteRange = true;
		bot.Options.ExitCombatBeforeQuest = true;
		
		bot.Drops.RejectElse = true;
		bot.Drops.Start();
		
		// Start here.
		
		bot.Drops.Stop();
	}
}
