using NeoModLoader.api.attributes;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using Zerg.Code.Convenience;
using Zerg.Code.Framework;

namespace Zerg.Code.Content
{
    class ZergWorldBehavior
    {
        public static void init()
        {
            WorldBehaviourAsset asset = new WorldBehaviourAsset
            {
                id = "zerg_creep_decay",
                interval = 15f,
                interval_random = 2f,
                action = new WorldBehaviourAction(ZergCreepDecay.checkCreep),
                action_world_clear = new WorldBehaviourAction(ZergCreepDecay.total_clear)
            };
            asset.manager = new WorldBehaviour(asset);
            AssetManager.world_behaviours.add(asset);

            asset = new WorldBehaviourAsset
            {
                id = "zerg_adaptiveEvolution",
                interval = 10f,
                interval_random = 5f,
                action = new WorldBehaviourAction(AdaptiveEvolution.updateEvolve),
                action_world_clear = new WorldBehaviourAction(AdaptiveEvolution.clear)
            };
            asset.manager = new WorldBehaviour(asset);
            AssetManager.world_behaviours.add(asset);

            asset = new WorldBehaviourAsset
            {
                id = "zerg_era",
                interval = 30f,
                interval_random = 0,
                action = new WorldBehaviourAction(ZergEraManager.check),
                action_world_clear = new WorldBehaviourAction(ZergEraManager.clear)
            };
            asset.manager = new WorldBehaviour(asset);
            AssetManager.world_behaviours.add(asset);
        }
    }
}
