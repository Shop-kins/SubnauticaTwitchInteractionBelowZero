﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using TwitchInteractionBelowZero.Player_Events;
using UnityEngine;
using UWE;

namespace TwitchInteraction.Player_Events
{
    //Add functions to the EventLookup.cs class
    class FunZone // These events are mild->medium inconveniences that can be cheap or used in free votes
    {
        public static void HealPlayer()
        {
            Player.main.liveMixin.AddHealth(777f);
        }

        public static void ToggleDayNight()
        {

            DayNightCycle dayNight = DayNightCycle.main;
            bool flag = dayNight.IsDay();
            if (!flag)
            {
                dayNight.timePassedAsDouble += 1200.0 - DayNightCycle.main.timePassed % 1200.0 + 600.0;

                dayNight.dayNightCycleChangedEvent.Trigger(true);
            }
            else
            {
                dayNight.timePassedAsDouble += 1200.0 - DayNightCycle.main.timePassed % 1200.0;
                dayNight.dayNightCycleChangedEvent.Trigger(false);
            }

        }

        public static void openPDA()
        {

            PDA pda = Player.main.GetPDA();
            if (!pda.Open())
            {
                return;
            }
        }

        public static void FillOxygen()
        {
            Player.main.oxygenMgr.AddOxygen(Player.main.GetOxygenCapacity() - Player.main.GetOxygenAvailable());
        }

        private static float initialMouseSens;

        public static void RandomMouseSens()
        {
            initialMouseSens = GameInput.GetMouseSensitivity();

            System.Random random = new System.Random();
            GameInput.SetMouseSensitivity((float)random.NextDouble());
        }

        public static void CleanupRandomMouseSens()
        {
            GameInput.SetMouseSensitivity(initialMouseSens);
        }

        public static void hideHUD()
        {
            HUDHandler.Hide(HideForScreenshots.HideType.Mask | HideForScreenshots.HideType.HUD);
        }

        public static void showHUD()
        {
            if (MiscSettings.fieldOfView < 40)
            {
                HUDHandler.Hide(HideForScreenshots.HideType.None);
                HUDHandler.Hide(HideForScreenshots.HideType.Mask);
            } else
            {
                HUDHandler.Hide(HideForScreenshots.HideType.None);
            }
           
        }

        public static void randomSummon()
        {
            System.Random random = new System.Random();

            DevConsole.SendConsoleCommand("spawn " + FunZoneFixtures.creatures[random.Next(FunZoneFixtures.creatures.Length)]);
        }

        public static void fillFoodWater()
        {
            Survival component = Player.main.GetComponent<Survival>();
            component.food += 124 - component.food;
            component.water += 100 - component.water;
        }

        public static void randomBlueprintUnlock()
        {
            System.Random random = new System.Random();

            TechType[] blueprintTech = FunZoneFixtures.blueprintTech;

            int randomNum = random.Next(blueprintTech.Length);

            int counter = 0;
            while (CrafterLogic.IsCraftRecipeUnlocked(blueprintTech[randomNum]) && counter < 50)
            {
                randomNum = random.Next(blueprintTech.Length);
                counter++;
            }

            if (CraftData.IsAllowed(blueprintTech[randomNum]) && KnownTech.Add(blueprintTech[randomNum], true))
            {
                ErrorMessage.AddDebug("Unlocked " + Language.main.Get(blueprintTech[randomNum].AsString(false)));
            }
            else
                CraftData.AddToInventory(TechType.Titanium, 2);
        }

        public static void randomItem()
        {
            System.Random random = new System.Random();
            TechType[] resources = FunZoneFixtures.basic_resources;
            CraftData.AddToInventory(resources[random.Next(resources.Length)], 1);
        }

        public static void randomAdvancedResources()
        {
            System.Random random = new System.Random();
            string[] resources = FunZoneFixtures.advanced_resources;

            DevConsole.SendConsoleCommand("item " + resources[random.Next(resources.Length)]);
        }

        public static void junkFill()
        {
            System.Random random = new System.Random();
            for (int i = 0; i < 48; i++)
            {
                CraftData.AddToInventory(FunZoneFixtures.random_junk[random.Next(FunZoneFixtures.random_junk.Length)], 1, false, false);
            }
        }   

        private static float initialFOV;
        public static void fovRandom()
        {
            initialFOV = MiscSettings.fieldOfView;


            //This is a kinda janky method of grabbing a random fov outside of the typically playable fovs
            System.Random random = new System.Random();

            int lowRandNum = random.Next(5, 45);
            int highRandNum = random.Next(85, 150);

            double randCoinFlip = random.NextDouble();
            int randNum;
            if (randCoinFlip > 0.5)
            {
                randNum = highRandNum;
            }
            else
            {
                randNum = lowRandNum;
            }

            //ErrorMessage.AddMessage(randNum.ToString());
            if (randNum < 40)
            {
                HideForScreenshots.Hide(HideForScreenshots.HideType.Mask);
            }
            MiscSettings.fieldOfView = randNum;
            if (SNCameraRoot.main != null)
            {
                SNCameraRoot.main.SyncFieldOfView();
            }
        }

