using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;
using UnityEngine.Assertions;
using Zerg.Code.Convenience;

namespace Zerg.Code.Content
{
    internal class ZergActor
    {
        public static List<string> list = new();
        public static void init()
        {
            ActorAsset actor = AssetManager.actor_library.clone("$zerg_actor$", "$mob$");
            actor.can_have_subspecies = true;
            actor.default_subspecies_traits = new List<string> { "NoSleep" };
            actor.disable_jump_animation = true;
            actor.can_turn_into_mush = false;
            actor.can_turn_into_ice_one = false;
            actor.can_turn_into_tumor = false;
            actor.can_turn_into_zombie = false;
            actor.can_turn_into_demon_in_age_of_chaos = false;
            actor.immune_to_tumor = true;
            actor.unit_other = true;
            actor.inspect_children = false;
            actor.inspect_sex = false;
            actor.inspect_generation = false;
            actor.civ = false;
            actor.trait_group_filter_subspecies = AssetLibrary<ActorAsset>.l<string>(new[] 
            {
                "advanced_brain",
                "phenotypes"
            });
            actor.name_template_sets = AssetLibrary<ActorAsset>.a<string>(new[]
            {
                "default_set"
            });
            actor.chromosomes_first = AssetLibrary<ActorAsset>.l<string>(new string[]
            {
                "chromosome_big",
                "chromosome_medium"
            });
            actor.use_phenotypes = false;
            actor.has_advanced_textures = false;
            actor.has_baby_form = false;
            actor.render_heads_for_babies = false;
            actor.kingdom_id_civilization = string.Empty;
            actor.build_order_template_id = string.Empty;
            actor.kingdom_id_wild = "Zerg";
            actor.color = Toolbox.makeColor("#AD00B9");
            actor.color_hex = "#AD00B9";
            actor.traits = new List<string> { "异虫", "poison_immune", "immune" };
            actor.name_taxonomic_kingdom = "animalia";
            actor.name_taxonomic_phylum = "neoplasia";
            actor.name_taxonomic_class = "malignomorpha";
            actor.name_taxonomic_order = "oncovorales";
            actor.name_taxonomic_family = "tumoridae";
            actor.name_taxonomic_genus = "neoplasmus";
            actor.name_taxonomic_species = "carcinomus";
            actor.collective_term = "group_cancer";
            actor.can_edit_equipment = false;
            actor.take_items = false;
            actor.use_items = false;
            actor.icon = "iconZerg";
            actor.base_stats = new();
            actor.base_stats["size"] = 0.5f;
            actor.base_stats["scale"] = 0.1f;
            actor.base_stats["mass"] = 1f;
            actor.base_stats["mass_2"] = 50f;
            actor.flying = false;
            actor.very_high_flyer = false;
            actor.music_theme = "Buildings_Tumor";
            actor.sound_hit = "event:/SFX/HIT/HitFlesh";
            actor.body_separate_part_hands = false;
            actor.shadow = false;
            actor.texture_id = "t_tumor_monster_unit";
            actor.check_flip = AssetManager.actor_library.get("tumor_monster_unit").check_flip;
            actor.animation_walk = ActorAnimationSequences.walk_0_1;
            actor.animation_idle = ActorAnimationSequences.walk_0_1;
            actor.animation_swim = ActorAnimationSequences.walk_0_1;
            actor.base_stats.addTag("Zerg");

            actor = AssetManager.actor_library.clone("$zerg_actor_building$", "$zerg_actor$");
            actor.can_level_up = false;
            actor.can_be_moved_by_powers = false;
            actor.allow_possession = false;
            actor.allow_strange_urge_movement = false;
            actor.damaged_by_ocean = true;
            actor.skip_fight_logic = true;
            actor.base_stats.addTag("immovable");
            actor.can_flip = false;
            //actor.default_attack = "Unable_to_attack";
            actor.job = new string[] { "building" };
            actor.base_stats[S.scale] = 0.2f;
            actor.traits = new List<string> { "异虫建筑", "fire_proof", "freeze_proof", "poison_immune", "immune" };
            actor.animation_walk = new string[] { "walk_0" };
            actor.animation_idle = new string[] { "walk_0" };
            actor.animation_swim = new string[] { "walk_0" };


            actor = AssetManager.actor_library.clone(SZA.Larva, "$zerg_actor$");
            actor.base_stats["size"] = 0.2f;
            actor.addGenome(new[]
            {
                ("health", 50f),
                ("stamina", 10f),
                ("lifespan", 10f),
                ("damage", 0f),
                ("speed", 2f),
                ("armor", 80f),
            });
            actor.skip_fight_logic = true;
            actor.allow_strange_urge_movement = false;
            list.Add(actor.id);
            setaAnimation(actor, 2, 3, 3, 1f, 1f, 1f);
            actor.addDecision("zerg_try_mutation");

            actor = AssetManager.actor_library.clone(SZA.Drone, "$zerg_actor$");
            actor.base_stats["size"] = 0.4f;
            actor.addGenome(new[]
            {
                ("health", 100f),
                ("stamina", 20f),
                ("lifespan", 30f),
                ("damage", 5f),
                ("attack_speed",2f),
                ("speed", 20f),
                ("armor", 20f),
            });
            actor.addDecision("zerg_try_mutation");
            list.Add(actor.id);
            setaAnimation(actor, 2, 3, 3, 1f, 0.8f, 0.8f);

            actor = AssetManager.actor_library.clone(SZA.Zergling, "$zerg_actor$");
            actor.base_stats["size"] = 0.5f;
            actor.addGenome(new[]
            {
                ("health", 105f),
                ("stamina", 50f),
                ("lifespan", 20f),
                ("damage", 15f),
                ("attack_speed",3f),
                ("speed", 45f),
                ("armor", 10f),
            });
            actor.addDecision("zerg_try_mutation");
            list.Add(actor.id);
            setaAnimation(actor, 1, 2, 4, 1f, 2f, 2f);

            //虫后
            actor = AssetManager.actor_library.clone(SZA.Queen, "$zerg_actor$");
            actor.addGenome(new[]
{
                ("health", 450f),
                ("stamina", 100f),
                ("lifespan", 1000f),
                ("damage", 20f),
                ("attack_speed",2f),
                ("speed", 15f),
                ("armor", 20f),
            });
            list.Add(actor.id);
            actor.base_stats["size"] = 1f;
            actor.base_stats["mana"] = 100f;
            actor.base_stats["accuracy"] = 100f;
            actor.traits = new List<string> { "eagle_eyed", "poison_immune", "immune" };
            actor.default_attack = "zerg_spine";
            actor.addDecision("create_creep_tumor");
            //actor.spells = new();
            //actor.spells.addSpell(ZergSpell.create_creep_tumor);
            setaAnimation(actor, 2, 2, 2, 1f, 0.8f, 0.8f);

            actor = AssetManager.actor_library.clone(SZA.Baneling, "$zerg_actor$");
            actor.addGenome(new[]
{
                ("health", 60f),
                ("stamina", 100f),
                ("lifespan", 40f),
                ("damage", 1f),
                ("attack_speed",5f),
                ("speed", 37f),
                ("armor", 25f)
            });
            list.Add(actor.id);
            actor.base_stats["size"] = 0.3f;
            actor.traits = new List<string> { "zerg_attack_explode", "poison_immune", "immune" };
            setaAnimation(actor, 1, 2, 5);

            actor = AssetManager.actor_library.clone(SZA.Mutalisk, "$zerg_actor$");
            actor.addGenome(new[]
{
                ("health", 240f),
                ("stamina", 180f),
                ("lifespan", 60f),
                ("damage", 25f),
                ("attack_speed",1f),
                ("speed", 55f),
                ("armor", 20f),

            });
            list.Add(actor.id);
            actor.base_stats["size"] = 1.2f;
            actor.base_stats["range"] = 2f;
            actor.base_stats["accuracy"] = 100f;
            actor.flying = true;
            actor.very_high_flyer = true;
            actor.default_attack = "$bow";
            actor.default_height = 2;
            actor.animation_speed_based_on_walk_speed = false;
            actor.addDecision("zerg_try_mutation");
            setaAnimation(actor, 5, 0, 5, 5, 1, 5);

            //以下为技术性实体，无对应按钮，需手动loadTexture(actor);
            //异虫 虫茧 建筑
            actor = AssetManager.actor_library.clone(SZB.Cocoons_Building, "$zerg_actor_building$");
            actor.addGenome(new[]
{
                ("health", 100f),
                ("stamina", 0f),
                ("lifespan", 1000f),
                ("damage", 0f),
                ("attack_speed",0f),
                ("speed", 0f),
                ("armor", 20f),
            });
            actor.can_have_subspecies = false;
            actor.show_in_taxonomy_tooltip = false;
            actor.show_in_knowledge_window = false;
            actor.traits = new List<string> { "虫茧", "异虫建筑", "诅咒免疫", "fire_proof", "freeze_proof", "poison_immune", "immune" };
            actor.animation_walk = new string[] { "walk_0", "walk_1", "walk_2", "walk_3" };
            actor.animation_idle = new string[] { "walk_0", "walk_1", "walk_2", "walk_3" };
            loadTexture(actor);

            //异虫 虫茧 陆地生物
            actor = AssetManager.actor_library.clone(SZB.Cocoons_land_Actor, "$zerg_actor_building$");
            actor.base_stats[S.lifespan] = 100000;
            actor.base_stats[S.scale] = 0.1f;
            actor.addGenome(new[]
{
                ("health", 100f),
                ("stamina", 0f),
                ("lifespan", 1000f),
                ("damage", 0f),
                ("attack_speed",0f),
                ("speed", 0f),
                ("armor", 20f),
            });
            actor.can_have_subspecies = false;
            actor.show_in_taxonomy_tooltip = false;
            actor.show_in_knowledge_window = false;
            actor.traits = new List<string> { "虫茧", "异虫建筑", "诅咒免疫", "fire_proof", "freeze_proof", "poison_immune", "immune" };
            actor.animation_idle = new string[] { "idle_0", "idle_1" };
            loadTexture(actor);
            //异虫 虫茧 飞行生物
            actor = AssetManager.actor_library.clone(SZB.Cocoons_fly_Actor, SZB.Cocoons_land_Actor);
            actor.addGenome(new[]
{
                ("health", 100f),
                ("stamina", 0f),
                ("lifespan", 1000f),
                ("damage", 0f),
                ("attack_speed",0f),
                ("speed", 0f),
                ("armor", 20f),
            });
            actor.flying = true;
            actor.very_high_flyer = true;
            actor.die_on_blocks = false;
            actor.ignore_blocks = true;
            actor.show_in_taxonomy_tooltip = false;
            actor.show_in_knowledge_window = false;
            actor.animation_idle = new string[] { "idle_0", "idle_1" };
            loadTexture(actor);

            //异虫 虫茧 孵化场升级
            actor = AssetManager.actor_library.clone(SZB.Cocoons_Hatchery, SZB.Cocoons_Building);
            actor.show_in_taxonomy_tooltip = false;
            actor.show_in_knowledge_window = false;
            actor.addGenome(new[]
{
                ("health", 200f),
                ("stamina", 0f),
                ("lifespan", 1000f),
                ("damage", 0f),
                ("attack_speed",0f),
                ("speed", 0f),
                ("armor", 40f),
            });
            actor.traits = new List<string> { "虫茧", "异虫建筑", "诅咒免疫", "fire_proof", "freeze_proof", "poison_immune", "immune" };
            actor.animation_idle = new string[] { "walk_0", "walk_1" };
            actor.animation_walk = new string[] { "walk_0", "walk_1" };
            loadTexture(actor);









            foreach (string id in list)
            {
                var asset = AssetManager.actor_library.get(id);
                asset.name_locale = id;
                loadTexture(asset);
            }






        }
        private static void loadTexture(ActorAsset asset)
        {
            string path = "units/t_" + asset.id;
            var texture_asset = new ActorTextureSubAsset(path, false);
            //texture_asset.texture_path_base = path;
            texture_asset.texture_path_main = path;
            //texture_asset.texture_path_base_female = path;
            //texture_asset.texture_path_base_male = path;
            //texture_asset.texture_heads = string.Empty;
            //texture_asset.texture_heads_male = string.Empty;
            //texture_asset.texture_heads_female = string.Empty;
            asset.texture_asset = texture_asset;
        }

        private static void setaAnimation(ActorAsset pAsset, int idle, int swim, int walk, float idle_speed = 1f, float swim_speed = 1f, float walk_speed = 1f)
        {
            int i;
            string[] idle_list = new string[idle];
            string[] walk_list = new string[walk];
            string[] swim_list = new string[swim];
            for (i = 0; i < idle; i++)
            {
                idle_list[i] = $"idle_{i}";
            }
            for (i = 0; i < walk; i++)
            {
                walk_list[i] = $"walk_{i}";
            }
            for (i = 0; i < swim; i++)
            {
                swim_list[i] = $"swim_{i}";
            }
            pAsset.animation_idle = idle_list;
            pAsset.animation_walk = walk_list;
            pAsset.animation_swim = swim_list;
            pAsset.animation_idle_speed = idle_speed;
            pAsset.animation_swim_speed = swim_speed;
            pAsset.animation_walk_speed = walk_speed;
        }
    }
}
