using ai.behaviours;
using AOT;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using Zerg.Code.Convenience;
using static UnityEngine.GraphicsBuffer;

namespace Zerg.Code.Behaviour
{
    class BehZergCall : BehaviourActionActor
    {
        public override BehResult execute(Actor pActor)
        {
            if (pActor.attack_target != null)
            {
                int cnt = 0;
                if (pActor.asset.id == SZA.Overlord) cnt = 50;
                else if (pActor.asset.id == SZA.Overseer) cnt = 100;
                else cnt = 50;

                if(pActor.hasHomeBuilding())
                {
                    foreach(long id in pActor.home_building.residents)
                    {
                        if (cnt == 0) break;
                        Actor actor = World.world.units.get(id);
                        if(!actor.has_attack_target && actor.asset.id != SZA.Larva && actor != pActor)
                        {
                            actor.attack_target = pActor.attack_target;
                            if(pActor.attack_target.current_tile != null) actor.goTo(pActor.attack_target.current_tile,true);
                            cnt--;
                        }
                    }
                }
                else
                {
                    foreach (Actor actor in Finder.getUnitsFromChunk(pActor.current_tile, 4, 64))
                    {
                        if (cnt == 0) break;
                        if (actor.kingdom == pActor.kingdom && !actor.has_attack_target && actor.hasTag("Zerg")&& actor.asset.id != SZA.Larva && actor != pActor)
                        {
                            actor.attack_target = pActor.attack_target;
                            if (pActor.attack_target.current_tile != null) actor.goTo(pActor.attack_target.current_tile, true);
                            cnt--;
                        }
                    }
                }
            }
            return BehResult.Continue;
        }
    }
}
