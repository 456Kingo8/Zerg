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
        public static SpellAsset spell_Fungal_Growth; 
        public static SpellAsset spell_Microbial_Shroud; 
        public static SpellAsset spell_Neural_Parasite;
        public static SpellAsset spell_Spawn_Locusts;
        public static SpellAsset spell_Corrosive_Bile;
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
            asset.chance = 0.6f;
            AssetManager.spells.add(asset);
            spell_Fungal_Growth = asset;

            asset = new SpellAsset();
            asset.id = "Microbial_Shroud";
            asset.cost_mana = 40;
            asset.can_be_used_in_combat = true;
            asset.cast_entity = CastEntity.UnitsOnly;
            asset.cast_target = CastTarget.Himself;
            asset.action = Microbial_Shroud_action;
            asset.chance = 0.6f;
            AssetManager.spells.add(asset);
            spell_Microbial_Shroud = asset;

            asset = new SpellAsset();
            asset.id = "Neural_Parasite";
            asset.cost_mana = 60;
            asset.can_be_used_in_combat = true;
            asset.cast_entity = CastEntity.UnitsOnly;
            asset.cast_target = CastTarget.Enemy;
            asset.action = Neural_Parasite_action;
            asset.chance = 0.6f;
            AssetManager.spells.add(asset);
            spell_Neural_Parasite = asset;

            asset = new SpellAsset();
            asset.id = "Spawn_Locust";
            asset.cost_mana = 1;
            asset.can_be_used_in_combat = true;
            asset.cast_entity = CastEntity.Both;
            asset.cast_target = CastTarget.Himself;
            asset.action = Spawn_Locusts_action;
            asset.chance = 1f;
            AssetManager.spells.add(asset);
            spell_Spawn_Locusts = asset;

            asset = new SpellAsset();
            asset.id = "Corrosive_Bile";
            asset.cost_mana = 25;
            asset.can_be_used_in_combat = true;
            asset.cast_entity = CastEntity.UnitsOnly;
            asset.cast_target = CastTarget.Enemy;
            asset.action = Corrosive_Bile_action;
            asset.chance = 1f;
            AssetManager.spells.add(asset);
            spell_Corrosive_Bile = asset;
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

                    Building build = World.world.buildings.addBuilding(SZB.Creep_Tumor, pSelf.current_tile);
                    if(pSelf.kingdom != null) build.setKingdom(pSelf.kingdom);
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

        public static bool Neural_Parasite_action(BaseSimObject pSelf, BaseSimObject pTarget, WorldTile pTile = null)
        {
            if (pSelf.a.hasStatus("Neural_Parasite_CD")) return false;
            if(pTarget != null && pTarget.isActor())
            {
                //if(pTarget.a.hasTrait("immune")) return false;
                if(pTarget.a.hasStatus("Neural_Parasite")) return false;
                if(pTarget.a.data.health <= 200) return false;
                pTarget.a.SetOriginal_kingdom(pTarget.a.kingdom.id);
                pTarget.a.setKingdom(pSelf.a.kingdom);
                pTarget.a.addStatusEffect("Neural_Parasite");
                pSelf.a.addStatusEffect("Neural_Parasite_CD");
                pTarget.a.clearAttackTarget();
                pSelf.a.clearAttackTarget();
                pTarget.a.finishStatusEffect("angry");

            }
            return true;
        }

        public static bool Spawn_Locusts_action(BaseSimObject pSelf, BaseSimObject pTarget, WorldTile pTile = null)
        {
            if (pSelf.a.hasStatus("Spawn_Locusts_CD")) return false;
            pSelf.a.addStatusEffect("Spawn_Locusts_CD");
            for (int i = 0; i < 4; i++)
            {
                Tools.throwAtTile("Spawn_Locusts", pSelf.a, pSelf.current_tile.neighboursAll.GetRandom().neighboursAll.GetRandom());
            }
            return true;
        }

        public static bool Corrosive_Bile_action(BaseSimObject pSelf, BaseSimObject pTarget, WorldTile pTile = null)
        {
            if(pTarget.current_tile == null) return false;
            World.world.drop_manager.spawn(pTarget.current_tile, "Corrosive_Bile", pCasterId: pSelf.a.id);
            return true;
        }
    }
}
