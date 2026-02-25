using ITAsset.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ITAsset.Data.Data.Seed;

public static class DataSeeder
{
    public static void Seed(ModelBuilder modelBuilder)
    {
        // 1. DeviceType
        modelBuilder.Entity<DeviceType>().HasData(
            new DeviceType { Id = 1, Name = "کامپیوتر رومیزی", CodePerfix = "PC" },
            new DeviceType { Id = 2, Name = "لپ تاپ", CodePerfix = "LT" },
            new DeviceType { Id = 3, Name = "سوییچ", CodePerfix = "SW" },
            new DeviceType { Id = 4, Name = "پرینتر", CodePerfix = "PR" }
        );

        // 2. Brand
        modelBuilder.Entity<Brand>().HasData(
            new Brand { Id = 1, Name = "Dell", DeviceTypeId = 1 },
            new Brand { Id = 2, Name = "HP", DeviceTypeId = 1 },
            new Brand { Id = 3, Name = "Cisco", DeviceTypeId = 3 }
        );

        // 3. Model
        modelBuilder.Entity<Model>().HasData(
            new Model { Id = 1, Name = "Optiplex 3080", BrandId = 1 },
            new Model { Id = 2, Name = "EliteBook 840", BrandId = 2 }
        );

        // 4. Location
        modelBuilder.Entity<Location>().HasData(
            new Location { Id = 1, Name = "انبار مرکزی" },
            new Location { Id = 2, Name = "طبقه اول - اتاق سرور", ParentLocationId = 1 }
        );

        // 5. Employee
        modelBuilder.Entity<Employee>().HasData(
            new Employee
            {
                Id = 1,
                EmployeeCode = "EMP001",
                FirstName = "علی",
                LastName = "محمدی",
                Department = "فناوری اطلاعات",
                Position = "کارشناس شبکه",
                Email = "ali@company.com",
                IsActive = true,
                CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc) // ✅ مقدار ثابت
            },
            new Employee
            {
                Id = 2,
                EmployeeCode = "EMP002",
                FirstName = "مریم",
                LastName = "احمدی",
                Department = "مالی",
                Position = "کارشناس مالی",
                Email = "maryam@company.com",
                IsActive = true,
                CreatedAt = new DateTime(2025, 8, 1, 0, 0, 0, DateTimeKind.Utc) // ✅ مقدار ثابت
            }
        );
    }
}