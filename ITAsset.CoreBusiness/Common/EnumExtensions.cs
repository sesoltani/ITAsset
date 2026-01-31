using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace ITAsset.Domain.Common;

public static class EnumExtensions
{
    public static string GetDisplayName(this Enum enumValue)
    {
        var member = enumValue.GetType()
            .GetMember(enumValue.ToString())
            .FirstOrDefault();

        var displayAttr = member?
            .GetCustomAttribute<DisplayAttribute>();

        return displayAttr?.Name ?? enumValue.ToString();
    }
}