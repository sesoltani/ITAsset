using ITAsset.Domain.Common;
using System.ComponentModel.DataAnnotations;

namespace ITAsset.Domain.Entities;

public class Asset
{
    public int Id { get; set; }

    // کد یکتای داخلی IT (قبل از صدور کد اموال)
    [Required, StringLength(50)]
    public string? ITCode { get; set; }

    // کد اموال سازمان (ممکن است بعداً صادر شود)
    [StringLength(50)]
    public string? AssetCode { get; set; }

    // مدل تجهیز (Laptop, PC, Switch, ...)
    public int ModelId { get; set; }

    [StringLength(100)]
    public string? SerialNumber { get; set; }

    public DateTime? PurchaseDate { get; set; }
    public decimal? PurchasePrice { get; set; }
    public DateTime? WarrantyExpiryDate { get; set; }

    public AssetStatus Status { get; set; } = AssetStatus.Available;

    // محل استقرار فعلی تجهیز
    public int? LocationId { get; set; }

    [StringLength(1000)]
    public string? Notes { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime LastUpdatedAt { get; set; } = DateTime.UtcNow;

    #region Navigation
    public Model? Model { get; set; }
    public Location? Location { get; set; }

    // فقط برای PC / Case استفاده می‌شود
    public ICollection<PcComponent> PcComponents { get; set; } = new List<PcComponent>();

    public ICollection<AssetAssignment> AssetAssignments { get; set; } = new List<AssetAssignment>();
    public ICollection<MaintenanceRecord> MaintenanceRecords { get; set; } = new List<MaintenanceRecord>();
    #endregion
}