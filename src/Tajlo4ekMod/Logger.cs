using Mafi;

namespace Tajlo4ekMod
{
    [GlobalDependency(RegistrationMode.AsSelf, false)]
    public static class Logger
    {
        public static void Log(string text)
        {
            Mafi.Log.Info("[TAJLO4EK] " + text);
        }

        public static void Log(object obj)
        {
            Log(obj.ToString());
        }
    }
}
