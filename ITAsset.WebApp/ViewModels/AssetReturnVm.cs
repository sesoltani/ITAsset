using ITAsset.Domain.Entities;

namespace ITAsset.WebApp.ViewModels;

public class AssetReturnVm
{
    public int AssignmentId { get; set; }
    public AssetCondition ConditionOnReturn { get; set; }
    public string? Note { get; set; }
}