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
        }
    }
}
