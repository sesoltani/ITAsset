using ITAsset.Data.Data.Seed;
using ITAsset.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ITAsset.Data.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    // ===== DbSets =====
    public DbSet<Asset> Assets => Set<Asset>();
    public DbSet<PcComponent> PcComponents => Set<PcComponent>();
    public DbSet<Location> Locations => Set<Location>();

    public DbSet<DeviceType> DeviceTypes => Set<DeviceType>();
    public DbSet<Brand> Brands => Set<Brand>();
    public DbSet<Model> Models => Set<Model>();

    public DbSet<Employee> Employees => Set<Employee>();
    public DbSet<User> Users => Set<User>();

    public DbSet<AssetAssignment> AssetAssignments => Set<AssetAssignment>();
    public DbSet<AssetAssignmentHistory> AssetAssignmentHistories => Set<AssetAssignmentHistory>();
    public DbSet<MaintenanceRecord> MaintenanceRecords => Set<MaintenanceRecord>();
    public DbSet<PcComponentChange> PcComponentChanges => Set<PcComponentChange>();

    // ========== Fluent API ==========
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        ConfigureAsset(modelBuilder);
        ConfigureDeviceType(modelBuilder);
        ConfigureBrand(modelBuilder);
        ConfigureModel(modelBuilder);
        ConfigureLocation(modelBuilder);
        ConfigureAssignments(modelBuilder);
        ConfigurePcComponent(modelBuilder);
        ConfigureAssignmentHistory(modelBuilder);
        ConfigureMaintenance(modelBuilder);

        //data seeder
        DataSeeder.Seed(modelBuilder);
    }

    private void ConfigureMaintenance(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<MaintenanceRecord>(entity =>
        {
            entity.Property(x => x.Cost)
                .HasColumnType("decimal(18,2)");
        });
    }

    private void ConfigureAsset(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Asset>(entity =>
        {
            entity.HasKey(a => a.Id);

            entity.Property(a => a.ITCode)
                .IsRequired()
                .HasMaxLength(50);

            entity.HasIndex(a => a.ITCode)
                .IsUnique(); // یکتا

            entity.Property(a => a.AssetCode)
                .HasMaxLength(50);

            entity.HasOne(a => a.Model)
                .WithMany(m => m.Assets)
                .HasForeignKey(a => a.ModelId)
                .OnDelete(DeleteBehavior.Restrict);

            entity.Property(a => a.CreatedAt)
                .HasDefaultValueSql("GETUTCDATE()");

            entity.Property(a => a.LastUpdatedAt)
                .HasDefaultValueSql("GETUTCDATE()");

            // اضافه کردن precision برای PurchasePrice
            entity.Property(a => a.PurchasePrice)
                .HasColumnType("decimal(18,2)"); // 18 رقم کل، 2 رقم اعشار
        });
    }


    private void ConfigureDeviceType(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<DeviceType>()
            .HasMany(x => x.Brands)
            .WithOne(x => x.DeviceType)
            .HasForeignKey(x => x.DeviceTypeId);
    }

    private void ConfigureBrand(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Brand>()
            .HasMany(x => x.Models)
            .WithOne(x => x.Brand)
            .HasForeignKey(x => x.BrandId);
    }

    private void ConfigureModel(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Model>()
            .HasMany(x => x.Assets)
            .WithOne(x => x.Model)
            .HasForeignKey(x => x.ModelId);
    }
    private void ConfigureLocation(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Location>(entity =>
        {
            entity.HasKey(l => l.Id);

            entity.Property(l => l.Name)
                .IsRequired()
                .HasMaxLength(200);

            entity.HasOne(l => l.ParentLocation)
                .WithMany(l => l.Children)
                .HasForeignKey(l => l.ParentLocationId)
                .OnDelete(DeleteBehavior.Restrict);
        });
    }

    private void ConfigureAssignments(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AssetAssignment>(entity =>
        {
            entity.HasOne(x => x.Asset)
                .WithMany(x => x.AssetAssignments)
                .HasForeignKey(x => x.AssetId)
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(x => x.Employee)
                .WithMany(x => x.AssetAssignments)
                .HasForeignKey(x => x.EmployeeId);

            entity.HasOne(x => x.AssignedByUser)
                .WithMany(x => x.AssetAssignments)
                .HasForeignKey(x => x.AssignedByUserId)
                .OnDelete(DeleteBehavior.Restrict);
            entity.HasIndex(x => new { x.AssetId, x.ActualReturnDate });

        });
    }
    private void ConfigurePcComponent(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<PcComponent>(entity =>
        {
            entity.HasKey(c => c.Id);

            entity.Property(c => c.ITCode)
                .IsRequired()
                .HasMaxLength(50);

            entity.HasIndex(c => c.ITCode)
                .IsUnique(); // یکتا

            entity.Property(c => c.ComponentType)
                .IsRequired()
                .HasMaxLength(50);

            entity.HasOne(c => c.ParentAsset)
                .WithMany(a => a.PcComponents)
                .HasForeignKey(c => c.ParentAssetId)
                .OnDelete(DeleteBehavior.Cascade);
        });
    }

    private void ConfigureAssignmentHistory(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AssetAssignmentHistory>(entity =>
        {
            entity.HasKey(x => x.Id);

            entity.HasOne(x => x.Asset)
                .WithMany()
                .HasForeignKey(x => x.AssetId)
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(x => x.Employee)
                .WithMany()
                .HasForeignKey(x => x.EmployeeId)
                .OnDelete(DeleteBehavior.SetNull);

            entity.HasOne(x => x.ChangedByUser)
                .WithMany()
                .HasForeignKey(x => x.ChangedByUserId)
                .OnDelete(DeleteBehavior.Restrict);
        });
    }


}

