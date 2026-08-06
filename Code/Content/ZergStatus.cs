using System;
using System.Collections.Generic;
using System.Text;
using Zerg.Code.Convenience;

namespace Zerg.Code.Content
{
    class ZergStatus
    {
        public static void init()
        {
            StatusAsset asset = new StatusAsset();
            asset.id = "Zerg_Mutation";
            asset.duration = 3f;
            asset.action_interval = 0.2f;
            asset.action = Turning;
            asset.action_finish = Turning_end;
            asset.base_stats = new BaseStats();
            asset.base_stats[S.speed] = -2147483648;
            asset.path_icon = "ui/icons/iconZerg";
            asset.locale_description = "Zerg_Mutation_des";
            asset.locale_id = "Zerg_Mutation_id";
            AssetManager.status.add(asset);


        }

        public static bool Turning(BaseSimObject pTarget, WorldTile pTile = null!)
        {
            int i = 10;
            pTarget.a.data.health += i;
            pTarget.a.stats[S.health] += i;
            pTarget.a.spawnParticle(Toolbox.color_heal);
            return true;
        }
        public static bool Turning_end(BaseSimObject pTarget, WorldTile pTile = null!)
        {
            pTarget.a.Zerg_endMutation();
            return true;
        }
    }
}
