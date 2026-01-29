using ITAsset.Data.Data;
using ITAsset.Domain.Common;
using ITAsset.Domain.Entities;
using ITAsset.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ITAsset.Data.Services;

public class ModelService : IModelService
{
    private readonly AppDbContext _context;

    public ModelService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<ResultModel<Model>> AddAsync(Model model)
    {
        _context.Models.Add(model);
        await _context.SaveChangesAsync();

        return ResultModel<Model>.Success(model, "مدل با موفقیت ثبت شد");
    }

    public async Task<ResultModel<List<Model>>> GetAllAsync()
    {
        var models = await _context.Models
            .Include(m => m.Brand)
            .ToListAsync();

        return ResultModel<List<Model>>.Success(models);
    }

    public async Task<ResultModel<Model>> GetByIdAsync(int id)
    {
       var model=await _context.Models
           .Include(m=>m.Brand)
           .FirstOrDefaultAsync(i=>i.Id==id);

       if(model==null)
           return ResultModel<Model>.Fail("مدل یافت نشد");
       return ResultModel<Model>.Success(model);
    }
}