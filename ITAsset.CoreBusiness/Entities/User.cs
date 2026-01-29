using System.ComponentModel.DataAnnotations;

namespace ITAsset.Domain.Entities;

public class User
{
    public int Id { get; set; }

    [Required, StringLength(50)]
    public string? UserName { get; set; }

    // پیشنهاد: اتصال به ASP.NET Identity
    [StringLength(500)]
    public string? PasswordHash { get; set; }

    public int? EmployeeId { get; set; }

    public UserRole Role { get; set; }
    public bool IsActive { get; set; } = true;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    #region Navigation
    public Employee? Employee { get; set; }

    public ICollection<PcComponentChange> PcComponentChanges { get; set; } = new List<PcComponentChange>();
    public ICollection<AssetAssignment> AssetAssignments { get; set; } = new List<AssetAssignment>();
    #endregion
}
public enum UserRole
{
    Admin = 1,
    ITStaff = 2,
    Viewer = 3
}