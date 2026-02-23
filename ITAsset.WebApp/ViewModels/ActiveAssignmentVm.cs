using ITAsset.Domain.Entities;

namespace ITAsset.WebApp.ViewModels;

public class ActiveAssignmentVm
{
    public int AssignmentId { get; set; }
    public string EmployeeName { get; set; } = "";
    public DateTime AssignedDate { get; set; }
    public AssetCondition ConditionOnAssign { get; set; }
}