// Project:         LootRealism mod for Daggerfall Unity (http://www.dfworkshop.net)
// Copyright:       Copyright (C) 2019
// License:         MIT License (http://www.opensource.org/licenses/mit-license.php)
// Authors:         Hazelnut & Ralzar

using System.Collections.Generic;
using UnityEngine;
using DaggerfallWorkshop;
using DaggerfallWorkshop.Game;
using DaggerfallWorkshop.Game.Items;
using DaggerfallWorkshop.Game.Utility.ModSupport;
using DaggerfallWorkshop.Utility;

namespace LootRealism
{
    public class LootRealismMod : MonoBehaviour
    {
        const int G = 85;   // Mob Array Gap from 42 .. 128 = 85
        static Mod mod;

        [Invoke(StateManager.StateTypes.Start, 0)]
        public static void Init(InitParams initParams)
        {
            mod = initParams.Mod;
            var go = new GameObject(mod.Title);
            go.AddComponent<LootRealismMod>();
        }

        void Awake()
        {
            InitMod();

            mod.IsReady = true;
        }

        private static void InitMod()
        {
            Debug.Log("Begin mod init: LootRealism");

            // Iterate over the new mob enemy data array and load into DFU enemies data.
            foreach (int mobDataId in MobLootKeys.Keys)
            {
                // Log a message indicating the enemy mob being updated and update the loot key.
                Debug.LogFormat("Updating enemy loot key for {0} to {1}.", EnemyBasics.Enemies[mobDataId].Name, MobLootKeys[mobDataId]);
                EnemyBasics.Enemies[mobDataId].LootTableKey = MobLootKeys[mobDataId];
            }
            // Replace the default loot matrix table with custom data.
            LootTables.DefaultLootTables = LootRealismTables;

            Debug.Log("Finished mod init: LootRealism");
        }

        static Dictionary<int, string> MobLootKeys = new Dictionary<int, string>()
        {
            {9, "F"},   // Giant
            {10, "B"},  // Nymph
            {19, "J"},  // Mummy
            {32, "J"},  // Lich
            {33, "S"},  // Ancient Lich
            {26, "S"},  // Fire Daedra
            {27, "S"},  // Daedroth
            {29, "S"},  // Daedra Seducer
            {140-G, "O"}, // Monk
        };

