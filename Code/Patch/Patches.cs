using ai;
using ai.behaviours;
using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Text;
using UnityEngine;
using Zerg.Code.Convenience;
using Zerg.Code.Extend;
using Zerg.Code.Framework;
using static UnityEngine.GraphicsBuffer;

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


                if(__instance.building.residents.Count <= 100)
                {
                    string spawn_units_asset = __instance.building.asset.spawn_units_asset;
                    Actor actor = Tools.spawnZergUnit(spawn_units_asset, __instance.building.current_tile);
                    if (__instance.building.kingdom != null) actor.setKingdom(__instance.building.kingdom);
                    __instance.setUnitFromHere(actor);
                    actor.applyRandomForce();

                    if (Randy.randomChance(0.1f) && list.Contains(SZB.Spawning_Pool))
                    {
                        actor = Tools.spawnZergUnit(SZA.Queen, __instance.building.current_tile);
                        if(__instance.building.kingdom != null)actor.setKingdom(__instance.building.kingdom);
                        __instance.setUnitFromHere(actor);
                        actor.applyRandomForce();
                    }
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

        [HarmonyPrefix]
        [HarmonyPatch(typeof(BuildingCreepWorker), "update")]
        public static void BuildingCreepWorker_update(BuildingCreepWorker __instance)
        {
            if (__instance._parent.building.asset.id != SZB.Creep_Tumor) return;
            if (__instance._total_step_counter < __instance.steps_max) return;
            if (Randy.randomChance(0.97f)) return;
            if(__instance.cur_tile == null) return;
            if (!Tools.canBuildFrom(__instance.cur_tile, AssetManager.buildings.get(SZB.Creep_Tumor))) return;
            foreach (Building building in Finder.getBuildingsFromChunk(__instance.cur_tile, 3, 18))
            {
                if (building.asset.grow_creep && building.asset.grow_creep_type == "biome_zerg_creep")
                {
                    return;
                }
            }

            Building build = World.world.buildings.addBuilding(SZB.Creep_Tumor,__instance.cur_tile);
            build.setKingdom(__instance._parent.building.kingdom);
        }

        [HarmonyPostfix]
        [HarmonyPatch(typeof(CombatActionLibrary), "tryToCastSpell")]
        public static void CombatActionLibrary_tryToCastSpell(CombatActionLibrary __instance,ref AttackData pData)
        {
            if(pData.initiator.a.hasTag("Zerg"))
            {
                pData.initiator.a.finishStatusEffect("recovery_spell");
            }
        }

        [HarmonyPrefix]
        [HarmonyPatch(typeof(KingdomManager), "removeObject")]
        public static void KingdomManager_removeObject(KingdomManager __instance, ref Kingdom pKingdom)
        {
            foreach(Building building in World.world.buildings)
            {
                if(building.asset.kingdom == "Zerg" && building.kingdom == pKingdom)
                {
                    building.setKingdom(World.world.kingdoms_wild.get("Zerg"));
                }
            }
        }

        [HarmonyPrefix]
        [HarmonyPatch(typeof(Actor), "addAggro", new Type[] { typeof(Actor) })]
        public static bool Actor_addAggro(Actor __instance, ref Actor pActor)
        {
            if (pActor.isRekt())
            {
                return false;
            }
            if (pActor == __instance)
            {
                return false;
            }
            if (__instance.hasTag("Zerg") || __instance.hasStatus("Neural_Parasite"))
            {
                if (pActor.attack_target == __instance) pActor.clearAttackTarget();
                if (__instance.attack_target == pActor) __instance.clearAttackTarget();
                return false;
            }
            if (pActor.hasTag("Zerg") || pActor.hasStatus("Neural_Parasite"))
            {
                if (pActor.attack_target == __instance) pActor.clearAttackTarget();
                if (__instance.attack_target == pActor) __instance.clearAttackTarget();
                return false;
            }
            return true;
        }

        [HarmonyPrefix]
        [HarmonyPatch(typeof(Actor), "addForce")]
        public static bool Actor_addForce(Actor __instance)
        {
            if (__instance.hasTrait("zerg_armored_unit")) return false; 
            return true;
        }

        [HarmonyPrefix]
        [HarmonyPatch(typeof(Actor), "makeStunned")]
        public static bool Actor_makeStunned(Actor __instance)
        {
            if (__instance.hasTrait("zerg_ultar_unit") || __instance.hasTrait("zerg_frenzied")) return false;
            return true;
        }

        [HarmonyPrefix]
        [HarmonyPatch(typeof(Actor), "die")]
        public static bool Actor_die(Actor __instance)
        {
            if(__instance.attackedBy != null && __instance.attackedBy.isActor() && (__instance.attackedBy.a.hasTag("Zerg")|| __instance.attackedBy.a.hasStatus("Neural_Parasite")))
            {
                foreach(ActorTrait trait in __instance.traits)
                {
                    AdaptiveEvolution.addNewTrait(trait.id);
                }
            }
            return true;
        }

        //[HarmonyPrefix]
        //[HarmonyPatch(typeof(CombatActionLibrary), "attackRangeAction")]
        //public static bool CombatActionLibrary_attackRangeAction(CombatActionLibrary __instance, ref AttackData pData)
        //{
        //    if (pData.initiator.a.hasTag("Zerg"))
        //    {
        //        Actor tSelf = pData.initiator.a;
        //        BaseSimObject tAttackTarget = pData.target;
        //        string tProjectileID = pData.projectile_id;
        //        float actor_scale = tSelf.actor_scale;
        //        float tScaleMod = tSelf.getScaleMod();
        //        float tSizeThis = tSelf.stats["size"];
        //        int tProjectiles = (int)tSelf.stats["projectiles"];
        //        Vector2 tAttackPosition;
        //        if (tAttackTarget == null)
        //        {
        //            tAttackPosition = pData.hit_position;
        //        }
        //        else
        //        {
        //            tAttackPosition = __instance.getAttackTargetPosition(pData);
        //            tAttackPosition.y += 0.2f * tScaleMod;
        //        }

        //        float tStartHeight = 0.6f * tScaleMod;
        //        float tTargetHeight = 0f;
        //        float tAngle = 0f;
        //        for (int i = 0; i < tProjectiles; i++)
        //        {
        //            Vector2 tProjectileAttackVector = new Vector2(tAttackPosition.x, tAttackPosition.y);
        //            Vector3 tStartProjectile = Toolbox.getNewPoint(tSelf.current_position.x, tSelf.current_position.y, tProjectileAttackVector.x, tProjectileAttackVector.y, tSizeThis * tScaleMod, true);
        //            tStartProjectile.y += tSelf.getHeight();
        //            if (tAttackTarget != null &&  tAttackTarget.isActor())
        //            {
        //                Vector3 index = tAttackTarget.a.target_angle * tAttackTarget.a._current_combined_movement_speed * Vector2.Distance(tSelf.current_position, tAttackTarget.current_position) / AssetManager.projectiles.get(tProjectileID).speed;
        //                tProjectileAttackVector.x += index.x;
        //                tProjectileAttackVector.y += index.y ;
        //            }

        //            if (tAttackTarget != null && tAttackTarget.isInAir())
        //            {
        //                tTargetHeight = tAttackTarget.getHeight();
        //            }
        //            tAngle = World.world.projectiles.spawn(tSelf, tAttackTarget, tProjectileID, tStartProjectile, tProjectileAttackVector, tTargetHeight, tStartHeight, pData.kill_action, pData.kingdom).getLaunchAngle();
        //        }
        //        tSelf.spawnSlash(tAttackPosition, null, 2f, tTargetHeight, 0f, new float?(tAngle));





        //        return false;
        //    }
        //    return true;
        //}
    }
}
