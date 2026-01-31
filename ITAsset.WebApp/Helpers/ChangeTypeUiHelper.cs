using ITAsset.Domain.Entities;

namespace ITAsset.WebApp.Helpers;

public class ChangeTypeUiHelper
{
    public static string GetBadgeClass(ChangeType type)
    {
        return type switch
        {
            ChangeType.Install => "bg-success",
            ChangeType.Replace => "bg-warning text-dark",
            ChangeType.Remove => "bg-danger",
            _ => "bg-secondary"
        };
    }
}