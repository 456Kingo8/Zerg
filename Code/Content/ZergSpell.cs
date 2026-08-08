using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using UnityEngine;
using Zerg.Code.Convenience;

namespace Zerg.Code.Content
{
    class ZergSpell
    {
        public static SpellAsset spell_Fungal_Growth; 
        public static SpellAsset spell_Microbial_Shroud;

        public static void init()
        {
            SpellAsset asset = new SpellAsset();
            asset.id = "create_creep_tumor";
            asset.cost_mana = 20;
            asset.can_be_used_in_combat = false;
            asset.cast_entity = CastEntity.Tile;
            asset.cast_target = CastTarget.Himself;
            asset.action = create_creep_tumor_action;
            asset.chance = 1f;
            AssetManager.spells.add(asset);

            asset = new SpellAsset();
            asset.id = "Fungal_Growth";
            asset.cost_mana = 30;
            asset.can_be_used_in_combat = true;
            asset.cast_entity = CastEntity.UnitsOnly;
            asset.cast_target = CastTarget.Enemy;
            asset.action = throw_Fungal_Growth_action;
            asset.chance = 1f;
            AssetManager.spells.add(asset);
            spell_Fungal_Growth = asset;

            asset = new SpellAsset();
            asset.id = "Microbial_Shroud";
            asset.cost_mana = 40;
            asset.can_be_used_in_combat = true;
            asset.cast_entity = CastEntity.UnitsOnly;
            asset.cast_target = CastTarget.Himself;
            asset.action = Microbial_Shroud_action;
            asset.chance = 1f;
            AssetManager.spells.add(asset);
            spell_Microbial_Shroud = asset;


        }

        public static bool create_creep_tumor_action(BaseSimObject pSelf, BaseSimObject pTarget, WorldTile pTile = null)
        {
            if(pSelf.current_tile != null)
            {
                var biome = pSelf.current_tile.getBiome();
                if (biome != null && biome.id == "biome_zerg_creep")
                {
                    if (!Tools.canBuildFrom(pSelf.current_tile, AssetManager.buildings.get(SZB.Creep_Tumor)))
                    {
                        return false;
                    }

                    foreach (Building building in Finder.getBuildingsFromChunk(pSelf.current_tile, 3, 18))
                    {
                        if (building.asset.grow_creep && building.asset.grow_creep_type == "biome_zerg_creep")
                        {
                            return false;
                        }
                    }

                    World.world.buildings.addBuilding(SZB.Creep_Tumor, pSelf.current_tile);
                    return true;
                }
            }
            return false;
        }

        public static bool throw_Fungal_Growth_action(BaseSimObject pSelf, BaseSimObject pTarget, WorldTile pTile = null)
        {
            Tools.throwAtTile("Fungal_Growth", pSelf,pTarget.current_tile);
            return true;
        }

        public static bool Microbial_Shroud_action(BaseSimObject pSelf, BaseSimObject pTarget, WorldTile pTile = null)
        {

            foreach (Actor act in Finder.getUnitsFromChunk(pSelf.current_tile,1,9))
            {
                if(act.kingdom == pSelf.kingdom)
                {
                    act.addStatusEffect("Microbial_Shroud");
                }
            }
            return true;
        }


    }
}
