using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using UnityEngine;
using HarmonyLib;
using Zerg.Code.Convenience;

namespace Zerg.Code.Content
{
    class ZergTrait
    {
        public static void init()
        {
            ActorTrait trait = new ActorTrait
            {
                id = "zerg_attack_explode",
                rate_birth = 0,
                path_icon = "ui/Icons/iconZerg",
                can_be_given = false,
                group_id = "special"
            };
            addLocale(trait);
            trait.action_attack_target = zerg_attack_explode_attack;
            trait.action_death = zerg_attack_explode_death;
            AssetManager.traits.add(trait);

            trait = new ActorTrait
            {
                id = "zerg_ultar_unit",
                rate_birth = 0,
                path_icon = "ui/Icons/iconZerg",
                can_be_given = true,
                group_id = "special"
            };
            addLocale(trait);
            AssetManager.traits.add(trait);
            List<string> list = new List<string>() { "tantrum", "confused", "angry", "stunned", "slowness", "frozen", "Neural_Parasite" };
            foreach (string item in list)
            {
                Tools.trait_add_to_status_array(item, "zerg_ultar_unit");
            }

            trait = new ActorTrait
            {
                id = "zerg_armored_unit",
                rate_birth = 0,
                path_icon = "ui/Icons/iconZerg",
                can_be_given = true,
                group_id = "special",
            };
            trait.base_stats = new();
            trait.base_stats[S.armor] = 20f;
            addLocale(trait);
            AssetManager.traits.add(trait);

            trait = new ActorTrait
            {
                id = "zerg_frenzied",
                rate_birth = 0,
                path_icon = "ui/Icons/iconZerg",
                can_be_given = false,
                group_id = "special",
            };
            addLocale(trait);
            AssetManager.traits.add(trait);
            foreach (string item in list)
            {
                Tools.trait_add_to_status_array(item, "zerg_frenzied");
            }
            if(ZergMain.linked_mod)
            {
                trait = new ActorTrait
                {
                    id = "zerg_infinite_evolution",
                    rate_birth = 0,
                    path_icon = "ui/Icons/iconGeneMutation",
                    can_be_given = false,
                    group_id = "special",
                };
                trait.base_stats = new();

#if WARRIOR
                trait.base_stats["Accuracy"] = 10f;
#endif

#if THEFANTASYWORLD
                trait.base_stats["health"] = 100f;
                trait.base_stats[S.multiplier_health] = 2f;
                trait.base_stats["BaseDamage"] = 30f;
                trait.base_stats["MagicalEnergy"] = 50f;
                trait.base_stats["DodgeEvade"] = 10f;
                trait.base_stats["hitthetarget"] = 20f;
                trait.base_stats["MagicApplication"] = 4f;
                trait.base_stats["MagicShield"] = 30f;
                trait.base_stats["FixedPhysicalDamage"] = 20f;
                trait.base_stats["Restorehealth"] = 10f;
                trait.base_stats["MagicReply"] = 10f;
                trait.base_stats["MagicResistance"] = 3f;
                trait.base_stats["PhysicalDefense"] = 3f;
                trait.base_stats["MagicDamage"] = 5f;
                trait.base_stats["PhysicalDamage"] = 20f;
                
#endif

#if CULTIWAY
                trait.base_stats["health"] += 20f;
                trait.base_stats[S.multiplier_health] += 2f;
                trait.base_stats[S.multiplier_damage] += 0.5f;
                trait.base_stats["mana"] = 100f;
#endif

                addLocale(trait);
                AssetManager.traits.add(trait);

            }

        }


        public static bool zerg_attack_explode_attack(BaseSimObject pSelf,BaseSimObject pTarget, WorldTile pTile = null)
        {
            zerg_attack_explode_death(pSelf,pTile);
            pSelf.a.die(false);
            return true;
        }

        public static bool zerg_attack_explode_death(BaseSimObject pTarget, WorldTile pTile = null)
        {
            foreach(Actor actor in Finder.getUnitsFromChunk(pTarget.a.current_tile,1,4))
            {
                if(actor.kingdom != pTarget.a.kingdom)
                {
                    actor.getHit(75, false, AttackType.Acid, pSkipIfShake: false);
                    actor.getHit(50, true, AttackType.Other, pSkipIfShake: false);
                }
            }
            foreach (Building building in Finder.getBuildingsFromChunk(pTarget.a.current_tile, 1, 5))
            {
                if (building.kingdom != pTarget.a.kingdom)
                {
                    building.getHit(150, false, AttackType.Acid, pSkipIfShake: false);
                    building.getHit(100, true, AttackType.Other, pSkipIfShake: false);
                }
            }

            return true;
        }

        public static void addLocale(ActorTrait trait)
        {
            trait.has_locales = true;
            trait.has_localized_id = true;
            trait.has_description_1 = true;
            trait.has_description_2 = true;
            trait.special_locale_id = trait.id + "_id";
            trait.special_locale_description = trait.id + "_des1";
            trait.special_locale_description_2 = trait.id + "_des2";
        }

    }
}
