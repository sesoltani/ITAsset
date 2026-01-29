using ITAsset.Data.Data;
using ITAsset.Domain.Common;
using ITAsset.Domain.Entities;
using ITAsset.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ITAsset.Data.Services;

public class LocationService : ILocationService
{
    private readonly AppDbContext _context;

    public LocationService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<ResultModel<Location>> AddAsync(Location location)
    {
        if (location.ParentLocationId == 0)
            location.ParentLocationId = null;

        _context.Locations.Add(location);
        await _context.SaveChangesAsync();

        return ResultModel<Location>.Success(location, "مکان با موفقیت ثبت شد");
    }

    public async Task<ResultModel<List<Location>>> GetAllAsync()
    {
        var locations=await _context.Locations
            .Include(l=>l.ParentLocation)
            .ToListAsync();
        return ResultModel<List<Location>>.Success(locations);
    }

    public async Task<ResultModel<Location>> GetByIdAsync(int id)
    {
        var location = await _context.Locations
            .Include(l => l.Children)
            .FirstOrDefaultAsync(a => a.Id == id);
        if(location==null)
            return ResultModel<Location>.Fail("مکان یافت نشد");
        return ResultModel<Location>.Success(location);
    }
}