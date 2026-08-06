using System;
using System.Collections.Generic;
using System.Text;
using Zerg.Code.Convenience;

namespace Zerg.Code.Content
{
    internal class ZergBuilding
    {
        public static List<string> list = new List<string>();


        public static void init()
        {
            BuildingAsset asset = AssetManager.buildings.clone("$zerg_building$", "$building_creep$");
            asset.has_sprites_main_disabled = false;
            asset.has_sprites_ruin = false;
            asset.has_sprites_spawn = false;
            asset.has_sprite_construction = false;
            asset.sound_idle = "event:/SFX/BUILDINGS_IDLE/IdleTumor";
            asset.sound_hit = "event:/SFX/HIT/HitFlesh";
            asset.sound_built = "event:/SFX/BUILDINGS/SpawnBuildingFlesh";
            asset.sound_destroyed = "event:/SFX/BUILDINGS/DestroyBuildingFlesh";
            asset.shadow = false;
            asset.group = "Zerg";
            asset.kingdom = "Zerg";
            asset.can_be_placed_on_blocks = false;
            asset.can_be_placed_on_liquid = false;
            asset.ignore_buildings = true;
            asset.check_for_close_building = false;
            asset.can_be_living_house = false;
            asset.transform_tiles_to_top_tiles = "tumor_low";
            asset.main_path = String.Empty;



            asset = AssetManager.buildings.clone(SZB.Hatchery, "$zerg_building$");
            asset.spawn_units = true;
            asset.spawn_units_asset = SZA.Larva;
            asset.housing_slots = 5;
            AssetManager.buildings.setGrowBiomeAround("biome_tumor", 16, 6, 0.1f, CreepWorkerMovementType.Direction);
            asset.grow_creep_direction_random_position = true;
            asset.grow_creep_flash = true;
            asset.grow_creep_redraw_tile = true;
            list.Add(asset.id);

            foreach(string id in list)
            {
                BuildingAsset asset1 = AssetManager.buildings.get(id);
                asset1.sprite_path = "building/t_" + id;
                asset1.has_sprites_main = true;
                asset1.atlas_asset = AssetManager.dynamic_sprites_library.get("building_shadows");
            }



        }
    }
}
