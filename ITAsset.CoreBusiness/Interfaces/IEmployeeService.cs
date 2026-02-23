using ITAsset.Domain.Entities;

namespace ITAsset.Domain.Interfaces;

public interface IEmployeeService
{
    Task<List<Employee>> GetAllAsync();
    Task<Employee?> GetByIdAsync(int id);
}