using ai.behaviours;
using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using Zerg.Code.Content;
using Zerg.Code.Convenience;
using Zerg.Code.Extend;

namespace Zerg.Code.Patch
{
    class Patches
    {
        public static void init()
        {
            Harmony.CreateAndPatchAll(typeof(Patches));
        }

        [HarmonyPrefix]
        [HarmonyPatch(typeof(UnitSpawner), "trySpawnUnit")]
        public static bool UnitSpawner_trySpawnUnit(UnitSpawner __instance)
        {
            if(__instance.building.asset.spawn_units_asset == SZA.Larva)
            {
                __instance._spawn_timer = 5f;
                List<string> list = new List<string>();
                List<string> list_all = new List<string>();
                foreach (Building building in Finder.getBuildingsFromChunk(__instance.building.current_tile,2,32))
                {
                    if(building.kingdom == __instance.building.kingdom &&SZB.list.Contains(building.asset.id) && !list.Contains(building.asset.id))
                    {
                        list.Add(building.asset.id);
                        list_all.Add(building.asset.id);
                        if (building.Zerg_canMutation()) building.Zerg_tryMutation(__instance.building);
                    }


                }
                foreach (Actor actor in Finder.getUnitsFromChunk(__instance.building.current_tile, 2, 32))
                {
                    string str = actor.GetMutation_id();
                    if (actor.kingdom == __instance.building.kingdom &&str != null &&SZB.list.Contains(str) && !list_all.Contains(str))
                    {
                        list_all.Add(str);
                    }
                }
                __instance.SetExtend(list);
                __instance.SetExtend_All(list_all);

                Subspecies tSubspecies = null ! ;//后续有判空
                if (__instance.building.residents.Count > 0)
                {
                    foreach (long tActorID in __instance.building.residents)
                    {
                        Actor tActor = World.world.units.get(tActorID);
                        if (!tActor.isRekt() && tActor.asset.id == __instance.building.asset.spawn_units_asset)
                        {
                            tSubspecies = tActor.subspecies;
                            break;
                        }
                    }
                }
                if (!tSubspecies.isRekt() && tSubspecies.hasReachedPopulationLimit())
                {
                    return false;
                }
                __instance.spawnUnit(tSubspecies);

                if (Randy.randomChance(0.1f))
                {
                    Actor actor = World.world.units.createNewUnit(SZA.Queen, __instance.building.current_tile, pMiracleSpawn: false, 0f, null, null, pSpawnWithItems: false, pAdultAge: false);
                    actor.applyRandomForce();
                }

                if(__instance.building.Zerg_canMutation()) __instance.building.Zerg_tryMutation(__instance.building);
                return false;
            }
            return true;
        }

        [HarmonyPrefix]
        [HarmonyPatch(typeof(BehTrySleep), "execute")]
        public static bool BehTrySleep_execute(BehTrySleep __instance, ref Actor pActor, ref BehResult __result)
        {
            if (pActor.hasSubspeciesTrait("NoSleep"))
            {
                __result = BehResult.Continue;
                return false;
            }
            return true;
        }

    }
}
