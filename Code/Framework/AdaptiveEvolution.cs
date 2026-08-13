using NeoModLoader.api.attributes;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using Zerg.Code.Convenience;

namespace Zerg.Code.Framework
{
    public static class AdaptiveEvolution
    {
        private static List<string> _adaptive_asset_total = new();//用于统计可掠夺的特质
        private static List<string> _adaptive_asset_current = new();//用于统计当前世界掠夺到的特质
        private static Dictionary<string,string> _adaptive_asset_cultivate = new();//用于统计当前世界掠夺到的修炼特质，仅存储最高等级
        private static List<string> _adaptive_asset_update_add = new();//用于获取的特质
        private static List<string> _adaptive_asset_update_remove = new();//用于删除的特质
        private static bool _update = false;

        public static void init()
        {
            foreach(AdaptationAsset asset in AdaptationLibrary.list )
            {
                if(asset.trait)
                {
                    _adaptive_asset_total.Add(asset.id);
                }
            }
        }

        public static void clear()
        {
            _adaptive_asset_current.Clear();
            _adaptive_asset_update_add.Clear();
            _adaptive_asset_update_remove.Clear();
            _adaptive_asset_cultivate.Clear();
        }

        [Hotfixable]
        public static void addNewTrait(string id)
        {

            if (_adaptive_asset_total.Contains(id))
            {
                var asset = AdaptationLibrary.get(id);
                if(asset.cultivate_way)
                {
                    if (_adaptive_asset_cultivate.ContainsKey(asset.cultivate_id))
                    {
                        var cur = AdaptationLibrary.get(_adaptive_asset_cultivate[asset.cultivate_id]);
                        if(cur.priority < asset.priority)
                        {
                            _adaptive_asset_cultivate[asset.cultivate_id] = id;
                            _adaptive_asset_current.Add(id);
                            _adaptive_asset_update_add.Add(id);
                            _adaptive_asset_current.Remove(cur.id);
                            _adaptive_asset_update_remove.Add(cur.id);
                            _update = true;
                        }
                    }
                    else
                    {
                        _adaptive_asset_cultivate.Add(asset.cultivate_id, id);
                        _adaptive_asset_current.Add(id);
                        _adaptive_asset_update_add.Add(id);
                        _update = true;
                    }
                }
                else
                {
                    _adaptive_asset_current.Add(id);
                    _adaptive_asset_update_add.Add(id);
                    _update = true;
                }
            }
        }

        [Hotfixable]
        public static void updateEvolve()
        {
            if (!_update) return;
            //if 模组配置按钮的return
            _update = false;
            foreach (Actor actor in World.world.units)
            {
                if(actor.hasTag("Zerg"))
                {
                    foreach(string id in _adaptive_asset_update_add)
                    {
                        var asset = AdaptationLibrary.get(id);
                        if (asset.trait) actor.addTrait(id, true);
                        if (asset.action != null) asset.action(actor);
                    }
                    foreach (string id in _adaptive_asset_update_remove)
                    {
                        var asset = AdaptationLibrary.get(id);
                        if (asset.trait) actor.removeTrait(id);
                        //if (asset.action != null) asset.action.Invoke(actor);
                    }
                }
            }
            _adaptive_asset_update_add.Clear();
            _adaptive_asset_update_remove.Clear();

            foreach (Building building in World.world.buildings)
            {
                if(building.isUsable() && (building.asset.id == SZB.Hatchery || building.asset.id == SZB.Lair || building.asset.id == SZB.Hive))
                {
                    EffectsLibrary.spawnAt("fx_monolith_launch", building.current_tile.posV3, 0.6f);
                }
            }
        }

        public static void Zerg_addAllAdaption(this Actor actor)
        {
            foreach (string id in _adaptive_asset_current)
            {
                var asset = AdaptationLibrary.get(id);
                if (asset.trait) actor.addTrait(id, true);
                if (asset.action != null) asset.action(actor);
            }
        }
    }
}