        public static void fovNormal()
        {
            MiscSettings.fieldOfView = initialFOV;
            if (SNCameraRoot.main != null)
            {
                SNCameraRoot.main.SyncFieldOfView();
            }

            HideForScreenshots.Hide(HideForScreenshots.HideType.None);
        }


        public static void killBadThings()
        {
            ReaperLeviathan[] Reapers = GameObject.FindObjectsOfType<ReaperLeviathan>();

            foreach (var r in Reapers)
            {
                GameObject.Destroy(r.gameObject);
            }

            SeaDragon[] seaDragons = GameObject.FindObjectsOfType<SeaDragon>();

            foreach (var r in seaDragons)
            {
                GameObject.Destroy(r.gameObject);
            }

            GhostLeviathan[] ghostLeviathans = GameObject.FindObjectsOfType<GhostLeviathan>();

            foreach (var r in ghostLeviathans)
            {
                GameObject.Destroy(r.gameObject);
            }
            Warper[] warpers = GameObject.FindObjectsOfType<Warper>();

            foreach (var r in warpers)
            {
                GameObject.Destroy(r.gameObject);
            }

            CrabSquid[] crabSquids = GameObject.FindObjectsOfType<CrabSquid>();

            foreach ( var r in crabSquids)
            {
                GameObject.Destroy(r.gameObject);
            }

        }

        public static void InvertControls()
        {
            InputPatch.InputPatch.invertKeyboardAxisX = true;
            InputPatch.InputPatch.invertKeyboardAxisY = true;
            InputPatch.InputPatch.invertKeyboardAxisZ = true;
            InputPatch.InputPatch.invertMouseAxisX = true;
            InputPatch.InputPatch.invertMouseAxisY = true;
        }

        public static void NormalControls()
        {
            InputPatch.InputPatch.invertKeyboardAxisX = false;
            InputPatch.InputPatch.invertKeyboardAxisY = false;
            InputPatch.InputPatch.invertKeyboardAxisZ = false;
            InputPatch.InputPatch.invertMouseAxisX = false;
            InputPatch.InputPatch.invertMouseAxisY = false;
        }

        public static void DisableControls()
        {
            InputPatch.InputPatch.controlsEnabled = false;
        }

        public static void EnableControls()
        {
            InputPatch.InputPatch.controlsEnabled = true;
        }

        public static void ClearRandomQuickSlot()
        {
            QuickSlots quickSlots = Inventory.main.quickSlots;

            // Find all slots which have items in them
            List<int> activeSlots = new List<int>();
            for (int i = 0; i < quickSlots.slotCount; i++)
            {
                if (quickSlots.GetSlotItem(i) != null)
                {
                    activeSlots.Add(i);
                }
            }

            if (activeSlots.Count == 0)
            {
                // Prevent OutOfBounds errors
                return;
            }

            System.Random random = new System.Random();
            int randomSlotID = activeSlots[random.Next(0, activeSlots.Count)];
            quickSlots.Unbind(randomSlotID);
        }

        public static void RandomizeQuickSlots()
        {
            QuickSlots quickSlots = Inventory.main.quickSlots;

            // Make a list of all slots (including empty slots)
            List<InventoryItem> allSlots = new List<InventoryItem>();
            for (int i = 0; i < quickSlots.slotCount; i++)
            {
                allSlots.Add(quickSlots.GetSlotItem(i));
            }

            // Shuffle the list
            System.Random random = new System.Random();
            int n = allSlots.Count;
            while (n > 1)
            {
                n--;
                int k = random.Next(n + 1);
                InventoryItem value = allSlots[k];
                allSlots[k] = allSlots[n];
                allSlots[n] = value;
            }

            // Set the quick slots
            for (int i = 0; i < quickSlots.slotCount; i++)
            {
                quickSlots.binding[i] = allSlots[i];
            }
            // Notify the game that the slots have changed
            for (int i = 0; i < quickSlots.slotCount; i++)
            {
                quickSlots.NotifyBind(i, allSlots[i] != null);
            }

        }

        public static void RemoveRandomBattery()
        {
            PlayerTool[] playerTools = Inventory.main.gameObject.GetAllComponentsInChildren<PlayerTool>();
            List<EnergyMixin> toolMixins = new List<EnergyMixin>();

            foreach (PlayerTool playerTool in playerTools)
            {
                EnergyMixin toolEnergyMixin = playerTool.GetComponent<EnergyMixin>();

                // This is a tool, not something like a floater
                if (toolEnergyMixin != null && toolEnergyMixin.HasItem())
                {
                    toolMixins.Add(toolEnergyMixin);
                }
            }

            if (toolMixins.Count == 0)
            {
                // Prevent OutOfBounds errors
                return;
            }

            System.Random random = new System.Random();
            int randomMixin = random.Next(0, toolMixins.Count);
            EnergyMixin energyMixin = toolMixins[randomMixin];

            InventoryItem storedBattery = energyMixin.batterySlot.storedItem;
            energyMixin.batterySlot.RemoveItem();
            Inventory.main.ForcePickup(storedBattery.item);
        }

