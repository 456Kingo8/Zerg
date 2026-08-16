using System;
using System.Collections.Generic;
using System.Text;
using Zerg.Code.Content;
using Zerg.Code.Behaviour;
#if CULTIWAY
using Cultiway.Utils.Extension;
using Cultiway.Core;
using Cultiway.Content.Extensions;
using Cultiway.Content.Behaviours;
using Cultiway.Content;
using Cultiway;
#endif

namespace Zerg.Code.LinkedMods
{
    class LinkCultiway
    {
        public static void init()
        {

#if CULTIWAY
            DecisionAsset decision =  new DecisionAsset();
            decision.id = "zerg_try_cultivate";
            decision.priority = NeuroLayer.Layer_4_Critical;
            decision.path_icon = "ui/Icons/iconZerg";
            decision.cooldown = 8;
            decision.unique = true;
            decision.only_safe = false;
            decision.weight = 4f;
            decision.decision_index = AssetManager.decisions_library.list.Count;
            decision.task_id = ZergDecision.add_Task(decision.id, decision.path_icon,new BehPlantXianCultivate()).id;
            AssetManager.decisions_library.add(decision);

            foreach (var id in ZergActor.list)
            {
                var asset = AssetManager.actor_library.get(id);
                var extend = asset.GetExtend<ActorAssetExtend>();
                extend.must_have_element_root = true;
                extend.available_cultisys_ids = new HashSet<string> {"Xian"};
                asset.addDecision("zerg_try_cultivate");

            }
#endif
        }
    }
}
