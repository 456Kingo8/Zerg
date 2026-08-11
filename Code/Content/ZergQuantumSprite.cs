using NCMS.Utils;
using NeoModLoader.General.UI.Tab;
using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;
using UnityEngine;

namespace Zerg.Code.Content
{
    class ZergQuantumSprite
    {
        public static void init()
        {
            QuantumSpriteAsset asset = new QuantumSpriteAsset();
            asset.id = "show_zones_zerg";
            asset.id_prefab = "p_mapZone";
            asset.base_scale = 1f;
            asset.draw_call = DrawZerg;
            asset.debug_option = DebugOption.Nothing;
            asset.render_map = true;
            asset.add_camera_zoom_multiplier = false;
            asset.color = Toolbox.makeColor("#AD00B9", 0.1f);
            createDroupSystems(asset);
            AssetManager.quantum_sprites.add(asset);

        }

        public static void DrawZerg(QuantumSpriteAsset pAsset)
        {
            PlayerConfig.dict.TryGetValue("show_zones_zerg_law", out var option);
            if (option == null || !option.boolVal) return;
            foreach (Actor unit in World.world.units)
            {
                if (unit != null && unit.hasTag("Zerg"))
                {
                    if(unit.kingdom != null)
                    {
                        var vector = unit.current_tile.zone.centerTile.posV;
                        QuantumSprite tQSprite = pAsset.group_system.getNext();
                        Color color = unit.kingdom.getColor()._color_main;
                        Color color1 = new Color(color.r, color.g, color.b, 0.1f);
                        tQSprite.setColor(ref color1);
                        tQSprite.set(ref vector, pAsset.base_scale);
                    }
                }
            }
            foreach (Building building in World.world.buildings)
            {
                if (building != null && building.asset.kingdom == "Zerg")
                {
                    if (building.kingdom != null)
                    {
                        var vector = building.current_tile.zone.centerTile.posV;
                        QuantumSprite tQSprite = pAsset.group_system.getNext();
                        Color color = building.kingdom.getColor()._color_main;
                        Color color1 = new Color(color.r, color.g, color.b, 0.4f);
                        tQSprite.setColor(ref color1);
                        tQSprite.set(ref vector, pAsset.base_scale);
                    }
                }
            }
        }

        private static void createDroupSystems(QuantumSpriteAsset tAsset)
        {
            QuantumSpriteGroupSystem tGroup = new GameObject().AddComponent<QuantumSpriteGroupSystem>();
            tGroup.create(tAsset);
            tAsset.group_system = tGroup;
            tAsset.group_system.turn_off_renderer = tAsset.turn_off_renderer;
            if (Config.preload_quantum_sprites && tAsset.default_amount != 0)
            {
                for (int i = 0; i < tAsset.default_amount; i++)
                {
                    tGroup.getNext();
                }
                tGroup.clearFull();
            }
        }

    }
}
