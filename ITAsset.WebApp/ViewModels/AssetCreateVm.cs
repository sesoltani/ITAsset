using System.ComponentModel.DataAnnotations;
using ITAsset.Domain.Entities;

namespace ITAsset.WebApp.ViewModels;

public class AssetCreateVm
{
    public string? AssetCode { get; set; }   // کد اموال (دستی)
    public string? ITCode { get; set; }       // کد IT (دستی)

    public int ModelId { get; set; }
    public int? LocationId { get; set; }

    public string? SerialNumber { get; set; }
    public DateTime? PurchaseDate { get; set; }
    public decimal? PurchasePrice { get; set; }
    public DateTime? WarrantyExpiryDate { get; set; }

    public AssetStatus Status { get; set; }
    public string? Notes { get; set; }
}