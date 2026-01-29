using System.ComponentModel.DataAnnotations;

namespace ITAsset.Domain.Entities;

public class Model
{
    public int Id { get; set; }

    [Required, MaxLength(200)]
    public string? Name { get; set; }

    public int BrandId { get; set; }

    // مشخصات کلی مدل (JSON یا متن)
    public string? Specifications { get; set; }

    #region Navigation
    public Brand? Brand { get; set; }
    public ICollection<Asset> Assets { get; set; } = new List<Asset>();
    #endregion
}