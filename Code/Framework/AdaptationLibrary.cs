using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace Zerg.Code.Framework
{
    class AdaptationLibrary
    {
        public static List<AdaptationAsset> list = new List<AdaptationAsset>();

        public static Dictionary<string, AdaptationAsset> id_dict = new Dictionary<string, AdaptationAsset>();//id到asset的对应关系

        public static Dictionary<string,List< AdaptationAsset>> cultivate_dict = new Dictionary<string, List<AdaptationAsset>>();//体系到list的对应关系

        public static void init()
        {
            AdaptationAsset asset = new AdaptationAsset();
            asset.id = "fire_proof";
            list.Add(asset);
            asset = new AdaptationAsset();
            asset.id = "freeze_proof";
            list.Add(asset);

#if WARRIOR
            var warrior_list = new float[] { 5, 10, 20, 40, 80, 160, 300, 500, 800, 1200, 3600, 12000 };
            string warrior_key = "wushu.warriorNum";
            for(int i = 1; i <= 9;i++)
            {
                int index = i;
                float num = warrior_list[i-1]; 
                asset = new AdaptationAsset();
                asset.id = $"Warrior{i}";
                asset.cultivate_way = true;
                asset.cultivate_id = "Warrior";
                asset.priority = index;
                asset.action = (Actor actor) => {actor.data.set(warrior_key, num); };
                list.Add(asset);
            }
            for (int i = 1; i <= 3; i++)
            {
                int index = i + 9;
                float num = warrior_list[i+8]; 
                asset = new AdaptationAsset();
                asset.id = $"Warrior9{i}";
                asset.cultivate_way = true;
                asset.cultivate_id = "Warrior";
                asset.priority = index;
                asset.action = (Actor actor) => {actor.data.set(warrior_key, num); };
                list.Add(asset);
            }
#endif

#if THEFANTASYWORLD

            var the_fantasy_world = new List<string>() { "enchanter", "pastor", "Paladin", "valiantgeneral", "Ranger", "Assassin", "Summoner", "minstrel", "warlock", "alchemist", "barbarian"};
            foreach(string id in the_fantasy_world)
            {
                for (int i = 1; i <= 7; i++)
                {
                    int index = i;
                    asset = new AdaptationAsset();
                    asset.id = $"{id}{i}";
                    asset.cultivate_way = true;
                    asset.cultivate_id = "THEFANTASYWORLD." + id;
                    asset.priority = index;
                    list.Add(asset);
                }
            }

#endif

#if XUANJIAN
            for (int i = 1; i <= 4; i++)
            {
                int index = i;
                asset = new AdaptationAsset();
                asset.id = $"XjRealm{i}";
                asset.trait = false;
                asset.cultivate_way = true;
                asset.cultivate_id = "XUANJIAN";
                asset.action = (Actor actor) => { actor.traits.Add(AssetManager.traits.get($"XjRealm{index}")); };
                asset.action_remove = (Actor actor) => { actor.traits.Remove(AssetManager.traits.get($"XjRealm{index}")); };
                asset.priority = AssetManager.traits.get($"XjRealm{index}").base_stats["health"]/100;
                list.Add(asset);
            }
            for (int i = 1; i <= 2; i++)
            {
                int index = i;
                asset = new AdaptationAsset();
                asset.id = $"XjRealm1{i}";
                asset.trait = false;
                asset.cultivate_way = true;
                asset.cultivate_id = "XUANJIAN";
                asset.priority = AssetManager.traits.get($"XjRealm1{index}").base_stats["health"] / 100;
                asset.action = (Actor actor) => { actor.traits.Add(AssetManager.traits.get($"XjRealm1{index}")); };
                asset.action_remove = (Actor actor) => { actor.traits.Remove(AssetManager.traits.get($"XjRealm1{index}")); };
                list.Add(asset);
            }
            for (int i = 1; i <= 4; i++)
            {
                int index = i;
                asset = new AdaptationAsset();
                asset.id = $"XjRealm2{i}";
                asset.cultivate_way = true;
                asset.cultivate_id = "XUANJIAN";
                asset.priority = AssetManager.traits.get($"XjRealm2{index}").base_stats["health"] / 100;
                asset.action = (Actor actor) => { actor.traits.Add(AssetManager.traits.get($"XjRealm2{index}")); };
                asset.action_remove = (Actor actor) => { actor.traits.Remove(AssetManager.traits.get($"XjRealm2{index}")); };
                list.Add(asset);
            }

#endif
            post_init();
        }

        private static void post_init()
        {
            foreach (AdaptationAsset asset in list)
            {
                 id_dict.Add(asset.id, asset);
                if(asset.cultivate_way)
                {
                    if(cultivate_dict.ContainsKey(asset.cultivate_id))
                    {
                        cultivate_dict[asset.cultivate_id].Add(asset);
                    }
                    else
                    {
                        cultivate_dict[asset.cultivate_id] = new List<AdaptationAsset>() { asset };
                    }
                }
            }
        }

        public static AdaptationAsset get(string id)
        {
            id_dict.TryGetValue(id, out AdaptationAsset value);
            if(value == null) return new AdaptationAsset();
            return value;
        }
    }
}
