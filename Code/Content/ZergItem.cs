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
            asset.base_stats[S.damage_range] = 1f;

            asset = AssetManager.items.clone("glaive_wurm", "$range");
            asset.projectile = "glaive_wurm_0";
            asset.material = "base";
            asset.attack_type = WeaponType.Range;
            asset.path_icon = "ui/icons/iconZerg";
            asset.base_stats[S.projectiles] = 1;
            asset.base_stats[S.damage_range] = 0.95f;

            asset = AssetManager.items.clone("zerg_none_attack", "$range");
            asset.projectile = "zerg_spine";
            asset.base_stats[S.projectiles] = 0;
        }

    }
}
