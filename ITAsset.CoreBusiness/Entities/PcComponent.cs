using System.ComponentModel.DataAnnotations;

namespace ITAsset.Domain.Entities;

public class PcComponent
{
    public int Id { get; set; }

    // کد یکتای قطعه
    [Required, StringLength(50)]
    public string? ITCode { get; set; }

    // CPU, RAM, HDD, GPU, PSU, ...
    [Required, StringLength(50)]
    public string? ComponentType { get; set; }

    [StringLength(100)]
    public string? Brand { get; set; }

    [StringLength(200)]
    public string? Model { get; set; }

    // مشخصات فنی (JSON)
    public string? Specifications { get; set; }

    [StringLength(100)]
    public string? SerialNumber { get; set; }

    // کیس مادر
    public int ParentAssetId { get; set; }

    public DateTime InstallationDate { get; set; } = DateTime.UtcNow;

    #region Navigation
    public Asset? ParentAsset { get; set; }
    public ICollection<PcComponentChange> PcComponentChanges { get; set; } = new List<PcComponentChange>();
    #endregion
}