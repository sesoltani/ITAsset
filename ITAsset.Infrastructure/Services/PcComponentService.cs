using System.Text.Json;
using ITAsset.Data.Data;
using ITAsset.Data.Data.Seed;
using ITAsset.Domain.Common;
using ITAsset.Domain.Entities;
using ITAsset.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ITAsset.Data.Services;

public class PcComponentService : IPcComponentService
{
    private readonly AppDbContext _context;
    private readonly IAssetService _assetService;

    public PcComponentService(AppDbContext context, IAssetService assetService)
    {
        _context = context;
        _assetService = assetService;
    }
    public async Task<ResultModel<PcComponent>> AddAsync(PcComponent component, int userId)
    {
        // چک Asset
        var assetResult = await _assetService.GetByIdAsync(component.ParentAssetId);
        if(!assetResult.IsSuccess)
            return ResultModel<PcComponent>.Fail("کیس مقصد وجود ندارد");

        // چک ITCode مشترک
        bool itCodeExist =
            await _context.Assets.AnyAsync(a => a.ITCode == component.ITCode) ||
            await _context.PcComponents.AnyAsync(c => c.ITCode == component.ITCode);

        if (itCodeExist)
            return ResultModel<PcComponent>.Fail("این ITCode قبلاً استفاده شده است");

        _context.PcComponents.Add(component);
        await _context.SaveChangesAsync(); // اول ذخیره

        // ثبت لاگ نصب
        _context.PcComponentChanges.Add(new PcComponentChange
        {
            AssetId = component.ParentAssetId,
            PcComponentId = component.Id,
            ChangeType = ChangeType.Install,
            NewComponentInfo = JsonSerializer.Serialize(component),
            ChangedByUserId = userId
        });

        await _context.SaveChangesAsync();
        return ResultModel<PcComponent>.Success(component, "قطعه با موفقیت نصب شد");
    }

    public async Task<ResultModel<List<PcComponent>>> GetByAssetIdAsync(int assetId)
    {
        var components = await _context.PcComponents
            .Where(c => c.ParentAssetId == assetId)
            .ToListAsync();

        return ResultModel<List<PcComponent>>.Success(components);
    }

    public async Task<ResultModel<PcComponent>> GetByIdAsync(int id)
    {
        var component = await _context.PcComponents
            .Include(c => c.PcComponentChanges)
            .FirstOrDefaultAsync(a => a.Id == id);
        if(component==null) return ResultModel<PcComponent>.Fail("قطعه یافت نشد");

        return ResultModel<PcComponent>.Success(component);
    }

    public async Task<ResultModel<PcComponent>> GetByITCodeAsync(string itCode)
    {
        var component = await _context.PcComponents
            .FirstOrDefaultAsync(a => a.ITCode == itCode);
        if(component==null)return ResultModel<PcComponent>.Fail("قطعه یافت نشد");

        return ResultModel<PcComponent>.Success(component);
    }

    public async Task<ResultModel<bool>> RemoveAsync(int componentId, int userId)
    {
        var component = await _context.PcComponents.FindAsync(componentId);
        if(component==null)
            return ResultModel<bool>.Fail("قطعه یافت نشد");

        _context.PcComponents.Remove(component);

        _context.PcComponentChanges.Add(new PcComponentChange
        {
            AssetId = component.ParentAssetId,
            PcComponentId = null,
            ChangeType = ChangeType.Remove,
            OldComponentInfo = JsonSerializer.Serialize(component),
            ChangedByUserId = userId
        });

        await _context.SaveChangesAsync();

        return ResultModel<bool>.Success(true, "قطعه حذف شد");

    }

    public async Task<ResultModel<PcComponent>> ReplaceAsync(int oldComponentId, PcComponent newComponent, int userId)
    {
        var oldComponent = await _context.PcComponents
            .FindAsync(oldComponentId);
        if(oldComponent==null)
            return ResultModel<PcComponent>.Fail("قطعه قبلی یافت نشد");

        bool itCodeExist =
            await _context.Assets.AnyAsync(a => a.ITCode == newComponent.ITCode) ||
            await _context.PcComponents.AnyAsync(c =>
                c.ITCode == newComponent.ITCode && c.Id != oldComponent.Id);

        if (itCodeExist)
            return ResultModel<PcComponent>.Fail("این ITCode قبلاً استفاده شده است");

        string oldInfo = JsonSerializer.Serialize(oldComponent);

        _context.PcComponents.Remove(oldComponent);
        _context.PcComponents.Add(newComponent);

        _context.PcComponentChanges.Add(new PcComponentChange
        {
            AssetId = oldComponent.ParentAssetId,
            ChangeType = ChangeType.Replace,
            OldComponentInfo = oldInfo,
            NewComponentInfo = JsonSerializer.Serialize(newComponent),
            ChangedByUserId = userId
        });

        await _context.SaveChangesAsync();

        return ResultModel<PcComponent>.Success(newComponent, "تعویض قطعه انجام شد");
    }
}