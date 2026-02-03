using ITAsset.Data.Data;
using ITAsset.Domain.Common;
using ITAsset.Domain.Entities;
using ITAsset.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ITAsset.Data.Services;

public class AssetAssignmentHistoryService: IAssetAssignmentHistoryService
{
    private readonly AppDbContext _context;

    public AssetAssignmentHistoryService(AppDbContext context)
    {
        _context = context;
    }
    public async Task<ResultModel<List<AssetAssignmentHistory>>> GetByAssetIdAsync(int assetId)
    {
        var list = await _context.AssetAssignmentHistories
            .Include(x => x.Employee)
            .Include(x => x.ChangedByUser)
            .Where(x => x.AssetId == assetId)
            .OrderByDescending(x=>x.ChangeDate)
            .ToListAsync();

        return ResultModel<List<AssetAssignmentHistory>>.Success(list);
    }
}