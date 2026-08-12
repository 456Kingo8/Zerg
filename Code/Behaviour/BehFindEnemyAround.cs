using ai.behaviours;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using Zerg.Code.Framework;

namespace Zerg.Code.Behaviour
{
    class BehFindEnemyAround : BehaviourActionActor
    {
        public override BehResult execute(Actor pActor)
        {

            if (pActor.attack_target != null) return BehResult.Continue;

            BaseSimObject target = null;
            foreach (Building building in Finder.getBuildingsFromChunk(pActor.current_tile,4,64))
            {
                if (building.kingdom != null && building.isUsable() && building.kingdom.isEnemy(pActor.kingdom))
                {
                    target = building; 
                    break;
                }
            }

            if (target != null)
            {
                pActor.tile_target = target.current_tile;
                if(target.current_tile != null)pActor.goTo(pActor.tile_target);

                return BehResult.Continue;
            }
            foreach(Actor actor in Finder.getUnitsFromChunk(pActor.current_tile,4,64))
            {
                if(actor.kingdom != null &&actor.isAlive()&&actor.kingdom.isEnemy(pActor.kingdom))
                {
                    target = actor; break;
                }
            }
            if (target != null)
            {
                pActor.tile_target = target.current_tile;
                if (target.current_tile != null) pActor.goTo(pActor.tile_target);
            }
            return BehResult.Continue;

        }
    }
}
