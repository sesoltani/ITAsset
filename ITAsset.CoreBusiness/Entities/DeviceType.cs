using System.ComponentModel.DataAnnotations;

namespace ITAsset.Domain.Entities;

public class DeviceType
{
    public int Id { get; set; }

    // PC, Laptop, Switch, Printer, ...
    [Required, MaxLength(100)]
    public string? Name { get; set; }

    // پیشوند کد IT (مثلاً PC, SW, LT)
    [Required, MaxLength(10)]
    public string? CodePerfix { get; set; }

    #region Navigation
    public ICollection<Brand> Brands { get; set; } = new List<Brand>();
    #endregion
}