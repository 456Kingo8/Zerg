using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace Zerg.Code.Extend
{
    public static class ActorExtensions
    {
        private const string Mutation_key = "Zerg.Mutation";
        private const string Mutation_num_key = "Zerg.MutationNumber";
        private const string Mutation_building_key = "Zerg.MutationBuilding";
        private const string Original_kingdom_key = "Zerg.OriginalKingdom";
        public static string GetMutation_id(this Actor actor)
        {
            actor.data.get(Mutation_key, out string val, null);
            return val;
        }
        public static void SetMutation_id(this Actor actor, string val)
        {
            actor.data.set(Mutation_key, val);
        }

        public static int GetMutation_num(this Actor actor)
        {
            actor.data.get(Mutation_num_key, out int val, 0);
            return val;
        }

        public static void SetMutation_num(this Actor actor, int val)
        {
            actor.data.set(Mutation_num_key, val);
        }

        public static bool GetMutation_building(this Actor actor)
        {
            actor.data.get(Mutation_building_key, out bool val, false);
            return val;
        }

        public static void SetMutation_building(this Actor actor, bool val)
        {
            actor.data.set(Mutation_building_key, val);
        }

        public static Kingdom GetOriginal_kingdom(this Actor actor)
        {
            actor.data.get(Original_kingdom_key, out long val, 0);
            return World.world.kingdoms.get(val);
        }

        public static void SetOriginal_kingdom(this Actor actor, long val)
        {
            actor.data.set(Original_kingdom_key, val);
        }

    }
}
