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
                bool flag = true;
                if (asset.need_house)
                {
                    if (!actor.hasHomeBuilding()) continue;
                    foreach (string id in asset.requirements)
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

                        foreach(Building building in Finder.getBuildingsFromChunk(actor.current_tile, 2, 10))
                        {
                            if(building.asset.id == SZB.Hatchery)
                            {
                                flag = false; 
                                break;
                            }
                        }
                        MonoBehaviour.print("flag" + flag);
                    }

                }
                if (flag && Randy.randomChance(asset.chance))
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

                    MonoBehaviour.print("mutation_start_" + asset.to_id);
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
                }
            }
            actor.die(false);
            return true;
        }
    }
}
