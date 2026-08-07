using ai.behaviours;
using System;
using System.Collections.Generic;
using System.Text;
using Zerg.Code.Content;

namespace Zerg.Code.Behaviour
{
    class BehCreateCreepTumor : BehaviourActionActor
    {
        public override BehResult execute(Actor pActor)
        {
            SpellAsset tSpellAsset = AssetManager.spells.get("create_creep_tumor");
            if (tSpellAsset.action != null && pActor.hasEnoughMana(tSpellAsset.cost_mana))
            {

                if (tSpellAsset.action.RunAnyTrue(pActor, pActor, pActor.current_tile))
                {
                    pActor.restoreMana(-tSpellAsset.cost_mana);
                }
            }
            return BehResult.Continue;

        }

    }
}
