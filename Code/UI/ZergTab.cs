using ModDeclaration;
using NeoModLoader.General;
using NeoModLoader.General.UI.Tab;
using System;
using System.Collections.Generic;
using System.Text;
using Zerg.Code.Content;
using UnityEngine;

namespace Zerg.Code.UI
{
    class ZergTab
    {
        public static PowersTab zerg_tab = new();
        public static void init()
        {
            zerg_tab = TabManager.CreateTab("Zerg_Rebuild", "tab_Zerg", "tab for Zerg",
            SpriteTextureLoader.getSprite("ui/icons/iconZergTab"));
            zerg_tab.SetLayout(new List<string>()
            {
            "creature_actor",
            "creature_building",
            "god_powers",
            "laws"
            });

            addActorButton();
            addBuildingButton();

        }

        internal static void addActorButton()
        {
            foreach (string id in ZergActor.list)
            {
                var godPower = new GodPower
                {
                    id = $"spawn{id}",
                    show_spawn_effect = true,
                    actor_spawn_height = 3f,
                    name = $"spawn{id}",
                    actor_asset_id = id,
                    click_action = AssetManager.powers.spawnUnit
                };
                AssetManager.powers.add(godPower);
                AssetManager.actor_library.get(id).power_id = godPower.id;
                zerg_tab.AddPowerButton("creature_actor",
                PowerButtonCreator.CreateGodPowerButton(godPower.id,
                SpriteTextureLoader.getSprite("ui/icons/tab/" + id)));
            }
        }

        internal static void addBuildingButton()
        {
            foreach (string id in ZergBuilding.list)
            {
                var drop = AssetManager.drops.clone("spawn" + id, "stone");
                drop.building_asset = id;
                drop.action_landed = new DropsAction(DropsLibrary.action_spawn_building);
                drop.sound_drop = "event:/SFX/DROPS/DropTumor";
                drop.default_scale = 0.2f;
                drop.falling_speed = 3f;
                drop.falling_height = new Vector2(30f, 30f);

                var godPower = AssetManager.powers.clone("spawn" + id, "$template_drop_building$");
                godPower.drop_id = "spawn" + id;
                godPower.cached_drop_asset = drop;
                godPower.name = id;
                zerg_tab.AddPowerButton("creature_building",
                PowerButtonCreator.CreateGodPowerButton(godPower.id,
                SpriteTextureLoader.getSprite("ui/icons/tab/" + id)));
            }
        }
    }
}
