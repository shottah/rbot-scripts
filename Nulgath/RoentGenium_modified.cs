using RBot;

public class Script {

	//Required inventory space: 26
	//Requires ownership of Voucher of Nulgath (non-mem), Nation Round 4 Medal
	public void ScriptMain(ScriptInterface bot){
		bot.Options.SafeTimings = true;
		bot.Options.RestPackets = true;
		bot.Options.ExitCombatBeforeQuest = true;
		
		bot.Skills.StartTimer();
		
		bot.Player.LoadBank();
		bot.Inventory.BankAllCoinItems();
		bot.Bank.ToInventory("Hadean Onyx of Nulgath");
		bot.Bank.ToInventory("Voucher of Nulgath (non-mem)");
		bot.Bank.ToInventory("Black Knight Orb");
		bot.Bank.ToInventory("Dwakel Decoder");
		bot.Bank.ToInventory("Nulgath Shaped Chocolate");
		bot.Bank.ToInventory("The Secret 1");
		bot.Bank.ToInventory("Elders' Blood");
		bot.Bank.ToInventory("Aelita's Emerald");
		bot.Bank.ToInventory("Unidentified 13");
		bot.Bank.ToInventory("Elemental Ink");
		bot.Bank.ToInventory("Gem of Nulgath");
		bot.Bank.ToInventory("Bone Dust");
		bot.Bank.ToInventory("Emblem of Nulgath");
		bot.Bank.ToInventory("Essence of Nulgath");
		bot.Bank.ToInventory("Tainted Gem");
		bot.Bank.ToInventory("Nulgath's Approval");
		bot.Bank.ToInventory("Archfiend's Favor");
		bot.Bank.ToInventory("Diamond of Nulgath");
		bot.Bank.ToInventory("Tainted Gem");
		bot.Bank.ToInventory("Totem of Nulgath");
		bot.Bank.ToInventory("Dark Crystal Shard");
		bot.Bank.ToInventory("Fiend Seal");
		bot.Bank.ToInventory("Nation Round 4 Medal");
		bot.Bank.ToInventory("Gem of Domination");
		bot.Bank.ToInventory("Mystic Quills");
		bot.Bank.ToInventory("Roentgenium of Nulgath");
		
		while(!bot.Inventory.Contains("Roentgenium of Nulgath", 15)){
			bot.Quests.EnsureAccept(5660);
		
			if(!bot.Inventory.Contains("Black Knight Orb")){
				bot.Quests.EnsureAccept(318);
			
				bot.Player.Join("well");
				bot.Player.HuntForItem("Gell Oh No", "Black Knight Leg Piece", 1, true);
				
				bot.Player.Join("greendragon");
				bot.Player.HuntForItem("Greenguard Dragon", "Black Knight Chest Piece", 1, true);
				
				bot.Player.Join("deathgazer");
				bot.Player.HuntForItem("Deathgazer", "Black Knight Shoulder Piece", 1, true);
				
				bot.Player.Join("trunk");
				bot.Player.KillForItem("Greenguard Basilisk", "Black Knight Arm Piece", 1, true);
				
				bot.Quests.EnsureComplete(318);
				
				bot.Wait.ForDrop("Black Knight Orb");
				bot.Player.Pickup("Black Knight Orb");
			}
			
			if(!bot.Inventory.Contains("Dwakel Decoder")){
				bot.Player.Join("crashsite", "Farm2", "Right");
				bot.Map.GetMapItem(106);
				bot.Wait.ForDrop("Dwakel Decoder");
				bot.Player.Pickup("Dwakel Decoder");
			}
			
			if(!bot.Inventory.Contains("The Secret 1")){
				bot.Player.Join("willowcreek");
				bot.Quests.EnsureAccept(623);
				bot.Player.HuntForItem("Hidden Spy", "The Secret 1", 1);
				bot.Player.Jump("Enter", "Spawn");
			}
			
			if(!bot.Inventory.Contains("Aelita's Emerald")){
				bot.Player.Join("yulgar");
				bot.Shops.Load(16);
				bot.Sleep(10000);
				bot.Shops.BuyItem(16, "Aelita's Emerald");
			}
			
			while(!bot.Inventory.Contains("Unidentified 13"))
				NulgathLarvaeFarm(bot);
				
			if(!bot.Inventory.Contains("Elemental Ink", 10)){
				if(!bot.Inventory.Contains("Mystic Quills", 4)){
					bot.Player.Join("mobius");
					bot.Player.HuntForItem("Slugfit", "Mystic Quills", 4);
					bot.Player.Jump("Enter", "Spawn");
				}
				
				bot.Player.Join("spellcraft");
		
				bot.SendPacket("%xt%zm%buyItem%671975%13284%549%1637%");
		        bot.Sleep(2500);
		        bot.SendPacket("%xt%zm%buyItem%671975%13284%549%1637%");
		        bot.Sleep(2500);
			}
			
			while(!bot.Inventory.Contains("Gem of Nulgath", 20)){
				if(bot.Map.Name != "tercessuinotlim"){
			        bot.Player.Join("citadel", "m22", "Right");
		            bot.Player.Join("tercessuinotlim", "Enter", "Spawn");
	            }
	            
	            bot.Quests.EnsureAccept(4778);
	            
	            bot.Player.HuntForItem("Dark Makai", "Essence of Nulgath", 60);
	            
	            bot.Quests.EnsureComplete(4778, 6136);
	            
	            bot.Wait.ForDrop("Gem of Nulgath");
	            bot.Player.Pickup("Gem of Nulgath");
			}
			
			if(!bot.Inventory.Contains("Essence of Nulgath", 50)){
				if(bot.Map.Name != "tercessuinotlim"){
			        bot.Player.Join("citadel", "m22", "Right");
		            bot.Player.Join("tercessuinotlim", "Enter", "Spawn");
	            }
		            
	            bot.Player.HuntForItem("Dark Makai", "Essence of Nulgath", 50);
			}
			
			while(!bot.Inventory.Contains("Emblem of Nulgath", 20)){
				if(bot.Map.Name != "shadowblast")
					bot.Player.Join("shadowblast");
				
				bot.Quests.EnsureAccept(4748);
				
				bot.Player.HuntForItems("Legion Fenrir|Legion Cannon|Legion Airstrike|Paragon|DoomBringer|DoomKnight Prime|Draconic DoomKnight|Shadow Destroyer|Shadowrise Guard", new string[] { "Gem of Domination", "Fiend Seal", "Archfiend's Favor", "Nulgath's Approval" }, new int[] { 1, 25, 1, 1 });
				
				bot.Quests.EnsureComplete(4748);
				bot.Wait.ForDrop("Emblem of Nulgath");
				bot.Player.Pickup("Emblem of Nulgath");
			}
			
			while(!bot.Inventory.Contains("Tainted Gem", 100)){
				if(bot.Map.Name != "battleunderb")
					bot.Player.Join("battleunderb");
					
				bot.Quests.EnsureAccept(568);
				
				bot.Player.HuntForItem("Skeleton Warrior|Skeleton Fighter", "Bone Dust", 32, false, false);
				
				if(!bot.Quests.EnsureComplete(568, -1, false, 5)){
					bot.Wait.ForDrop("Tainted Gem");
					bot.Player.Pickup("Tainted Gem");
					bot.Player.RejectExcept("Tainted Gem");
				}
			}
			
			if(!bot.Inventory.Contains("Nulgath's Approval", 300) || !bot.Inventory.Contains("Archfiend's Favor", 300)){
				bot.Player.Join("evilwarnul");
				
				bot.Player.HuntForItems("Legion Fenrir|Blade Master|Skull Warrior|Undead Bruiser|Undead Infantry|Undead Legend", new string[] { "Archfiend's Favor", "Nulgath's Approval" }, new int[] { 300, 300 });
			}
			
			if(!bot.Inventory.Contains("Elder's Blood")){
				while(bot.Quests.IsDailyComplete(802)){
					NulgathLarvaeFarm(bot);
				}
					
				bot.Quests.EnsureAccept(802);
				
				bot.Player.Join("arcangrove");
				bot.Player.HuntForItem("Gorillaphant", "Slain Gorillaphant", 50, true);
				
				bot.Quests.EnsureComplete(802);
				
				bot.Wait.ForDrop("Elder's Blood");
				bot.Player.Pickup("Elder's Blood");
			}
			
			if(!bot.Inventory.Contains("Nulgath Shaped Chocolate")){
				while(bot.Player.Gold < 2000000){
					bot.Quests.EnsureAccept(236);
					bot.Player.Join("greenguardwest");
					bot.Player.HuntForItem("Big Bad Boar", "Were Egg", 1, true);
					bot.Quests.EnsureComplete(236);
					bot.Wait.ForDrop("Berserker Bunny");
					bot.Player.Pickup("Berserker Bunny");
					bot.Shops.SellItem("Berserker Bunny");
				}
				
				bot.Player.Join("citadel");
				bot.Shops.Load(44);
				bot.Sleep(10000);
				bot.Shops.BuyItem(44, "Nulgath Shaped Chocolate");
			}
			
			bot.Quests.EnsureComplete(5660);
			
			bot.Wait.ForPickup("Roentgenium of Nulgath");
			bot.Player.Pickup("Roentgenium of Nulgath");
		}
	}
	
	public void NulgathLarvaeFarm(ScriptInterface bot){
		bot.Quests.EnsureAccept(2566);
		
		bot.Player.Join("gilead");
		bot.Player.HuntForItem("Mana Elemental", "Charged Mana Energy for Nulgath", 5, true);
		
		bot.Player.Join("elemental");
		bot.Player.HuntForItem("Mana Golem", "Mana Energy for Nulgath", 1, true);
		
		bot.Quests.EnsureComplete(2566);
		
		bot.Player.Pickup("Unidentified 13", "Voucher of Nulgath (non-mem)", "Dark Crystal Shard", "Diamond of Nulgath", "Gem of Nulgath", "Tainted Gem", "Totem of Nulgath");
	}
}
