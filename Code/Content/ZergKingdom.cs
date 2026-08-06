using System;
using System.Collections.Generic;
using System.Text;

namespace Zerg.Code.Content
{
    class ZergKingdom
    {

        public static Kingdom Zerg_wild = new();
        public static void init()
        {
            KingdomAsset Zerg = new KingdomAsset();
            Zerg.id = "Zerg";
            Zerg.count_as_danger = true;
            Zerg.mobs = true;
            Zerg.civ = false;
            Zerg.default_kingdom_color = new ColorAsset("#AD00B9");
            AssetManager.kingdoms.add(Zerg);
            Zerg_wild = World.world.kingdoms_wild.newWildKingdom(Zerg);
        }
    }
}
