using ITAsset.Domain.Entities;

namespace ITAsset.WebApp.ViewModels;

public class AssetAssignVm
{
    public int AssetId { get; set; }
    public int EmployeeId { get; set; }
    public DateTime? ExpectedReturnDate { get; set; }
    public AssetCondition ConditionOnAssign { get; set; } = AssetCondition.Excellent;
    public string? Note { get; set; }
}