using ai.behaviours;
using System;
using System.Collections.Generic;
using System.Text;
using Zerg.Code.Behaviour;
using Zerg.Code.UI;

namespace Zerg.Code.Content
{
    class ZergDecision
    {
        public static void init()
        {
            DecisionAsset asset = new DecisionAsset();
            asset.id = "zerg_try_mutation";
            asset.priority = NeuroLayer.Layer_4_Critical;
            asset.path_icon = "ui/Icons/iconZerg";
            asset.cooldown = 5;
            asset.unique = true;
            asset.only_safe = true;
            asset.weight = 2f;
            asset.decision_index = AssetManager.decisions_library.list.Count;
            asset.task_id = add_Task(asset.id, asset.path_icon, new BehZergTryMutation()).id;
            AssetManager.decisions_library.add(asset);

        }

        private static BehaviourTaskActor add_Task(string id, string icon, BehaviourActionActor pBeh)
        {
            BehaviourTaskActor task = new BehaviourTaskActor();
            task.id = id;
            task.addBeh(pBeh);
            task.setIcon(icon);
            task.locale_key = "task_" + id;
            AssetManager.tasks_actor.add(task);
            return task;
        }
    }
}
