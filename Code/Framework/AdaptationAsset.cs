using System;
using System.Collections.Generic;
using System.Text;

namespace Zerg.Code.Framework
{
    class AdaptationAsset
    {
        public delegate void AdditionAction(Actor actor);

        public string id;
        public bool trait = true;
        public bool cultivate_way = false;
        public string cultivate_id;
        public float priority = 0f;
        public AdditionAction action;
        public AdditionAction action_remove;
    }
}
