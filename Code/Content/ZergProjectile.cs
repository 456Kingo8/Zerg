using System;
using System.Collections.Generic;
using System.Text;

namespace Zerg.Code.Content
{
    class ZergProjectile
    {
        public static void init()
        {
            ProjectileAsset asset = AssetManager.projectiles.clone("zerg_spine", "arrow");
            asset.mass = 1;
            asset.speed = 60;

        }

    }
}
