using System.ComponentModel.DataAnnotations;

namespace ITAsset.Domain.Entities;

public class AssetAssignmentHistory
{
    public int Id { get; set; }

    public int AssetId { get; set; }
    public int? EmployeeId { get; set; } // در صورت تحویل

    public AssignmentAction Action { get; set; }

    public AssetCondition? Condition { get; set; }

    public int ChangedByUserId { get; set; }
    public DateTime ChangeDate { get; set; } = DateTime.UtcNow;

    [StringLength(500)]
    public string? Note { get; set; }

    #region Navigation
    public Asset? Asset { get; set; }
    public Employee? Employee { get; set; }
    public User? ChangedByUser { get; set; }
    #endregion
}

public enum AssignmentAction
{
    Assign = 1,
    Return = 2
}