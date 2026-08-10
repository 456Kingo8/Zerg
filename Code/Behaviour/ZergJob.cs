using ai.behaviours;
using System;
using System.Collections.Generic;
using System.Text;

namespace Zerg.Code.Behaviour
{
    class ZergJob
    {
        public static void init()
        {
            AssetManager.job_actor.add(new ActorJob
            { id = "building" });
            AssetManager.job_actor.t.addTask("wait");

            AssetManager.job_actor.add(new ActorJob
            { id = "zerg_commander" });
            AssetManager.job_actor.t.addTask("random_move");
            AssetManager.job_actor.t.addTask("zerg_commander");
        }
    }
}
