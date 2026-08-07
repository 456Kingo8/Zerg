using System;
using System.Collections.Generic;
using System.Text;
using Zerg.Code.Convenience;
using Zerg.Code.Extend;

namespace Zerg.Code.Content
{
    public static class MutationLibrary
    {
        public static List<MutationAsset> list = new List<MutationAsset>();
        public static Dictionary<string,List<MutationAsset>> from_dict = new Dictionary<string, List<MutationAsset>>();

        public static void init()
        {
            MutationAsset asset = new MutationAsset();
            asset.from_id = SZA.Larva;
            asset.to_id = SZA.Drone;
            asset.chance = 0.2f;
            asset.cost_time = 4f;
            asset.building_requirements = new List<string>();
            asset.need_house = false;
            list.Add(asset);

            asset = new MutationAsset();
            asset.from_id = SZA.Drone;
            asset.to_id = SZB.Hatchery;
            asset.chance = 0.2f;
            asset.cost_time = 60f;
            asset.coco_id = SZB.Cocoons_Building;
            asset.need_house = false;
            asset.building = true;
            list.Add(asset);

            asset = new MutationAsset();
            asset.from_id = SZA.Drone;
            asset.to_id = SZB.Spawning_Pool;
            asset.chance = 0.2f;
            asset.cost_time = 10f;
            asset.coco_id = SZB.Cocoons_Building;
            asset.need_biome = true;
            asset.need_house = true;
            asset.building_requirements = new List<string> {SZB.Hatchery};
            asset.building = true;
            list.Add(asset);


            asset = new MutationAsset();
            asset.from_id = SZA.Larva;
            asset.to_id = SZA.Zergling;
            asset.chance = 0.2f;
            asset.cost_time = 5f;
            asset.num = 2;
            asset.need_house = true;
            asset.building_requirements = new List<string>() { SZB.Spawning_Pool};
            list.Add(asset);




            foreach (MutationAsset asset1 in list)
            {
                if(from_dict.ContainsKey(asset1.from_id))
                {
                    from_dict[asset1.from_id].Add(asset1);
                }
                else
                {
                    from_dict.Add(asset1.from_id,new List<MutationAsset> {asset1});
                }
            }
        }
    }
}