        public static void DumpEquipment()
        {
        	int equipmentCount = 0;

            // Count the hotbar tools
            List<ItemsContainer.ItemGroup> itemGroups = new List<ItemsContainer.ItemGroup>(Inventory.main.quickSlots.container._items.Values);
            List<InventoryItem> hotbarTools = new List<InventoryItem>();
            for (int i = 0; i < Inventory.main.quickSlots.binding.Length; i++)
            {
                if (Inventory.main.quickSlots.binding[i] != null)
                {
                	equipmentCount++;
                	hotbarTools.Add(Inventory.main.quickSlots.binding[i]);
                }
            }


            // These are hardcoded in the Inventory class to, so why bother
            string[] inventoryEquipmentSlots = new string[]
            {
                "Head",
                "Body",
                "Gloves",
                "Foots",
                "Chip1",
                "Chip2",
                "Tank"
            };

            List<string> equipment = new List<string>();

            // Count all equipment (O2 tank, rebreather, etc.)
            foreach (string equipmentSlot in inventoryEquipmentSlots)
            {
                InventoryItem equipmentItem;
                Inventory.main.equipment.equipment.TryGetValue(equipmentSlot, out equipmentItem);
                if (equipmentItem == null)
                {
                    continue;
                }
                equipment.Add(equipmentSlot);
                equipmentCount++;
            }

            System.Random random = new System.Random();
            int randomEquipment = random.Next(0, equipmentCount);

            if (randomEquipment < hotbarTools.Count) {
            	// Drop a hotbar tool
                Inventory.main.InternalDropItem(hotbarTools[randomEquipment].item, true);
            } else {
                // Drop a piece of equipment
                string equipmentSlot = equipment[randomEquipment - hotbarTools.Count];

                InventoryItem equipmentItem;
                Inventory.main.equipment.equipment.TryGetValue(equipmentSlot, out equipmentItem);

                // This is basically the Equipment.RemoveItem function, but a little modified
                Inventory.main.equipment.equipment[equipmentSlot] = null;
                TechType equipmentType = equipmentItem.item.GetTechType();
                Inventory.main.equipment.UpdateCount(equipmentType, false);
                Equipment.SendEquipmentEvent(equipmentItem.item, 1, Inventory.main.equipment.owner, equipmentSlot);
                Inventory.main.equipment.NotifyUnequip(equipmentSlot, equipmentItem);
                equipmentItem.container = null;
                
                // Put the equipment in the inventory
                Inventory.main._container.UnsafeAdd(equipmentItem);

                // Imediately dump it again
                Inventory.main.InternalDropItem(equipmentItem.item, true);
            }

        }

        public static void EnableSonicMode()
        {
            PlayerMotor waterController = Player.main.playerController.underWaterController;
            waterController.swimDrag = 0.75f;
            waterController.forwardMaxSpeed = 50f;

            PlayerMotor groundController = Player.main.playerController.groundController;
            groundController.forwardMaxSpeed = 35f;

            FastMovement.Active = true;
        }

        public static void DisableFastMode()
        {
            PlayerMotor waterController = Player.main.playerController.underWaterController;
            waterController.debugSpeedMult = 1f;
            waterController.swimDrag = 2.5f;
            waterController.forwardMaxSpeed = 5f;

            PlayerMotor groundController = Player.main.playerController.groundController;
            groundController.forwardMaxSpeed = 3.5f;

            FastMovement.Active = false;
        }

        public static void SpawnUserBeacon(String username)
        {
            CoroutineHost.StartCoroutine(FunZone.SpawnUserBeaconAsync(username));
        }

        public static IEnumerator SpawnUserBeaconAsync(String username)
        {
            TaskResult<GameObject> currentResult = new TaskResult<GameObject>();
            yield return (object)CraftData.GetPrefabForTechTypeAsync(TechType.Beacon, false, (IOut<GameObject>)currentResult);
            GameObject prefab = currentResult.Get();
            var beaconObj = GameObject.Instantiate(prefab, Player.main.lastPosition, Quaternion.identity);
            var beacon = beaconObj.GetComponent<Beacon>();
            beacon.label = username;
            beacon.beaconLabel.SetLabel(username);
        }

        public static void LifepodForceDrop(String username)
        {
            UnityEngine.Object.Destroy(LifepodDrop.FindObjectOfType<LifepodDrop>().gameObject);
            SupplyDropData dropData = LifepodDrop.FindObjectOfType<LifepodDrop>().GetDropData();
            SupplyDropManager.main.PerformDrop(dropData, dropData.PickDropZone());
            Player.main.OnPlayerPositionCheat();
        }



    }
}

