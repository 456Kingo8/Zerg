using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace Zerg.Code.Extend
{
    public static class UnitSpawnerExtend
    {
        private static ConditionalWeakTable<UnitSpawner, List<string>> unit_spawner_extends = new();

        public static List<string> GetExtend(this UnitSpawner unit)
        {
            if (!unit_spawner_extends.TryGetValue(unit, out List<string>? extend) || extend == null)
            {
                extend = new List<string>();
                unit_spawner_extends.Add(unit, extend);
            }
            return extend;
        }
    }
}