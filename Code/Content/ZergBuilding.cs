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
            asset.has_ruin_state = false;
            asset.has_kingdom_color = true;
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
            asset.transform_tiles_to_top_tiles = "zerg_creep_low";
            asset.main_path = String.Empty;
            asset.prevent_freeze = true;
            asset.removed_by_sponge = false;

            asset = AssetManager.buildings.clone(SZB.Hatchery, "$zerg_building$");
            asset.base_stats[S.health] = 1500;
            SetFootprint(asset, 11, 7);
            asset.spawn_units = true;
            asset.spawn_units_asset = SZA.Larva;
            AssetManager.buildings.setGrowBiomeAround("biome_zerg_creep", 32, 10, 0.1f, CreepWorkerMovementType.RandomNeighbourAll);
            asset.grow_creep_direction_random_position = true;
            asset.grow_creep_flash = false;
            asset.grow_creep_redraw_tile = true;
            list.Add(asset.id);

            asset = AssetManager.buildings.clone(SZB.Spawning_Pool, "$zerg_building$");
            asset.base_stats[S.health] = 500;
            SetFootprint(asset, 8, 6);
            list.Add(asset.id);

            asset = AssetManager.buildings.clone(SZB.Lair, SZB.Hatchery);
            asset.base_stats[S.health] = 3000;
            list.Add(asset.id);

            asset = AssetManager.buildings.clone(SZB.Baneling_Nest, "$zerg_building$");
            asset.base_stats[S.health] = 800;
            SetFootprint(asset, 6, 4);
            list.Add(asset.id);

            asset = AssetManager.buildings.clone(SZB.Spire, "$zerg_building$");
            asset.base_stats[S.health] = 800;
            SetFootprint(asset, 6, 4);
            list.Add(asset.id);

            asset = AssetManager.buildings.clone(SZB.Infestation_Pit, "$zerg_building$");
            asset.base_stats[S.health] = 800;
            SetFootprint(asset, 6, 4);
            list.Add(asset.id);


            asset = AssetManager.buildings.clone(SZB.Creep_Tumor, "$zerg_building$");
            asset.base_stats[S.health] = 150;
            SetFootprint(asset, 3, 3);
            AssetManager.buildings.setGrowBiomeAround("biome_zerg_creep", 20, 6, 0.15f, CreepWorkerMovementType.Direction);
            asset.grow_creep_direction_random_position = true;
            asset.grow_creep_flash = false;
            asset.grow_creep_redraw_tile = true;
            list.Add(asset.id);

            foreach (string id in list)
            {
                BuildingAsset asset1 = AssetManager.buildings.get(id);
                asset1.sprite_path = "building/t_" + id;
                asset1.has_sprites_main = true;
                asset1.atlas_asset = AssetManager.dynamic_sprites_library.get("building_shadows");
            }



        }

        //修仙来的神秘方法 赞美Inmny 
        //https://github.com/inmny/Cultiway-Reborn/blob/master/Source/Content/Buildings.cs
        private static void SetFootprint(BuildingAsset asset, int width, int height)
        {
            int left = width / 2;
            asset.fundament = new BuildingFundament(left, width - left - 1, height - 1, 0);
        }
    }
}
