using System;
using System.Collections.Generic;
using System.Text;
using Zerg.Code.Convenience;

namespace Zerg.Code.Content
{
    class ZergStatus
    {
        public static void init()
        {
            StatusAsset asset = new StatusAsset();
            asset.id = "Zerg_Mutation";
            asset.duration = 3f;
            asset.action_interval = 0.2f;
            asset.action = Turning;
            asset.action_finish = Turning_end;
            asset.base_stats = new BaseStats();
            asset.base_stats[S.speed] = -2147483648;
            asset.path_icon = "ui/icons/iconZerg";
            asset.locale_description = "Zerg_Mutation_des";
            asset.locale_id = "Zerg_Mutation_id";
            AssetManager.status.add(asset);

            asset = new StatusAsset();
            asset.id = "zerg_creep";
            asset.duration = 15f;
            asset.action_interval = 0.5f;
            asset.action = Healing;
            asset.base_stats = new BaseStats();
            asset.base_stats[S.multiplier_speed] = 0.25f;
            asset.base_stats[S.multiplier_attack_speed] = 0.25f;
            asset.base_stats[S.multiplier_damage] = 0.2f;
            asset.path_icon = "ui/icons/iconZerg";
            asset.locale_description = "zerg_creep_des";
            asset.locale_id = "zerg_creep_id";
            AssetManager.status.add(asset);

            asset = new StatusAsset();
            asset.id = "Microbial_Shroud"; //微生物环绕云
            asset.duration = 16f;
            asset.base_stats = new BaseStats();
            asset.base_stats["armor"] = 50f;
            asset.path_icon = "ui/icons/iconZerg";
            asset.locale_description = "Zerg_Microbial_Shroud_des";
            asset.locale_id = "Zerg_Microbial_Shroud_id";
            AssetManager.status.add(asset);


            asset = new StatusAsset();
            asset.id = "Fungal_Growth";//霉菌滋生
            asset.duration = 3f;
            asset.action_interval = 0.9f;
            asset.action = Fungal_Growth_action;
            asset.base_stats = new BaseStats();
            asset.animated = true;
            asset.texture = "Fungal_Growth";
            asset.allow_timer_reset = true;
            asset.animation_speed = 1f;
            asset.scale = 0.5f;
            asset.base_stats[S.multiplier_speed] = -0.7f;
            asset.path_icon = "ui/icons/iconZerg";
            asset.locale_description = "Zerg_Fungal_Growth_des";
            asset.locale_id = "Zerg_Fungal_Growth_id";
            loadSprite(asset);
            AssetManager.status.add(asset);


        }

        public static bool Turning(BaseSimObject pTarget, WorldTile pTile = null!)
        {
            int i = 10;
            pTarget.a.data.health += i;
            pTarget.a.stats[S.health] += i;
            pTarget.a.spawnParticle(Toolbox.color_heal);
            return true;
        }
        public static bool Turning_end(BaseSimObject pTarget, WorldTile pTile = null!)
        {
            pTarget.a.Zerg_endMutation();
            return true;
        }

        public static bool Healing(BaseSimObject pTarget, WorldTile pTile = null!)
        {
            pTarget.a.restoreHealth(1);
            pTarget.a.restoreStamina(1);
            pTarget.a.spawnParticle(Toolbox.color_heal);
            return true;
        }

        public static bool Fungal_Growth_action(BaseSimObject pTarget, WorldTile pTile = null!)
        {
            pTarget.a.getHit(25,true,AttackType.Other,pSkipIfShake:false,pCheckDamageReduction:false);
            return true;
        }


        private static void loadSprite(StatusAsset asset)
        {
            asset.sprite_list = SpriteTextureLoader.getSpriteList("effects/" + asset.texture, false);
            asset.material = LibraryMaterials.instance.dict[asset.material_id];
            asset.need_visual_render = true;
        }

    }
}
