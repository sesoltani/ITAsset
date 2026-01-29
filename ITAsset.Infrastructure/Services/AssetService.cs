using ITAsset.Data.Data;
using ITAsset.Domain.Common;
using ITAsset.Domain.Entities;
using ITAsset.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ITAsset.Data.Services;

public class AssetService : IAssetService
{
    private readonly AppDbContext _context;

    public AssetService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<ResultModel<Asset>> AddAsync(Asset asset)
    {
        // چک تکراری بودن AssetCode
        if(await _context.Assets.AnyAsync(a=>a.AssetCode==asset.AssetCode))
            return ResultModel<Asset>.Fail("این کد اموال قبلاً ثبت شده است.");

        // چک تکراری بودن ITCode (بین Asset و PcComponent)
        bool itCodeExists =
            await _context.Assets.AnyAsync(a => a.ITCode == asset.ITCode) ||
            await _context.PcComponents.AnyAsync(a => a.ITCode == asset.ITCode);

        if(itCodeExists)
            return ResultModel<Asset>.Fail("این ITCode قبلاً ثبت شده است.");

        _context.Assets.Add(asset);
        await _context.SaveChangesAsync();

        return ResultModel<Asset>.Success(asset,  "ثبت با موفقیت انجام شد");
    }

    public async Task<ResultModel<bool>> DeleteAsync(int id)
    {
        var asset = await _context.Assets.FindAsync(id);
        if(asset==null)
            return ResultModel<bool>.Fail("آیتم پیدا نشد");
        _context.Assets.Remove(asset);
        await _context.SaveChangesAsync();
        
        return ResultModel<bool>.Success(true, "حذف با موفقیت انجام شد");
    }

    public async Task<ResultModel<List<Asset>>> GetAllAsync()
    {
        var assets = await _context.Assets
            .Include(a => a.Model)
            .Include(c => c.PcComponents)
            .ToListAsync();
        return ResultModel<List<Asset>>.Success(assets);
    }

    public async Task<ResultModel<Asset>> GetByIdAsync(int id)
    {
        var asset = await _context.Assets
            .Include(a => a.Model)
            .Include(c => c.PcComponents)
            .FirstOrDefaultAsync(a => a.Id == id);

        if (asset == null)
            return ResultModel<Asset>.Fail("آیتم پیدا نشد.");
        return ResultModel<Asset>.Success(asset);
    }

    public async Task<ResultModel<Asset>> GetByITCodeAsync(string itCode)
    {
        var asset = await _context.Assets
            .Include(a => a.Model)
            .Include(c => c.PcComponents)
            .FirstOrDefaultAsync(a => a.ITCode == itCode);
        if (asset == null) return ResultModel<Asset>.Fail("آیتم پیدا نشد");
        return ResultModel<Asset>.Success(asset);
    }

    public async Task<ResultModel<bool>> IsITCodeExistsAsync(string itCode)
    {
        bool exists = await _context.Assets.AnyAsync(a => a.ITCode == itCode)
            || await _context.PcComponents.AnyAsync(c => c.ITCode == itCode);

        return ResultModel<bool>.Success(exists);
    }

    public async Task<ResultModel<Asset>> UpdateAsync(Asset asset)
    {
        var existing = await _context.Assets.FindAsync(asset.Id);
        if(existing==null)
            return ResultModel<Asset>.Fail("آیتم پیدا نشد");

        // اگر ITCode تغییر کرده، بررسی یکتا بودن
        if (existing.ITCode !=asset.ITCode)
        {
            var itCodeChech = await IsITCodeExistsAsync(asset.ITCode);
            if(itCodeChech.Data==true)
                return ResultModel<Asset>.Fail("این ITCode قبلاً ثبت شده است.");
        }

        // به‌روزرسانی فیلدها
        existing.AssetCode = asset.AssetCode;
        existing.ITCode= asset.ITCode;
        existing.ModelId = asset.ModelId;
        existing.SerialNumber= asset.SerialNumber;
        existing.PurchaseDate= asset.PurchaseDate;
        existing.PurchasePrice= asset.PurchasePrice;
        existing.WarrantyExpiryDate= asset.WarrantyExpiryDate;
        existing.Status= asset.Status;
        existing.Location= asset.Location;
        existing.Notes= asset.Notes;
        existing.LastUpdatedAt = DateTime.UtcNow;
        await _context.SaveChangesAsync();

        return ResultModel<Asset>.Success(existing, "به‌روزرسانی با موفقیت انجام شد");
    }
}