using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace Zerg.Code.Content
{
    class ZergEra
    {
        public static WorldAgeAsset Zerg_Era;


        public static void init()
        {
            WorldAgeAsset asset = AssetManager.era_library.clone("age_zerg", "age_hope");
            asset.bonus_loyalty = 200;
            asset.bonus_opinion = 200;
            asset.path_icon = "ui/icons/iconZergEra";
            asset.title_color = Toolbox.makeColor("#AD00B9");
            asset.era_effect_overlay_alpha = 0.2f;
            asset.years_min = 30;
            asset.overlay_rain_darkness = true;
            asset.years_max = 30;
            asset.cloud_interval = 20f;
            Zerg_Era = asset;
        }
    }
}
