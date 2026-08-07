using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using UnityEngine;

namespace Zerg.Code.Extend
{
    public static class UnitSpawnerExtend
    {
        private static ConditionalWeakTable<UnitSpawner, List<string>> unit_spawner_extends_building = new();//用于存储实际拥有的建筑
        private static ConditionalWeakTable<UnitSpawner, List<string>> unit_spawner_extends_all = new();//用于存储所有建筑，包括建筑中

        public static List<string> GetExtend(this UnitSpawner unit)
        {
            if (!unit_spawner_extends_building.TryGetValue(unit, out List<string>? extend) || extend == null)
            {
                extend = new List<string>();
                unit_spawner_extends_building.Add(unit, extend);
            }
            return extend;
        }

        public static void SetExtend(this UnitSpawner unit,List<string> str)
        {
            unit_spawner_extends_building.AddOrUpdate(unit, str);
        }



        public static List<string> GetExtend_All(this UnitSpawner unit)
        {
            if (!unit_spawner_extends_all.TryGetValue(unit, out List<string>? extend) || extend == null)
            {
                extend = new List<string>();
                unit_spawner_extends_all.Add(unit, extend);
            }
            return extend;
        }

        public static void SetExtend_All(this UnitSpawner unit, List<string> str)
        {
            unit_spawner_extends_all.AddOrUpdate(unit, str);
        }
    }
}