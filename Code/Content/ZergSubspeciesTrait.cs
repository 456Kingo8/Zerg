using System;
using System.Collections.Generic;
using System.Text;

namespace Zerg.Code.Content
{
    class ZergSubspeciesTrait
    {
        public static void init()
        {
            SubspeciesTrait NoSleep = new SubspeciesTrait();
            NoSleep.id = "NoSleep";
            NoSleep.group_id = "sleep_cycles";
            NoSleep.rarity = Rarity.R1_Rare;
            NoSleep.priority = 105;
            NoSleep.path_icon = "ui/Icons/iconZerg";
            AssetManager.subspecies_traits.add(NoSleep);
        }
    }
}
