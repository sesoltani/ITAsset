using System.ComponentModel.DataAnnotations;

namespace ITAsset.Domain.Entities;

public class PcComponentChange
{
    public int Id { get; set; }

    public int AssetId { get; set; }
    public int? PcComponentId { get; set; } // Null اگر حذف شده

    public ChangeType ChangeType { get; set; }

    public string? OldComponentInfo { get; set; }
    public string? NewComponentInfo { get; set; }

    public int ChangedByUserId { get; set; }
    public DateTime ChangeDate { get; set; } = DateTime.UtcNow;

    [StringLength(500)]
    public string? Reason { get; set; }

    #region Navigation
    public Asset? Asset { get; set; }
    public PcComponent? PcComponent { get; set; }
    public User? ChangedByUser { get; set; }
    #endregion
}
public enum ChangeType
{
    Install = 1,
    Replace = 2,
    Remove = 3
}