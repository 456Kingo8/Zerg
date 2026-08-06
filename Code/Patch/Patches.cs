using ai.behaviours;
using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Text;
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
            if(AssetManager.actor_library.get(__instance.building.asset.spawn_units_asset).traits.Contains("异虫"))
            {
                List<string> list = __instance.GetExtend();
                list.Clear();
                foreach(Building building in Finder.getBuildingsFromChunk(__instance.building.current_tile,2,32))
                {
                    if(building.kingdom == __instance.building.kingdom &&SZB.list.Contains(building.asset.id))
                    {
                        list.Add(building.asset.id);
                    }
                }
                foreach (Actor actor in Finder.getUnitsFromChunk(__instance.building.current_tile, 2, 32))
                {
                    string str = actor.GetMutation_id();
                    if (actor.kingdom == __instance.building.kingdom &&str != null &&SZB.list.Contains(str))
                    {
                        list.Add(str);
                    }
                }


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

                if (Randy.randomChance(0.05f))
                {
                    Actor actor = World.world.units.createNewUnit(SZA.Queen, __instance.building.current_tile, pMiracleSpawn: false, 0f, null, null, pSpawnWithItems: false, pAdultAge: false);
                    actor.applyRandomForce();
                }
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