        static readonly LootChanceMatrix[] LootRealismTables = {
            new LootChanceMatrix() {key = "-", MinGold = 0,     MaxGold = 0,    P1 = 0, P2 = 0, C1 = 0, C2 = 0, C3 = 0, M1 = 0, AM = 0,  WP = 0,  MI = 0, CL = 0, BK = 0, M2 = 0, RL = 0 }, //None
	        new LootChanceMatrix() {key = "A", MinGold = 0,     MaxGold = 5,    P1 = 0, P2 = 0, C1 = 1, C2 = 1, C3 = 1, M1 = 0, AM = 1,  WP = 5,  MI = 1, CL = 5, BK = 0, M2 = 1, RL = 0 }, //Orcs
	        new LootChanceMatrix() {key = "B", MinGold = 0,     MaxGold = 0,    P1 = 5, P2 = 5, C1 = 0, C2 = 0, C3 = 0, M1 = 0, AM = 0,  WP = 0,  MI = 0, CL = 0, BK = 0, M2 = 0, RL = 0 }, //Nature
	        new LootChanceMatrix() {key = "C", MinGold = 0,     MaxGold = 10,   P1 = 5, P2 = 5, C1 = 3, C2 = 3, C3 = 3, M1 = 3, AM = 10, WP = 40, MI = 1, CL = 50,BK = 2, M2 = 2, RL = 1 }, //Rangers
	        new LootChanceMatrix() {key = "D", MinGold = 0,     MaxGold = 0,    P1 = 2, P2 = 2, C1 = 2, C2 = 2, C3 = 2, M1 = 2, AM = 0,  WP = 0,  MI = 0, CL = 0, BK = 0, M2 = 0, RL = 0 }, //Harpy
	        new LootChanceMatrix() {key = "E", MinGold = 0,     MaxGold = 10,   P1 = 2, P2 = 2, C1 = 5, C2 = 5, C3 = 5, M1 = 2, AM = 0,  WP = 5,  MI = 0, CL = 0, BK = 0, M2 = 1, RL = 2 }, //Giant
	        new LootChanceMatrix() {key = "F", MinGold = 2,     MaxGold = 15,   P1 = 2, P2 = 2, C1 = 5, C2 = 5, C3 = 5, M1 = 2, AM = 80, WP = 70, MI = 2, CL = 0, BK = 0, M2 = 3, RL = 10 },//Giant Loot
	        new LootChanceMatrix() {key = "G", MinGold = 0,     MaxGold = 8,    P1 = 0, P2 = 0, C1 = 0, C2 = 0, C3 = 0, M1 = 0, AM = 10, WP = 1,  MI = 1, CL = 10,BK = 0, M2 = 3, RL = 5 }, //Undead naked
	        new LootChanceMatrix() {key = "H", MinGold = 0,     MaxGold = 5,    P1 = 0, P2 = 0, C1 = 0, C2 = 0, C3 = 0, M1 = 0, AM = 5,  WP = 30, MI = 1, CL = 2, BK = 1, M2 = 0, RL = 5 }, //Undead armed
	        new LootChanceMatrix() {key = "I", MinGold = 0,     MaxGold = 0,    P1 = 0, P2 = 0, C1 = 0, C2 = 0, C3 = 0, M1 = 0, AM = 0,  WP = 0,  MI = 1, CL = 0, BK = 0, M2 = 0, RL = 5 }, //Undead spirits
	        new LootChanceMatrix() {key = "J", MinGold = 10,    MaxGold = 40,   P1 = 3, P2 = 3, C1 = 3, C2 = 3, C3 = 3, M1 = 5, AM = 1,  WP = 1,  MI = 5, CL = 5, BK = 5, M2 = 2, RL = 10 },//Undead bosses
	        new LootChanceMatrix() {key = "K", MinGold = 10,    MaxGold = 20,   P1 = 3, P2 = 3, C1 = 3, C2 = 3, C3 = 3, M1 = 3, AM = 70, WP = 50, MI = 3, CL = 0, BK = 2, M2 = 2, RL = 80 },//Undead Loot
	        new LootChanceMatrix() {key = "L", MinGold = 0,     MaxGold = 10,   P1 = 0, P2 = 0, C1 = 3, C2 = 3, C3 = 3, M1 = 3, AM = 70, WP = 50, MI = 2, CL = 20,BK = 0, M2 = 3, RL = 3 }, //Nest Loot
	        new LootChanceMatrix() {key = "M", MinGold = 0,     MaxGold = 7,    P1 = 1, P2 = 1, C1 = 1, C2 = 1, C3 = 1, M1 = 2, AM = 80, WP = 40, MI = 2, CL = 15,BK = 2, M2 = 3, RL = 1 }, //Cave Loot
	        new LootChanceMatrix() {key = "N", MinGold = 1,     MaxGold = 40,   P1 = 5, P2 = 5, C1 = 5, C2 = 5, C3 = 5, M1 = 5, AM = 90, WP = 60, MI = 2, CL = 20,BK = 5, M2 = 2, RL = 5 }, //Castle Loot
	        new LootChanceMatrix() {key = "O", MinGold = 0,     MaxGold = 30,   P1 = 1, P2 = 1, C1 = 1, C2 = 1, C3 = 1, M1 = 1, AM = 5,  WP = 10, MI = 1, CL = 50,BK = 0, M2 = 0, RL = 0 }, //Rogues
	        new LootChanceMatrix() {key = "P", MinGold = 2,     MaxGold = 10,   P1 = 5, P2 = 2, C1 = 2, C2 = 2, C3 = 2, M1 = 2, AM = 10, WP = 10, MI = 2, CL = 50,BK = 9, M2 = 2, RL = 0 }, //Spellsword
	        new LootChanceMatrix() {key = "Q", MinGold = 10,    MaxGold = 40,   P1 = 2, P2 = 2, C1 = 4, C2 = 4, C3 = 4, M1 = 2, AM = 0,  WP = 0,  MI = 2, CL = 70,BK = 5, M2 = 3, RL = 5 }, //Vampires
	        new LootChanceMatrix() {key = "R", MinGold = 2,     MaxGold = 10,   P1 = 0, P2 = 0, C1 = 3, C2 = 3, C3 = 3, M1 = 5, AM = 0,  WP = 0,  MI = 1, CL = 0, BK = 0, M2 = 2, RL = 0 }, //Water monsters
	        new LootChanceMatrix() {key = "S", MinGold = 30,    MaxGold = 70,   P1 = 5, P2 = 5, C1 = 5, C2 = 5, C3 = 5, M1 = 15,AM = 0,  WP = 0,  MI = 8, CL = 5, BK = 5, M2 = 2, RL = 10 },//Dragon Loot, Daedras
	        new LootChanceMatrix() {key = "T", MinGold = 10,    MaxGold = 20,   P1 = 0, P2 = 0, C1 = 0, C2 = 0, C3 = 0, M1 = 0, AM = 80, WP = 60, MI = 1, CL = 50,BK = 0, M2 = 0, RL = 0},  //Fighter
	        new LootChanceMatrix() {key = "U", MinGold = 0,     MaxGold = 20,   P1 = 5, P2 = 5, C1 = 5, C2 = 5, C3 = 5, M1 = 10,AM = 50, WP = 20, MI = 3, CL = 20,BK = 10,M2 = 5, RL = 10 },//Laboratory Loot, Wizards
        };
    }
}
