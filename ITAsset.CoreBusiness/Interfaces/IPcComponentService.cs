using ITAsset.Domain.Common;
using ITAsset.Domain.Entities;

namespace ITAsset.Domain.Interfaces;

public interface IPcComponentService
{
    Task<ResultModel<List<PcComponent>>> GetByAssetIdAsync(int assetId);

    Task<ResultModel<PcComponent>> GetByIdAsync(int id);

    Task<ResultModel<PcComponent>> GetByITCodeAsync(string itCode);

    Task<ResultModel<PcComponent>> AddAsync(PcComponent component, int userId);

    Task<ResultModel<PcComponent>> ReplaceAsync(int oldComponentId, PcComponent newComponent, int userId);

    Task<ResultModel<bool>> RemoveAsync(int componentId, int userId);
}