using ITAsset.Domain.Common;
using ITAsset.Domain.Entities;

namespace ITAsset.Domain.Interfaces;

public interface ILocationService
{
    Task<ResultModel<List<Location>>> GetAllAsync();
    Task<ResultModel<Location>> GetByIdAsync(int id);
    Task<ResultModel<Location>> AddAsync(Location location);
}