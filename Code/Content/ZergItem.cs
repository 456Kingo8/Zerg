using System;
using System.Collections.Generic;
using System.Text;
using Zerg.Code.Convenience;

namespace Zerg.Code.Content
{
    class ZergItem
    {
        public static void init()
        {
            EquipmentAsset asset = AssetManager.items.clone("zerg_spine", "$range");
            asset.projectile = "zerg_spine";
            asset.material = "base";
            asset.attack_type = WeaponType.Range;
            asset.path_icon = "ui/icons/iconZerg";
            asset.base_stats[S.projectiles] = 1;
            asset.base_stats[S.range] = 10;
            asset.base_stats["damage_range"] = 0.15f;


        }

    }
}
