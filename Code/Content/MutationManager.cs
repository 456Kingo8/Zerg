using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using Zerg.Code.Convenience;
using Zerg.Code.Extend;

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

            List<string> ids = new List<string>();

            if(actor.hasHomeBuilding())ids = actor._home_building.component_unit_spawner.GetExtend();

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
                    if (ids.Contains(asset.to_id)) continue;//不重复建造同种建筑

                    foreach (string id in asset.building_requirements)
                    {
                        if (!ids.Contains(id))
                        {
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
                    if (actor.hasCity()) coco.setCity(actor.city);
                    if (actor.hasHomeBuilding()) coco.setHomeBuilding(actor.home_building);
                    actor.die(true);
                    if(asset.building && actor.hasHomeBuilding()) actor._home_building.component_unit_spawner.GetExtend().Add(asset.to_id);//提前占位防止重复变异，后续id由Patches中的判定维持存在
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
                    if (actor.hasCity()) act.setCity(actor.city);
                    if (actor.hasHomeBuilding()) act.setHomeBuilding(actor.home_building);

                    if (id == SZA.Drone && Randy.randomChance(0.5f))
                    {
                        act.clearHomeBuilding();
                        act.beh_tile_target = act.current_chunk.neighbours.GetRandom().tiles.GetRandom();
                    }

                }
            }
            actor.die(false);
            return true;
        }
    }
}
