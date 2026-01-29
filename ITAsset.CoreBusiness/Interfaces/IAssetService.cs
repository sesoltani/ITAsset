using ITAsset.Domain.Common;
using ITAsset.Domain.Entities;

namespace ITAsset.Domain.Interfaces;

public interface IAssetService
{
    Task<ResultModel<List<Asset>>> GetAllAsync();
    Task<ResultModel<Asset>> GetByIdAsync(int id);
    Task<ResultModel<Asset>> GetByITCodeAsync(string itCode);
    Task<ResultModel<bool>> IsITCodeExistsAsync(string itCode);
    Task<ResultModel<Asset>> AddAsync(Asset asset);
    Task<ResultModel<Asset>> UpdateAsync(Asset asset);
    Task<ResultModel<bool>> DeleteAsync(int id);
}