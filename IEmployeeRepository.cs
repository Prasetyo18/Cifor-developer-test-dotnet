using EmployeeApi.Models;

namespace EmployeeApi.Repositories
{
    public interface IEmployeeRepository
    {
        Task<IEnumerable<Employee>> GetAllAsync();
        Task<Employee?> GetByIdAsync(string id);
        Task<IEnumerable<Employee>> SearchAsync(string? name, string? department);
        Task AddAsync(Employee employee);
        Task<bool> UpdateAsync(string id, Employee employee);
        Task<bool> DeleteAsync(string id);
    }
}
