using System.ComponentModel.DataAnnotations;

namespace ITAsset.Domain.Entities;

public class MaintenanceRecord
{
    public int Id { get; set; }

    public int AssetId { get; set; }

    public DateTime MaintenanceDate { get; set; }
    public MaintenanceType MaintenanceType { get; set; }

    [StringLength(1000)]
    public string? Description { get; set; }

    public decimal? Cost { get; set; }

    [StringLength(200)]
    public string? PerformedBy { get; set; }

    public DateTime? NextMaintenanceDate { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    #region Navigation
    public Asset? Asset { get; set; }
    #endregion
}
public enum MaintenanceType
{
    Repair = 1,
    Service = 2
}