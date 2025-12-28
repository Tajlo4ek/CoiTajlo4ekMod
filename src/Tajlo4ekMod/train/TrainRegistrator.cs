using Mafi;
using Mafi.Base;
using Mafi.Core.Mods;
using Mafi.Core.Trains;
using Tajlo4ekMod.utils;

namespace Tajlo4ekMod.train
{
    internal class TrainRegistrator : IModData
    {
        private const int newCapacity = 500;

        public void RegisterData(ProtoRegistrator registrator)
        {
            Utils.SwitchValue(registrator, Ids.Trains.WagonT1Unit, typeof(CargoWagonProto), "m_baseCapacity", newCapacity.Quantity());
            Utils.SwitchValue(registrator, Ids.Trains.WagonT1Fluid, typeof(CargoWagonProto), "m_baseCapacity", newCapacity.Quantity());
            Utils.SwitchValue(registrator, Ids.Trains.WagonT1Loose, typeof(CargoWagonProto), "m_baseCapacity", newCapacity.Quantity());
        }
    }
}
