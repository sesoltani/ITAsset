using ITAsset.Data.Data;
using ITAsset.Domain.Entities;
using ITAsset.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ITAsset.Data.Services;

public class EmployeeService: IEmployeeService
{
    private readonly AppDbContext _context;

    public EmployeeService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<Employee>> GetAllAsync()
    {
        return await _context.Employees
            .Where(e => e.IsActive) // فقط کارمندان فعال
            .ToListAsync();
    }

    public async Task<Employee?> GetByIdAsync(int id)
    {
        return await _context.Employees
            .FirstOrDefaultAsync(e => e.Id == id && e.IsActive);
    }
}