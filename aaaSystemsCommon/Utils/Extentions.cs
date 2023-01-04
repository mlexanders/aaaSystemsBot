using System.Reflection;

namespace aaaSystemsCommon.Utils
{
    public static class Extentions
    {
        public static string GetRoot(this Type type) => type?
            .GetCustomAttribute<EntityRootAttribute>()?.Root ?? nameof(type);
    }
}
