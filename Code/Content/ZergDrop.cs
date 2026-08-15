using System;
using System.Collections.Generic;
using System.Text;
using static System.Net.WebRequestMethods;

namespace Zerg.Code.Content
{
    class ZergDrop
    {
        public static void init()
        {
            DropAsset asset = AssetManager.drops.clone("Corrosive_Bile", "lava");
            asset.falling_random_x_move = false;
            asset.action_landed = null;
            asset.falling_speed = 6.4f;
            asset.action_landed_drop = action_Corrosive_Bile;

        }

        public static void action_Corrosive_Bile(Drop pDrop, WorldTile pTile = null, string pDropID = null)
        {
            long tCasterId = pDrop.getCasterId();
            Actor tCaster = World.world.units.get(tCasterId);
            if (tCaster.isRekt()) return;

            foreach (Actor tActor in Finder.getUnitsFromChunk(pTile, 1, 2f, false))
            {
                if (tCaster.kingdom.isEnemy(tActor.kingdom))
                {
                    tActor.getHit(150,true,AttackType.Other,tCaster,false,false,false);
                }
            }
        }
    }
}
