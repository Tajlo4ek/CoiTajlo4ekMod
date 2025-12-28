using Mafi;
using Mafi.Base;
using Mafi.Core.Mods;
using Mafi.Core.Prototypes;
using Mafi.Core.Trains;
using System;
using System.Reflection;

namespace Tajlo4ekMod.train
{
    internal class TrainRegistrator : IModData
    {
        public void RegisterData(ProtoRegistrator registrator)
        {
            ChangeCapacity(registrator, Ids.Trains.WagonT1Unit, typeof(CargoWagonProto));
            ChangeCapacity(registrator, Ids.Trains.WagonT1Fluid, typeof(CargoWagonProto));
            ChangeCapacity(registrator, Ids.Trains.WagonT1Loose, typeof(CargoWagonProto));
        }

        private void SwitchValue(object obj, Type type, string fieldName, object newValue)
        {
            var field = type.GetField(fieldName, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static);
            if (field != null)
            {
                try
                {
                    field.SetValue(obj, newValue);
                }
                catch (Exception ex)
                {
                    Logger.Log(ex.ToString());
                    Logger.Log("error set " + fieldName);
                }
            }
            else
            {
                Logger.Log("error get field " + fieldName);
            }
        }

        private void ChangeCapacity(ProtoRegistrator registrator, Proto.ID id, Type type)
        {
            if (registrator.PrototypesDb.TryGetProto(id, out Proto obj))
            {
                SwitchValue(obj, type, "m_baseCapacity", 500.Quantity());
            }
            else
            {
                Logger.Log(id + " not found");
            }
        }
    }
}
