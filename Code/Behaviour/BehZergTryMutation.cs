using ai.behaviours;
using UnityEngine;
using Zerg.Code.Content;

namespace Zerg.Code.Behaviour
{
    public class BehZergTryMutation : BehaviourActionActor
    {
        public override BehResult execute(Actor pActor)
        {
            pActor.Zerg_tryMutation();
            return BehResult.Continue;
        }
    }
}
