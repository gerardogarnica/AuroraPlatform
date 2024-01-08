using System.Reflection;

namespace Aurora.Framework.Utils
{
    public static class AssemblyUtils
    {
        public static string GetExecutingAssemblyLocation()
        {
            var location = Assembly.GetExecutingAssembly().Location;

            return Path.GetDirectoryName(location);
        }
    }
}