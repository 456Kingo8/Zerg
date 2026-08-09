using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using Zerg.Code.Convenience;
using Zerg.Code.Extend;
using static Unity.IO.LowLevel.Unsafe.AsyncReadManagerMetrics;

namespace Zerg.Code.Content
{
    public static class MutationManager
    {

        public static bool Zerg_tryMutation(this Actor actor)
        {
            if (!MutationLibrary.from_dict.ContainsKey(actor.asset.id))
            {
                return false;
            }

            List<string> ids = new List<string>();//实际建造完成的建筑
            List<string> ids_all = new List<string>();//所有建筑，包括用于建造的actor虫茧
            if(actor.hasHomeBuilding())ids = actor._home_building.component_unit_spawner.GetExtend();
            if (actor.hasHomeBuilding()) ids_all = actor._home_building.component_unit_spawner.GetExtend_All();

            foreach (MutationAsset asset in MutationLibrary.from_dict[actor.asset.id])
            {
                if (!Randy.randomChance(asset.chance)) continue;




                if (asset.need_biome)//判定是否在菌毯上
                {
                    if (actor.current_tile.top_type == null) continue;
                    if(actor.current_tile.top_type.biome_asset == null) continue;
                    if(actor.current_tile.top_type.biome_asset.id != "biome_zerg_creep") continue;
                }

                if (asset.building && !Tools.canBuildFrom(actor.current_tile,AssetManager.buildings.get(asset.to_id))) 
                    continue; //判定占地面积


                bool flag = true;
                if (asset.need_house)
                {
                    if (!actor.hasHomeBuilding()) continue;
                    if (ids_all.Contains(asset.to_id) || Hive_special_judge(asset.to_id,ids_all)) continue;//不重复建造同种建筑,理论上特判是多余的，因为建筑不走这个方法
                    //MonoBehaviour.print(ids.ToJson());
                    //MonoBehaviour.print(ids_all.ToJson()); 
                    foreach (string id in asset.building_requirements)
                    {
                        if (!ids.Contains(id))
                        {
                            if (Hive_special_judge(id, ids)) continue;



                            flag = false;
                            break;
                        }
                    }

                }
                else
                {
                    flag = true;
                    //未来或许可以在此处加入科技限制等其他限制,暂时来说need house与有requirements是等价的

                    if (asset.to_id == SZB.Hatchery)  //这里应该抽象化为一个特殊的委托，但暂时无需求
                    {
                        foreach(Building building in Finder.getBuildingsFromChunk(actor.current_tile, 4, 32))
                        {
                            string str = building.asset.id;
                            if (str == SZB.Hatchery || str == SZB.Hive || str == SZB.Lair)
                            {
                                flag = false; 
                                break;
                            }
                        }
                        foreach (Actor act in Finder.getUnitsFromChunk(actor.current_tile, 4, 32))
                        {
                            string str = act.GetMutation_id();
                            if (str == SZB.Hatchery || str == SZB.Hive || str == SZB.Lair)
                            {
                                flag = false;
                                break;
                            }
                        }

                    }

                }
                if (flag)
                {
                    Actor coco = World.world.units.createNewUnit(asset.coco_id, actor.current_tile);
                    coco.addStatusEffect("Zerg_Mutation", asset.cost_time);
                    coco.SetMutation_id(asset.to_id);
                    coco.SetMutation_num(asset.num);
                    coco.SetMutation_building(asset.building);
                    if (actor.hasKingdom())coco.setKingdom(actor.kingdom);
                    //if (actor.hasCity()) coco.setCity(actor.city);//老马设定的是building.city =  zone.city，故本行废弃，不然建筑会被牛走
                    if (actor.hasHomeBuilding()) coco.setHomeBuilding(actor.home_building);
                    actor.die(true);
                    if(asset.building && actor.hasHomeBuilding()) actor._home_building.component_unit_spawner.GetExtend_All().Add(asset.to_id);//提前占位防止重复变异，后续id由Patches中的判定维持存在
                    //MonoBehaviour.print("mutation_start_" + asset.to_id);
                }
                return flag;
            }
            return false;
        }

