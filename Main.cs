using NeoModLoader;
using NeoModLoader.api;
using Zerg.Code.Behaviour;
using Zerg.Code.Content;
using Zerg.Code.Patch;
using Zerg.Code.UI;

namespace Zerg
{
    public class Zerg : BasicMod<Zerg>
    {
        protected override void OnModLoad()
        {
            ZergKingdom.init();
            ZergDecision.init();
            ZergActor.init();
            ZergBuilding.init();
            ZergTab.init();//必须在Actor和Builiding之后
            ZergJob.init();
            ZergStatus.init();
            MutationLibrary.init();
            Patches.init();
        }
    }
}
