using System;
using System.Collections.Generic;
using System.Text;

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
            trait.action_attack_target = zerg_attack_explode_attack;
            trait.action_death = zerg_attack_explode_death;
            AssetManager.traits.add(trait);
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
                    building.getHit(100, false, AttackType.Acid, pSkipIfShake: false);
                    building.getHit(150, true, AttackType.Other, pSkipIfShake: false);
                }
            }

            return true;
        }



    }
}
