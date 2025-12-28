using Mafi.Core.Mods;
using System;
using System.Reflection;
using Mafi.Core.Prototypes;

namespace Tajlo4ekMod.utils
{
    internal class Utils
    {
        public static void LogAllField(ProtoRegistrator registrator, Proto.ID id, Type type)
        {

            if (registrator.PrototypesDb.TryGetProto(id, out Proto obj) == false)
            {
                Logger.Log(id + " not found");
                return;
            }

            var fields = type.GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static);

            foreach (var field in fields)
            {
                Logger.Log(field.Name + " " + field.GetValue(obj));
            }
        }

        public static void SwitchValue(ProtoRegistrator registrator, Proto.ID id, Type type, string fieldName, object newValue)
        {
            if (registrator.PrototypesDb.TryGetProto(id, out Proto obj) == false)
            {
                Logger.Log(id + " not found");
                return;
            }

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
                    return;
                }
            }
            else
            {
                Logger.Log("error get field " + fieldName);
                return;
            }
        }
    }
}
