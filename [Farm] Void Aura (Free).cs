/*
	Created by matabeitt
	This script farms for Unique items used to get 
	Necrotic Sword of Doom.
*/

using RBot;
using System;
using System.Collections.Generic;

public class Requirement {
	
	public string Monster {get; set;}
	public string Item {get; set;}
	public int Quantity {get; set;}
	public bool IsTemp {get; set;}
	
	public Requirement (string monster, string item, int quantity, bool isTemp) {
		Monster = monster;
		Item = item;
		Quantity = quantity;
		IsTemp = isTemp;
	}
	
	public void Complete (ScriptInterface bot) {
		if (bot.Bank.Contains(Item)) bot.Bank.ToInventory(Item);
		while (bot.Inventory.GetQuantity(Item) < Quantity || bot.Inventory.GetTempQuantity(Item) < Quantity) {
			bot.Player.HuntForItem(Monster, Item, Quantity, IsTemp);
		}
		bot.Inventory.ToBank(Item);
	}
}

public class Location {
	
	public ScriptInterface Bot;
	public string Map {get; set;}
	public string Cell {get; set;}
	public string Pad {get; set;}
	public List<Requirement> Requirements {get; set;}
	
	public Location(string map, string cell, string pad) {
		Bot = null;
		Map = map;
		Cell = cell;
		Pad = pad;
		Requirements = new List<Requirement>();
	}
	
	public void Bind (ScriptInterface bot) {
		Bot = bot;
	}
	
	public void Add (Requirement req) {
		Requirements.Add(req);
	}
	
	public void Add (string monster, string item, int quantity=1, bool isTemp=true) {
		Requirements.Add(new Requirement(monster, item, quantity, isTemp));
	}
	
	public void Join () {
		Bot.Player.Join(Map, Cell, Pad);
	}
	
	public void Collect () {
		Join();
		foreach (Requirement r in Requirements) {
			r.Complete(Bot);
		}
	}

}

public class Script {
	
	public List<Requirement> QUEST_REQ;
	public List<Location> QUEST_LOC;
	
	public void ScriptMain(ScriptInterface bot){
	
		bot.Skills.StartTimer();
		bot.Options.SafeTimings = true;
		bot.Options.RestPackets = true;
		bot.Options.PrivateRooms = true;
		bot.Options.InfiniteRange = true;
		bot.Options.ExitCombatBeforeQuest = true;
		
		bot.Drops.RejectElse = true;
		
		QUEST_LOC = new List<Location>(); 
		QUEST_LOC.Add(new Location("timespace", "Frame1", "Left"));
		QUEST_LOC.Add(new Location("citadel", "m13", "Right"));
		QUEST_LOC.Add(new Location("greenguardwest", "BKWest15", "Right"));
		QUEST_LOC.Add(new Location("mudluk", "Boss", "Down"));
		QUEST_LOC.Add(new Location("aqlesson", "Frame9", "Right"));
		QUEST_LOC.Add(new Location("necrocavern", "r16", "Down"));
		QUEST_LOC.Add(new Location("hachiko", "Roof", "Left"));
		QUEST_LOC.Add(new Location("timevoid", "Frame8", "Left"));
		QUEST_LOC.Add(new Location("dragonchallenge", "r4", "Left"));
		QUEST_LOC.Add(new Location("maul", "r3", "Down"));
		
		QUEST_REQ = new List<Requirement>();
		QUEST_REQ.Add(new Requirement("Astral Ephemerite",	"Astral Ephemerite Essence", 20, isTemp:false));
		QUEST_REQ.Add(new Requirement("Belrot the Fiend",	"Belrot the Fiend Essence", 20, isTemp:false));
		QUEST_REQ.Add(new Requirement("Black Knight",		"Black Knight Essence", 20, isTemp:false));
		QUEST_REQ.Add(new Requirement("Tiger Leech ",		"Tiger Leech Essence", 20, isTemp:false));
		QUEST_REQ.Add(new Requirement("Carnax",				"Carnax Essence", 20, isTemp:false));
		QUEST_REQ.Add(new Requirement("Chaos Vordred",		"Chaos Vordred Essence", 20, isTemp:false));
		QUEST_REQ.Add(new Requirement("Dai Tengu",			"Dai Tengu Essence", 20, isTemp:false));
		QUEST_REQ.Add(new Requirement("Unending Avatar",	"Unending Avatar Essence", 20, isTemp:false));
		QUEST_REQ.Add(new Requirement("Void Dragon",		"Void Dragon Essence", 20, isTemp:false));
		QUEST_REQ.Add(new Requirement("Creature Creation",	"Creature Creation Essence", 20, isTemp:false));
		
		bot.Drops.Add("Void Aura");
		
		
		for (int i = 0; i < QUEST_LOC.Count; i++) {
			QUEST_LOC[i].Add(QUEST_REQ[i]);			// Add the Requirements to the Location
			bot.Drops.Add(QUEST_REQ[i].Monster);	// Add the Requirements to the Whitelist
			QUEST_LOC[i].Bind(bot);					// Bind the script interface to the utility class
			QUEST_LOC[i].Collect();					// Activate a farming sequence for each Location
		}
		
		
	}
}