using ModDeclaration;
using NeoModLoader.General;
using NeoModLoader.General.UI.Tab;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using Zerg.Code.Content;
using Zerg.Code.Convenience;

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

            addSpecialButton();

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
                drop.falling_speed = 4f;
                drop.falling_height = new Vector2(30f, 30f);

                var godPower = AssetManager.powers.clone("spawn" + id, "$template_drop_building$");
                godPower.drop_id = "spawn" + id;
                godPower.cached_drop_asset = drop;
                godPower.name = "spawn" + id;
                zerg_tab.AddPowerButton("creature_building",
                PowerButtonCreator.CreateGodPowerButton(godPower.id,
                SpriteTextureLoader.getSprite("ui/icons/tab/" + id)));
            }
        }

        internal static void addSpecialButton()
        {
            var godPower = new GodPower
            {
                id = "spawn_special_drone",
                show_spawn_effect = true,
                actor_spawn_height = 3f,
                name = "spawn_special_drone",
                actor_asset_id = SZA.Drone,
                click_action = spawn_special_drone_action
            };
            AssetManager.powers.add(godPower);
            zerg_tab.AddPowerButton("god_powers",
            PowerButtonCreator.CreateGodPowerButton(godPower.id,
            SpriteTextureLoader.getSprite("ui/icons/tab/Drone")));

            CreateNewToggleButton("show_zones_zerg_law", "Zerg",false);
            CreateNewToggleButton("zerg_infinite_evolution_law", "Zerg",true);




        }


        private static void CreateNewToggleButton(string id, string path_icon, bool default_value)
        {
            AssetManager.options_library.add(new OptionAsset
            {
                id = id,
                default_bool = default_value,
                type = OptionType.Bool
            });
            PlayerOptionData option = PlayerConfig.instance.data.add(new PlayerOptionData(id)
            {
                boolVal = default_value
            });


            GodPower power = new GodPower();
            power.id = id;
            power.name = id;
            power.toggle_name = id;
            AssetManager.powers.add(power);


            ZergTab.zerg_tab.AddPowerButton("laws", PowerButtonCreator.CreateToggleButton(id, SpriteTextureLoader.getSprite("ui/icons/tab/" + path_icon)));
        }


        public static bool spawn_special_drone_action(WorldTile pTile, string pPowerID)
        {
            GodPower godPower = AssetManager.powers.get(pPowerID);
            MusicBox.playSound("event:/SFX/UNIQUE/SpawnWhoosh", pTile.pos.x, pTile.pos.y);
            EffectsLibrary.spawn("fx_spawn", pTile); 
            Actor act = World.world.units.spawnNewUnitByPlayer(godPower.actor_asset_id, pTile, pSpawnSound: true, pMiracleSpawn: true, godPower.actor_spawn_height);
            if (pTile?.zone?.city?.kingdom != null)
            {
                act.setKingdom(pTile.zone.city.kingdom);
            }
            return true;
        }



    }
}
