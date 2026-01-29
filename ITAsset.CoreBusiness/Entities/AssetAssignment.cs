
namespace ITAsset.Domain.Entities;

public class AssetAssignment
{
    public int Id { get; set; }

    public int AssetId { get; set; }
    public int EmployeeId { get; set; }

    // کاربر IT که تحویل داده
    public int AssignedByUserId { get; set; }

    public DateTime AssignedDate { get; set; } = DateTime.UtcNow;
    public DateTime? ExpectedReturnDate { get; set; }
    public DateTime? ActualReturnDate { get; set; }

    public AssetCondition ConditionOnAssign { get; set; }
    public AssetCondition? ConditionOnReturn { get; set; }

    public bool IsActive { get; set; } = true;

    #region Navigation
    public Asset? Asset { get; set; }
    public Employee? Employee { get; set; }
    public User? AssignedByUser { get; set; }
    #endregion
}
public enum AssetCondition
{
    Excellent = 1,
    Good = 2,
    Fair = 3,
    Poor = 4
}