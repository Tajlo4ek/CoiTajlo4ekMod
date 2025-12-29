using Mafi.Core.Mods;
using Mafi.Core.Prototypes;
using System;
using System.Reflection;

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
            var prop = type.GetProperty(fieldName, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static);


            try
            {
                if (field != null)
                {
                    field.SetValue(obj, newValue);
                }
                else if (prop != null)
                {
                    prop.SetValue(obj, newValue);
                }
                else
                {
                    Logger.Log("error get field " + fieldName);
                    return;
                }
            }
            catch (Exception ex)
            {
                Logger.Log(ex.ToString());
                Logger.Log("error set " + fieldName);
                return;
            }



        }
    }
}
