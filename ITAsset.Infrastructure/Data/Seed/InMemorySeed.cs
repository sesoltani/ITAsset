using ITAsset.Domain.Entities;
using System.Xml.Linq;

namespace ITAsset.Data.Data.Seed;

public class InMemorySeed
{
    // Device Types
    public List<DeviceType> DeviceTypes { get; set; } = new()
    {
        new DeviceType{Id = 1,Name = "LAPTOP",CodePerfix = "LAP"},
        new DeviceType { Id = 2, Name = "Server",CodePerfix = "SRV" },
        new DeviceType { Id = 3, Name = "Printer",CodePerfix = "PRT" }
    };

    // Brands
    public List<Brand> Brands { get; set; } = new()
    {
        new Brand{Id = 1,Name = "DELL",DeviceTypeId = 2},
        new Brand { Id = 2, Name = "HP", DeviceTypeId = 1 },
        new Brand { Id = 3, Name = "Lenovo", DeviceTypeId = 1 }
    };

    // Models
    public List<Model> Models { get; set; } = new()
    {
        new Model { Id = 1, Name = "Latitude 5420", BrandId = 1 },
        new Model { Id = 2, Name = "ThinkPad X1", BrandId = 3 }
    };

    // Assets
    public List<Asset> Assets { get; set; } = new();

    // PcComponents
    public List<PcComponent> PcComponents { get; set; } = new();

    // Locations
    public List<Location> Locations { get; set; } = new()
    {
        new Location { Id = 1, Name = "Main Warehouse" },
        new Location { Id = 2, Name = "Building A - Floor 2", ParentLocationId = 1 }
    };

}

