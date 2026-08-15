using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using Zerg.Code.Content;
using Zerg.Code.Convenience;

namespace Zerg.Code.Framework
{
    class ZergEraManager
    {
        public static bool is_zerg_era
        {
            get
            {
                return World.world.era_manager.getCurrentAge().id == "age_zerg";
            }
        }

        public static void init()
        {
            return;//并没有实际作用
        }

        public static void check()
        {
            PlayerConfig.dict.TryGetValue("zerg_auto_era_law", out var option);
            if (option?.boolVal != true) return;


            float tCount = (float)(ZergBiome.creep_high.hashset.Count + ZergBiome.creep_low.hashset.Count);
            float tTotalLand = (float)Tools.getAllBiomeTopTileCount();
            float val = tCount / tTotalLand;

            if (is_zerg_era && val < 0.4f)
            {
                World.world.era_manager.startNextAge();
            }
            else if (!is_zerg_era && val > 0.6f)
            {
                World.world.era_manager.setCurrentAge(ZergEra.Zerg_Era, true);
            }
        }

        public static void clear()
        {
            return;
        }


    }
}
