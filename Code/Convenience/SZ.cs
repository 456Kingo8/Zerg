using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Zerg.Code.Convenience
{
    class SZA
    {
        /// <summary>
        /// <para>幼虫</para>
        /// </summary>
        public static string Larva = "Larva";//幼虫

        /// <summary>
        /// <para>工蜂</para>
        /// </summary>
        public static string Drone = "Drone";//工蜂

        /// <summary>
        /// <para>虫后</para>
        /// </summary>
        public static string Queen = "Queen";//虫后

        /// <summary>
        /// <para>王虫</para>
        /// </summary>
        public static string Overlord = "Overlord";//王虫

        /// <summary>
        /// <para>眼虫</para>
        /// </summary>
        public static string Overseer = "Overseer";//眼虫

        /// <summary>
        /// <para>拟态雏虫</para>
        /// </summary>
        public static string Changeling = "Changeling";//拟态雏虫

        /// <summary>
        /// <para>跳虫</para>
        /// </summary>
        public static string Zergling = "Zergling";//跳虫

        /// <summary>
        /// <para>爆虫</para>
        /// </summary>
        public static string Baneling = "Baneling";//爆虫

        /// <summary>
        /// <para>蟑螂</para>
        /// </summary>
        public static string Roach = "Roach";//蟑螂

        /// <summary>
        /// <para>破坏者(火蟑螂)</para>
        /// </summary>
        public static string Ravager = "Ravager";//破坏者(火蟑螂)

        /// <summary>
        /// <para>刺蛇</para>
        /// </summary>
        public static string Hydralisk = "Hydralisk";//刺蛇

        /// <summary>
        /// <para>感染者</para>
        /// </summary>
        public static string Infestor = "Infestor";//感染者

        /// <summary>
        /// <para>异化作战体</para>
        /// </summary>
        public static string Infected_Humans = "Infected_Humans";

        /// <summary>
        /// <para>自爆人</para>
        /// </summary>
        public static string Infested_Terran = "Infested_Terran";

        /// <summary>
        /// <para>被感染的智慧生物</para>
        /// </summary>
        public static string Infested_Unit = "Infested_Unit";

        /// <summary>
        /// <para>被感染的生物</para>
        /// </summary>
        public static string Infested_Animal = "Infested_Animal";

        /// <summary>
        /// <para>雷兽</para>
        /// </summary>
        public static string Ultralisk = "Ultralisk";//雷兽

        /// <summary>
        /// <para>虫群宿主</para>
        /// </summary>
        public static string Swarm_Host = "SwarmHost";//虫群宿主

        /// <summary>
        /// <para>蝗虫</para>
        /// </summary>
        public static string Locust = "Locust";//蝗虫

        /// <summary>
        /// <para>潜伏者(地刺)</para>
        /// </summary>
        public static string Lurker = "Lurker";//潜伏者(地刺)

        /// <summary>
        /// <para>异龙</para>
        /// </summary>
        public static string Mutalisk = "Mutalisk";//异龙

        /// <summary>
        /// <para>异龙</para>
        /// </summary>
        public static string Ex_Mutalisk = "ExMutalisk";//精英异龙

        /// <summary>
        /// <para>腐化者</para>
        /// </summary>
        public static string Corruptor = "Corruptor";//腐化者

        /// <summary>
        /// <para>巢虫领主</para>
        /// </summary>
        public static string Brood_Lord = "BroodLord";//巢虫领主

        /// <summary>
        /// <para>巢虫</para>
        /// </summary>
        public static string Broodling = "Broodling";//巢虫

        /// <summary>
        /// <para>飞蛇</para>
        /// </summary>
        public static string Viper = "Viper";//飞蛇

        /// <summary>
        /// <para>利维坦</para>
        /// </summary>
        public static string Leviathan = "Leviathan";//利维坦

        /// <summary>
        /// <para>凯瑞甘利维坦</para>
        /// </summary>
        public static string SuperLeviathan = "SuperLeviathan";//凯瑞甘利维坦

        /// <summary>
        /// <para>爆炸蚊</para>
        /// </summary>
        public static string Scourge = "Scourge";//爆炸蚊

        /// <summary>
        /// <para>暴虐虫</para>
        /// </summary>
        public static string Bile_Swarm = "BileSwarm";//暴虐虫


        /// <summary>
        /// <para>菌毯肿瘤</para>
        /// </summary>
        public static string Creep = "Creep";//菌毯肿瘤

        /// <summary>
        /// <para>孢子爬虫</para>
        /// </summary>
        public static string Spore_Crawler_Walk = "SporeCrawlerWalk";//孢子爬虫

    //    public static Dictionary<string, string> localized = new Dictionary<string, string>
    //    {
    //        {Larva,"幼虫" },
    //        {Drone,"工蜂" },
    //        {Queen,"虫后" },
    //        {Overlord,"王虫" },
    //        {Overseer,"眼虫" },
    //        {Changeling,"拟态雏虫" },
    //        {Zergling,"跳虫" },
    //        {Baneling,"爆虫" },
    //        {Roach,"蟑螂" },
    //        {Ravager,"破坏者" },
    //        {Hydralisk,"刺蛇" },
    //        {Infestor,"感染者"},
    //        {Ultralisk,"雷兽"},
    //        {Swarm_Host,"虫群宿主"},
    //        {Locust,"蝗虫"},
    //        {Lurker,"潜伏者"},

    //        {Mutalisk,"异龙"},
    //        {Ex_Mutalisk,"精英异龙"},
    //        {Corruptor,"腐化者"},
    //        {Brood_Lord,"巢虫领主"},
    //        {Broodling,"巢虫"},
    //        {Viper,"飞蛇"},
    //        {Leviathan,"利维坦"},

    //        {Creep,"菌毯肿瘤"},
    //        {Spore_Crawler_Walk,"孢子爬虫(行走)"},

    //};
    }
    

    class SZB
    {
        public static string Hatchery = "Hatchery";//孵化场
        public static string Lair = "Lair";//虫穴
        public static string Hive = "Hive";//主巢
        public static string Creep_Tumor = "CreepTumor";//菌毯肿瘤
        public static string Spawning_Pool = "SpawningPool";//分裂池
        public static string Evolution_Chamber = "EvolutionChamber";//进化腔
        public static string Roach_Warren = "RoachWarren";//蟑螂温室
        public static string Baneling_Nest = "BanelingNest";//爆虫巢穴
        public static string Spine_Crawler = "SpineCrawler";//脊针爬虫
        public static string Spore_Crawler = "SporeCrawler";//孢子爬虫
        public static string Hydralisk_Den = "HydraliskDen";//刺蛇巢
        public static string Infestation_Pit = "InfestationPit";//感染深渊
        public static string Spire = "Spire";//尖塔
        public static string Greater_Spire = "GreaterSpire";//巨型尖塔
        public static string Nydus_Worm = "NydusWorm";//坑道虫
        public static string Nydus_Network = "NydusNetwork";//虫道网络
        public static string Ultralisk_Cavern = "UltraliskCavern";//雷兽窟
        public static string Lurker_Den = "LurkerDen";//潜伏者巢穴
        public static string Overmind = "Overmind";//主宰
        public static string Cocoons_land_Actor = "CocoonsLand";//陆地单位虫茧
        public static string Cocoons_fly_Actor = "CocoonsFly";//飞行单位虫茧
        public static string Cocoons_Building_Small = "CocoonsBuildingSmall";//建筑虫茧 小
        public static string Cocoons_Building_Medium = "CocoonsBuildingMedium";//建筑虫茧 中
        public static string Cocoons_Building_Large = "CocoonsBuildingLarge";//建筑虫茧 大
        public static string Cocoons_Hatchery = "CocoonsHatchery";//孵化场升级茧 暂时跟大虫茧一样

        public static List<string> list = new List<string>() { Hatchery, Lair, Hive, Spawning_Pool, Evolution_Chamber, Roach_Warren, Baneling_Nest, Hydralisk_Den, Infestation_Pit, Spire, Greater_Spire, Nydus_Worm, Nydus_Network, Ultralisk_Cavern, Lurker_Den };

        //public static Dictionary<string, string> localized = new Dictionary<string, string>
        //{
        //    {Hatchery,"孵化场"},
        //    {Lair,"虫穴"},
        //    {Hive,"主巢"},
        //    {Creep_Tumor,"菌毯肿瘤"},
        //    {Spawning_Pool,"分裂池"},
        //    {Evolution_Chamber,"进化腔"},
        //    {Roach_Warren,"蟑螂温室"},
        //    {Baneling_Nest,"爆虫巢穴"},
        //    {Spine_Crawler,"脊针爬虫"},
        //    {Spore_Crawler,"孢子爬虫"},
        //    {Hydralisk_Den,"刺蛇巢"},
        //    {Infestation_Pit,"感染深渊"},
        //    {Spire,"尖塔"},
        //    {Greater_Spire,"巨型尖塔"},
        //    {Nydus_Worm,"坑道虫"},
        //    {Nydus_Network,"虫道网络"},
        //    {Ultralisk_Cavern,"雷兽窟"},
        //    {Lurker_Den,"潜伏者巢穴"},
        //    {Overmind,"主宰"},
        //};

    }

    //class SZManager
    //{
    //    public static string findKey(string id)
    //    {
    //        if(SZA.localized.ContainsValue(id))
    //        {
    //            return SZA.localized.FirstOrDefault(x => x.Value == id).Key;
    //        }
    //        else if (SZB.localized.ContainsValue(id))
    //        {
    //            return SZB.localized.FirstOrDefault(x => x.Value == id).Key;
    //        }
    //        return null;
    //    }

    //}

}
