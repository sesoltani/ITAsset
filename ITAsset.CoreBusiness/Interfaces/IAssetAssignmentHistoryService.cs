using ITAsset.Domain.Common;
using ITAsset.Domain.Entities;

namespace ITAsset.Domain.Interfaces;

public interface IAssetAssignmentHistoryService
{
    Task<ResultModel<List<AssetAssignmentHistory>>> GetByAssetIdAsync(int assetId);
}