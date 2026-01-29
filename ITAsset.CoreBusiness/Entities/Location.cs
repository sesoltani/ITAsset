using System.ComponentModel.DataAnnotations;

namespace ITAsset.Domain.Entities;

public class Location
{
    public int Id { get; set; }

    // مثل: انبار مرکزی / ساختمان A / طبقه 2 / اتاق سرور
    [Required, MaxLength(200)]
    public string? Name { get; set; }

    // برای ساخت درخت
    public int? ParentLocationId { get; set; }

    #region Navigation
    public Location? ParentLocation { get; set; }
    public ICollection<Location> Children { get; set; } = new List<Location>();
    #endregion
}