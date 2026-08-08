using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using UnityEngine;
using Zerg.Code.Convenience;
using Zerg.Code.Extend;

namespace Zerg.Code.Content
{
    class ZergSpell
    {

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







    }
}
