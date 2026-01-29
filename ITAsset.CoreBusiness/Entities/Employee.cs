using System.ComponentModel.DataAnnotations;

namespace ITAsset.Domain.Entities;

public class Employee
{
    public int Id { get; set; }

    [Required, StringLength(20)]
    public string? EmployeeCode { get; set; }

    [Required, StringLength(50)]
    public string? FirstName { get; set; }

    [Required, StringLength(50)]
    public string? LastName { get; set; }

    [Required, StringLength(50)]
    public string? Department { get; set; }

    [StringLength(100)]
    public string? Position { get; set; }

    [StringLength(20)]
    public string? PhoneNumber { get; set; }

    [StringLength(100)]
    public string? Email { get; set; }

    public bool IsActive { get; set; } = true;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    #region Navigation
    public ICollection<AssetAssignment> AssetAssignments { get; set; } = new List<AssetAssignment>();
    #endregion
}