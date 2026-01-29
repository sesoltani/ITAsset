using ITAsset.Data.Data;
using ITAsset.Domain.Common;
using ITAsset.Domain.Entities;
using ITAsset.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ITAsset.Data.Services;

public class PcComponentHistoryService: IPcComponentHistoryService
{
    private readonly AppDbContext _context;

    public PcComponentHistoryService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<ResultModel<List<PcComponentChange>>> GetByAssetIdAsync(int assetId)
    {
        var history = await _context.PcComponentChanges
            .Where(h => h.AssetId == assetId)
            .OrderByDescending(h => h.ChangeDate)
            .ToListAsync();
        return ResultModel<List<PcComponentChange>>.Success(history);
    }

    public async Task<ResultModel<PcComponentChange>> GetByIdAsync(int id)
    {
        var item = await _context.PcComponentChanges
            .Include(i => i.PcComponent)
            .FirstOrDefaultAsync(i => i.Id == id);

        if (item==null)
            return ResultModel<PcComponentChange>.Fail("رکورد تاریخچه یافت نشد");

        return ResultModel<PcComponentChange>.Success(item);
    }
}