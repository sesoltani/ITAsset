using ITAsset.Domain.Common;
using ITAsset.Domain.Entities;

namespace ITAsset.Domain.Interfaces;

public interface IPcComponentHistoryService
{
    Task<ResultModel<List<PcComponentChange>>> GetByAssetIdAsync(int assetId);

    Task<ResultModel<PcComponentChange>> GetByIdAsync(int id);
}