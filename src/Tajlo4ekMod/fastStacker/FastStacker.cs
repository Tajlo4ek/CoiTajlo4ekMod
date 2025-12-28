using Mafi;
using Mafi.Base;
using Mafi.Core.Factory.Transports;
using Mafi.Core.Mods;
using Tajlo4ekMod.utils;

namespace Tajlo4ekMod.fastStacker
{
    internal class FastStacker : IModData
    {
        public void RegisterData(ProtoRegistrator registrator)
        {
            Utils.SwitchValue(registrator, Ids.Transports.Stacker, typeof(StackerProto), "DumpPeriod", new Duration(1));
        }
    }
}
