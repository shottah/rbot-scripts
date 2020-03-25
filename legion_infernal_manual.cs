using RBot;
using System.Collections.Generic;

public class Script {

	public void ScriptMain(ScriptInterface bot){
		bot.Options.SafeTimings = true;
		bot.Options.RestPackets = true;
		bot.Options.PrivateRooms = true;
		bot.Options.InfiniteRange = true;
		bot.Options.SkipCutsenes = true;
		bot.Options.ExitCombatBeforeQuest = true;
		
		bot.Skills.StartTimer();
		
		bot.Skills.Add(1, 2f);
		bot.Skills.Add(2, 2f);
		bot.Skills.Add(3, 2f);
		bot.Skills.Add(4, 2f);
		
		bot.Drops.Add("Dage's Favor");
		bot.Drops.Add("Legion Token");
		bot.Drops.RejectElse = true;
		bot.Drops.Start();
		
		bot.Bank.ToInventory("Legion Token");
		bot.Bank.ToInventory("Infernal Caladbolg");
		
		if (!bot.Inventory.Contains("Infernal Caladbolg")) bot.Exit();
		
		while (!bot.ShouldExit()) {
			bot.Quests.EnsureAccept(3722);
			
			manualHunt(bot, "fotia", "Fotia Elemental", "Betrayer Extinguished", 5, isTemp:true);
			
			if (bot.Player.InCombat) bot.Player.Jump(bot.Player.Cell, bot.Player.Pad);
			
			bot.Sleep(2000);
			
			manualHunt(bot, "evilwardage", "Dreadfiend of Nulgath", "Fiend Felled", 2, isTemp:true);
			
			if (bot.Player.InCombat) bot.Player.Jump(bot.Player.Cell, bot.Player.Pad);
			
			bot.Quests.EnsureComplete(3722);
			
			bot.Sleep(2000);
		}
		
		bot.Drops.Stop();
	}
	
	public void manualHunt(ScriptInterface bot, string map, string enemy, string item, int quantity, bool isTemp=false) {
		if (bot.Map.Name != map) bot.Player.Join(map);
		List<string> l = null;
		while (!(bot.Inventory.ContainsTempItem(item, quantity) || bot.Inventory.Contains(item, quantity))) {
			l = bot.Monsters.GetLivingMonsterCells(enemy);
			if (l.Count > 0) {
				bot.Player.Jump(l.ToArray()[0], "Right");
				bot.Player.Kill (enemy);
			}
		}
		return;
	}
}