        public static bool Zerg_endMutation(this Actor actor)
        {
            string id = actor.GetMutation_id();
            int num = actor.GetMutation_num();
            if (id == null || num == 0)
            {
                return false;
            }
            if(actor.GetMutation_building())
            {
                BuildingAsset asset = AssetManager.buildings.get(id);
                if (asset == null)
                {
                    return false;
                }

                Building build = World.world.buildings.addBuilding(asset, actor.current_tile);
                if (actor.hasKingdom()) build.setKingdom(actor.kingdom);
                if(build.asset.id == SZB.Lair || build.asset.id == SZB.Hive)
                {
                    if(actor.home_building != null && actor.home_building.residents.Count > 0)
                    {
                        foreach (var t in actor.home_building.residents)
                        {
                            var act = World.world.units.get(t);
                            act.setHomeBuilding(build);
                        }
                    }
                }
            }
            else
            {
                if (AssetManager.actor_library.get(id) == null)
                {
                    return false;
                }
                for (int i = 0; i < num; i++)
                {
                    Actor act = World.world.units.createNewUnit(id, actor.current_tile);
                    if (actor.hasKingdom()) act.setKingdom(actor.kingdom);
                    //if (actor.hasCity()) act.setCity(actor.city);//老马设定的是building.city =  zone.city，故本行废弃，不然建筑会被牛走
                    if (actor.hasHomeBuilding()) act.setHomeBuilding(actor.home_building);

                    if (id == SZA.Drone)
                    {
                        if (Randy.randomChance(0.5f))
                        {
                            act.clearHomeBuilding();
                            act.beh_tile_target = act.current_chunk.neighbours.GetRandom().tiles.GetRandom();
                        }

                    }
                    else if(act.asset.decision_ids != null &&act.asset.decision_ids.Contains("zerg_try_mutation"))
                    {
                        if(Randy.randomChance(0.2f))
                        act.switchDecisionState(AssetManager.decisions_library.get("zerg_try_mutation").decision_index);
                    }

                }
            }
            actor.die(false);
            return true;
        }

        public static bool Zerg_canMutation(this Building build)
        {
            return MutationLibrary.from_dict.ContainsKey(build.asset.id);
        }

        public static bool Zerg_tryMutation(this Building build,Building homebuilding)
        {

            if(!MutationLibrary.from_dict.ContainsKey(build.asset.id))
            {
                MonoBehaviour.print("[Zerg]Try to mutation without pre-check");
                return false;
            }


            foreach (MutationAsset asset in MutationLibrary.from_dict[build.asset.id])
            {
                if (!Randy.randomChance(asset.chance)) continue;
                List<string> ids = homebuilding.component_unit_spawner.GetExtend();
                List<string> ids_all = homebuilding.component_unit_spawner.GetExtend_All();
                bool flag = true;

                if (ids_all.Contains(asset.to_id) || Hive_special_judge(asset.to_id, ids_all)) continue;//不重复建造同种建筑


                foreach (string id in asset.building_requirements)
                {
                    if (!ids.Contains(id))
                    {
                        if (Hive_special_judge(id, ids)) continue;



                        flag = false;
                        break;
                    }
                }
                if(flag)
                {
                    Actor coco = World.world.units.createNewUnit(asset.coco_id, build.current_tile);
                    coco.addStatusEffect("Zerg_Mutation", asset.cost_time);
                    coco.SetMutation_id(asset.to_id);
                    coco.SetMutation_num(asset.num);
                    coco.SetMutation_building(asset.building);
                    if (build.hasKingdom()) coco.setKingdom(build.kingdom);
                    //if (build.hasCity()) coco.setCity(build.city);//老马设定的是building.city =  zone.city，故本行废弃，不然建筑会被牛走
                    coco.setHomeBuilding(homebuilding);
                    homebuilding.component_unit_spawner.GetExtend_All().Add(asset.to_id);//提前占位防止重复变异，后续id由Patches中的判定维持存在
                    return true;
                }
            }
            return false;
        }

        public static bool Hive_special_judge(string id,List<string> strings)
        {
            if (id == SZB.Hatchery && (strings.Contains(SZB.Lair) || strings.Contains(SZB.Hive))) return true;
            if (id == SZB.Lair && strings.Contains(SZB.Hive)) return true;
            return false;
        }
    }
}
