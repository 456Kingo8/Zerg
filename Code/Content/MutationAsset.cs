using System;
using System.Collections.Generic;
using System.Text;
using Zerg.Code.Convenience;

namespace Zerg.Code.Content
{
    public class MutationAsset
    {
        public string from_id = "Larva";

        public string to_id = "Drone";

        public List<string> building_requirements = new List<string>(); //需求的前置建筑

        public float chance = 0f;

        public float cost_time = 10f;

        public int num = 1;

        public string coco_id = SZB.Cocoons_land_Actor;

        public bool need_house = true;

        public bool building = false;

        public bool need_biome = false;
    }
}
