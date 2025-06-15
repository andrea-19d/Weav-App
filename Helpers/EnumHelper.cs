using System.Runtime.Serialization;

namespace Weav_App.Helpers;

public static class EnumHelper
{
    public static List<string> GetEnumMemberValues<T>() where T : Enum
    {
        return typeof(T)
            .GetFields()
            .Where(f => f.IsStatic)
            .Select(f =>
            {
                var attr = f.GetCustomAttributes(typeof(EnumMemberAttribute), false)
                    .FirstOrDefault() as EnumMemberAttribute;
                return attr?.Value ?? f.Name;
            })
            .ToList();
    }
}
