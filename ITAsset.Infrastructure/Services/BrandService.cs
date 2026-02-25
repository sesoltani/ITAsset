using ITAsset.Data.Data;
using ITAsset.Domain.Common;
using ITAsset.Domain.Entities;
using ITAsset.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ITAsset.Data.Services;

public class BrandService:IBrandService
{
    private readonly AppDbContext _context;

    public BrandService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<ResultModel<List<Brand>>> GetAllAsync()
    {
        var brands = await _context.Brands
            .Include(b => b.DeviceType)
            .Include(b => b.Models)
            .ToListAsync();

        return ResultModel<List<Brand>>.Success(brands);
    }

    public async Task<ResultModel<Brand>> GetByIdAsync(int id)
    {
        var brand = await _context.Brands
            .Include(b => b.DeviceType)
            .Include(b => b.Models)
            .FirstOrDefaultAsync(b => b.Id == id);

        if (brand == null)
            return ResultModel<Brand>.Fail("برند یافت نشد");

        return ResultModel<Brand>.Success(brand);
    }

    public async Task<ResultModel<Brand>> AddAsync(Brand brand)
    {
        // چک تکراری نبودن نام برند برای یک DeviceType
        var exists = await _context.Brands
            .AnyAsync(b => b.Name == brand.Name && b.DeviceTypeId == brand.DeviceTypeId);

        if (exists)
            return ResultModel<Brand>.Fail("این برند قبلاً برای این نوع دستگاه ثبت شده است");

        _context.Brands.Add(brand);
        await _context.SaveChangesAsync();

        return ResultModel<Brand>.Success(brand, "برند با موفقیت ثبت شد");
    }

    public async Task<ResultModel<Brand>> UpdateAsync(Brand brand)
    {
        var existing = await _context.Brands.FindAsync(brand.Id);
        if (existing == null)
            return ResultModel<Brand>.Fail("برند یافت نشد");

        // چک تکراری نبودن (به جز خودش)
        var exists = await _context.Brands
            .AnyAsync(b => b.Name == brand.Name &&
                          b.DeviceTypeId == brand.DeviceTypeId &&
                          b.Id != brand.Id);

        if (exists)
            return ResultModel<Brand>.Fail("این برند قبلاً برای این نوع دستگاه ثبت شده است");

        existing.Name = brand.Name;
        existing.DeviceTypeId = brand.DeviceTypeId;

        await _context.SaveChangesAsync();
        return ResultModel<Brand>.Success(existing, "ویرایش با موفقیت انجام شد");
    }

    public async Task<ResultModel<bool>> DeleteAsync(int id)
    {
        var brand = await _context.Brands
            .Include(b => b.Models)
            .FirstOrDefaultAsync(b => b.Id == id);

        if (brand == null)
            return ResultModel<bool>.Fail("برند یافت نشد");

        // چک کردن وجود مدل برای این برند
        if (brand.Models.Any())
            return ResultModel<bool>.Fail("این برند دارای مدل است و قابل حذف نیست");

        _context.Brands.Remove(brand);
        await _context.SaveChangesAsync();

        return ResultModel<bool>.Success(true, "برند با موفقیت حذف شد");
    }

    public async Task<ResultModel<List<Brand>>> GetByDeviceTypeAsync(int deviceTypeId)
    {
        var brands = await _context.Brands
            .Where(b => b.DeviceTypeId == deviceTypeId)
            .Include(b => b.Models)
            .ToListAsync();

        return ResultModel<List<Brand>>.Success(brands);
    }
}