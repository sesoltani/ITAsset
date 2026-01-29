using System.ComponentModel.DataAnnotations;

namespace ITAsset.WebApp.ViewModels;

public class PcComponentCreateVm
{
    [Required]
    public int ParentAssetId { get; set; }

    [Required, StringLength(50)]
    public string ComponentType { get; set; } = string.Empty;

    [Required, StringLength(50)]
    public string ITCode { get; set; } = string.Empty;

    [StringLength(100)]
    public string? Brand { get; set; }

    [StringLength(200)]
    public string? Model { get; set; }

    [StringLength(100)]
    public string? SerialNumber { get; set; }

    public string? Specifications { get; set; }
}