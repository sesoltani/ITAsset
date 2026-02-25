using ITAsset.Domain.Common;
using ITAsset.Domain.Entities;

namespace ITAsset.Domain.Interfaces;

public interface IBrandService
{
    Task<ResultModel<List<Brand>>> GetAllAsync();
    Task<ResultModel<Brand>> GetByIdAsync(int id);
    Task<ResultModel<Brand>> AddAsync(Brand brand);
    Task<ResultModel<Brand>> UpdateAsync(Brand brand);
    Task<ResultModel<bool>> DeleteAsync(int id);
    Task<ResultModel<List<Brand>>> GetByDeviceTypeAsync(int deviceTypeId);
}