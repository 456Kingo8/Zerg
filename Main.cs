using NeoModLoader;
using NeoModLoader.api;
using Zerg.Code.Behaviour;
using Zerg.Code.Content;
using Zerg.Code.Framework;
using Zerg.Code.Patch;
using Zerg.Code.UI;

namespace Zerg
{
    public class ZergMain : BasicMod<ZergMain> ,IReloadable
    {
        protected override void OnModLoad()
        {
            Config.isEditor = true;
            ZergKingdom.init();
            ZergDecision.init();
            ZergSubspeciesTrait.init();
            ZergItem.init();
            ZergProjectile.init();
            ZergTrait.init();
            ZergSpell.init();//spell必须在actor之前
            ZergActor.init();
            ZergBuilding.init();
            ZergBiome.init();
            ZergWorldBehavior.init();
            ZergJob.init();
            ZergStatus.init();
            MutationLibrary.init();
            Patches.init();

            ZergTab.init();//必须在Actor和Builiding之后
        }

        public void Reload()
        {

        }
    }
}
