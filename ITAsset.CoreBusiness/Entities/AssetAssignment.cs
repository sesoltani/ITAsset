
using System.ComponentModel.DataAnnotations;

namespace ITAsset.Domain.Entities;

public class AssetAssignment
{
    public int Id { get; set; }

    public int AssetId { get; set; }
    public int EmployeeId { get; set; }

    public int AssignedByUserId { get; set; }

    public DateTime AssignedDate { get; set; } = DateTime.UtcNow;
    public DateTime? ExpectedReturnDate { get; set; }
    public DateTime? ActualReturnDate { get; set; }

    public AssetCondition ConditionOnAssign { get; set; }
    public AssetCondition? ConditionOnReturn { get; set; }

    public bool IsReturned => ActualReturnDate != null;

    #region Navigation
    public Asset? Asset { get; set; }
    public Employee? Employee { get; set; }
    public User? AssignedByUser { get; set; }
    #endregion
}
public enum AssetCondition
{
    [Display(Name = "عالی")]
    Excellent = 1,

    [Display(Name = "خوب")]
    Good = 2,

    [Display(Name = "متوسط")]
    Fair = 3,

    [Display(Name = "ضعیف")]
    Poor = 4
}