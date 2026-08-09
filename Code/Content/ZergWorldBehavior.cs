using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using Zerg.Code.Convenience;

namespace Zerg.Code.Content
{
    class ZergWorldBehavior
    {
        public static void init()
        {
            WorldBehaviourAsset asset = new WorldBehaviourAsset
            {
                id = "zerg_creep_decay",
                interval = 5f,
                interval_random = 2f,
                action = new WorldBehaviourAction(ZergCreepDecay.checkCreep),
                action_world_clear = new WorldBehaviourAction(ZergCreepDecay.clear)
            };
            asset.manager = new WorldBehaviour(asset);
            AssetManager.world_behaviours.add(asset);
        }
    }



    public static class ZergCreepDecay
    {
        public static void checkCreep()
        {
            if (WorldLawLibrary.world_law_forever_tumor_creep.isEnabled())
            {
                return;
            }
            BiomeAsset biomeAsset = AssetManager.biome_library.get("biome_zerg_creep");

            ZergCreepDecay.clear();
            ZergCreepDecay.addToNotChecked(biomeAsset.getTileLow());
            ZergCreepDecay.addToNotChecked(biomeAsset.getTileHigh());
            if (ZergCreepDecay.not_checked_tiles.Count == 0)
            {
                return;
            }

            List<string> creep_hub_id = new List<string>() {SZB.Hatchery,SZB.Lair,SZB.Hive,SZB.Creep_Tumor};
            Kingdom tKingdom = World.world.kingdoms_wild.get("Zerg");



            if (tKingdom.buildings.Count > 0)
            {
                List<Building> list = tKingdom.buildings;
                for (int i = 0; i < list.Count; i++)
                {
                    Building tBuilding = list[i];
                    if (tBuilding.isUsable() && creep_hub_id.Contains(tBuilding.asset.id))
                    {
                        ZergCreepDecay.checkTile(tBuilding.current_tile);
                        ZergCreepDecay.next_wave.Add(tBuilding.current_tile);
                    }
                }
            }
            ZergCreepDecay.startWave("biome_zerg_creep");
            if (ZergCreepDecay.not_checked_tiles.Count > 0)
            {
                ZergCreepDecay.destroyNonCheckedCreep();
            }
        }

        private static void startWave(string pBiomeID)
        {
            if (ZergCreepDecay.next_wave.Count == 0)
            {
                return;
            }
            ZergCreepDecay.cur_wave.AddRange(ZergCreepDecay.next_wave);
            ZergCreepDecay.next_wave.Clear();
            while (ZergCreepDecay.cur_wave.Count > 0)
            {
                WorldTile tTile = ZergCreepDecay.cur_wave[ZergCreepDecay.cur_wave.Count - 1];
                ZergCreepDecay.cur_wave.RemoveAt(ZergCreepDecay.cur_wave.Count - 1);
                for (int i = 0; i < tTile.neighboursAll.Length; i++)
                {
                    WorldTile tNeighbour = tTile.neighboursAll[i];
                    if (tNeighbour.Type.biome_id == pBiomeID && !ZergCreepDecay.checked_tiles.Contains(tNeighbour))
                    {
                        ZergCreepDecay.checkTile(tNeighbour);
                        ZergCreepDecay.next_wave.Add(tNeighbour);
                    }
                }
            }
            if (ZergCreepDecay.next_wave.Count > 0)
            {
                ZergCreepDecay.startWave(pBiomeID);
            }
        }


        private static void destroyNonCheckedCreep()
        {
            foreach (WorldTile tTile in ZergCreepDecay.not_checked_tiles)
            {
                ZergCreepDecay._list_of_disconnected_tiles.Add(tTile);
            }

            int cnt = (int)MathF.Max(40, _list_of_disconnected_tiles.Count / 200);

            foreach (WorldTile pTile in ZergCreepDecay._list_of_disconnected_tiles.LoopRandom(cnt))
            {
                MapAction.decreaseTile(pTile, false, "flash");
            }
        }

        private static void checkTile(WorldTile pTile)
        {
            ZergCreepDecay.checked_tiles.Add(pTile);
            ZergCreepDecay.not_checked_tiles.Remove(pTile);
        }

        private static void addToNotChecked(TopTileType pTileType)
        {
            if (pTileType.hashset.Count == 0)
            {
                return;
            }
            ZergCreepDecay.not_checked_tiles.UnionWith(pTileType.hashset);
        }


        public static void clear()
        {
            ZergCreepDecay.checked_tiles.Clear();
            ZergCreepDecay.not_checked_tiles.Clear();
            ZergCreepDecay.next_wave.Clear();
            ZergCreepDecay.cur_wave.Clear();
            ZergCreepDecay._list_of_disconnected_tiles.Clear();
        }

        private static List<WorldTile> next_wave = new List<WorldTile>();

        private static List<WorldTile> cur_wave = new List<WorldTile>();

        private static HashSetWorldTile checked_tiles = new HashSetWorldTile();

        private static HashSetWorldTile not_checked_tiles = new HashSetWorldTile();

        private static List<WorldTile> _list_of_disconnected_tiles = new List<WorldTile>();



    }
}
