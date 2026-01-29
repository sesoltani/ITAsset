using ITAsset.Domain.Common;
using ITAsset.Domain.Entities;

namespace ITAsset.Domain.Interfaces;

public interface IModelService
{
    Task<ResultModel<List<Model>>> GetAllAsync();
    Task<ResultModel<Model>> GetByIdAsync(int id);
    Task<ResultModel<Model>> AddAsync(Model model);
}