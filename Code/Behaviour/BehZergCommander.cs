using ai.behaviours;
using System;
using System.Collections.Generic;
using System.Text;

namespace Zerg.Code.Behaviour
{
    class BehZergCommander : BehaviourActionActor//战斗会盖过原本的task，故将两个task合在一起
    {
        public override BehResult execute(Actor pActor)
        {
            new BehFindEnemyAround().execute(pActor);
            return new BehZergCall().execute(pActor);
        }
    }
}
