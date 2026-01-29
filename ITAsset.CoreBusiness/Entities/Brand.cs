using System.ComponentModel.DataAnnotations;

namespace ITAsset.Domain.Entities;

public class Brand
{
    public int Id { get; set; }

    [Required, MaxLength(100)]
    public string? Name { get; set; }

    public int DeviceTypeId { get; set; }

    #region Navigation
    public DeviceType? DeviceType { get; set; }
    public ICollection<Model> Models { get; set; } = new List<Model>();
    #endregion
}