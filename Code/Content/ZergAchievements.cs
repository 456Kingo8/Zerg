using System;
using System.Collections.Generic;
using System.Text;
using Zerg.Code.Convenience;

namespace Zerg.Code.Content
{
    class ZergAchievements
    {
        public static Achievement Zerg_World;
        public static void init()
        {
            Achievement achievement = new Achievement();
            achievement.id = "Zerg_World";
            achievement.action = Zerg_World_check;
            achievement.icon = "ui/icons/iconZergWorld";
            achievement.locale_key = "zerg_world";
            achievement.hidden = true;
            achievement.group = "destruction";
            AssetManager.achievements.add(achievement);
            Zerg_World = achievement;

            AssetManager.achievement_groups.dict["destruction"].achievements_list.Add(achievement);
        }


        private static bool Zerg_World_check(object pCheckData)
        {
            float tCount = (float)(ZergBiome.creep_high.hashset.Count + ZergBiome.creep_low.hashset.Count);
            if (tCount == 0f)
            {
                return false;
            }
            float tTotalLand = (float)Tools.getAllBiomeTopTileCount();
            return tCount / tTotalLand >= 0.75f && World.world.kingdoms_wild.get("Zerg").units.Count >= 800;
        }
    }
}
