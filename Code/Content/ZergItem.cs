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
            asset.base_stats[S.damage_range] = 1f;
            asset.show_for_unlockables_ui = false;
            asset.show_in_knowledge_window = false;
            asset.show_in_meta_editor = false;

            asset = AssetManager.items.clone("zerg_spine_locust", "$range");
            asset.projectile = "zerg_spine_locust";
            asset.material = "base";
            asset.attack_type = WeaponType.Range;
            asset.path_icon = "ui/icons/iconZerg";
            asset.base_stats[S.projectiles] = 8;
            asset.base_stats[S.damage_range] = 1f;
            asset.show_for_unlockables_ui = false;
            asset.show_in_knowledge_window = false;
            asset.show_in_meta_editor = false;

            asset = AssetManager.items.clone("glaive_wurm", "$range");
            asset.projectile = "glaive_wurm_0";
            asset.material = "base";
            asset.attack_type = WeaponType.Range;
            asset.path_icon = "ui/icons/iconZerg";
            asset.base_stats[S.projectiles] = 1;
            asset.base_stats[S.damage_range] = 0.95f;
            asset.show_for_unlockables_ui = false;
            asset.show_in_knowledge_window = false;
            asset.show_in_meta_editor = false;

            asset = AssetManager.items.clone("acid_saliva", "$range");
            asset.projectile = "acid_saliva";
            asset.material = "base";
            asset.attack_type = WeaponType.Range;
            asset.path_icon = "ui/icons/iconZerg";
            asset.base_stats[S.projectiles] = 24;
            asset.base_stats[S.damage_range] = 0.95f;
            asset.show_for_unlockables_ui = false;
            asset.show_in_knowledge_window = false;
            asset.show_in_meta_editor = false;

            asset = AssetManager.items.clone("plasma_discharge", "acid_saliva");
            asset.projectile = "plasma_discharge";
            asset.base_stats[S.projectiles] = 20;

            asset = AssetManager.items.clone("zerg_none_attack", "$range");
            asset.projectile = "zerg_spine";
            asset.base_stats[S.projectiles] = -10f;
        }

    }
}
