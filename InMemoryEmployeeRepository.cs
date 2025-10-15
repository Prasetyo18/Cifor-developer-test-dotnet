using EmployeeApi.Models;
using System.Collections.Concurrent;

namespace EmployeeApi.Repositories
{
    public class InMemoryEmployeeRepository : IEmployeeRepository
    {
        private readonly ConcurrentDictionary<string, Employee> _store = new();

        public InMemoryEmployeeRepository()
        {
            var seed = new[]
            {
                new Employee{ EmployeeId="G0001", Name="John Doe", Address="Jl. Bogor Raya", Department="HR", Email="John.doe@gmail.com"},
                new Employee{ EmployeeId="G0002", Name="Sally Kun", Address="Kemang Raya", Department="HR", Email="Sally.kun@gmail.com"},
                new Employee{ EmployeeId="G0003", Name="Hiro Katajima", Address="Pluit, Jakarta", Department="ICT", Email="Hiro.Katajima@gmail.com"},
                new Employee{ EmployeeId="G0004", Name="Gareth Kelly", Address="Bandung", Department="ICT", Email="Gareth.kelly@gmail.com"},
                new Employee{ EmployeeId="G0005", Name="Rose Yang", Address="Depok", Department="IS", Email="Rose.yang@gmail.com"},
            };

            foreach (var e in seed) _store.TryAdd(e.EmployeeId, e);
        }

        public Task AddAsync(Employee employee)
        {
            if (!_store.TryAdd(employee.EmployeeId, employee))
                throw new InvalidOperationException("Employee with same id already exists.");
            return Task.CompletedTask;
        }

        public Task<bool> DeleteAsync(string id)
        {
            return Task.FromResult(_store.TryRemove(id, out _));
        }

        public Task<IEnumerable<Employee>> GetAllAsync()
        {
            return Task.FromResult(_store.Values.AsEnumerable());
        }

        public Task<Employee?> GetByIdAsync(string id)
        {
            _store.TryGetValue(id, out var e);
            return Task.FromResult(e);
        }

        public Task<IEnumerable<Employee>> SearchAsync(string? name, string? department)
        {
            var query = _store.Values.AsEnumerable();
            if (!string.IsNullOrWhiteSpace(name))
            {
                var n = name.Trim().ToLowerInvariant();
                query = query.Where(x => x.Name?.ToLowerInvariant().Contains(n) == true);
            }
            if (!string.IsNullOrWhiteSpace(department))
            {
                var d = department.Trim().ToLowerInvariant();
                query = query.Where(x => x.Department?.ToLowerInvariant().Contains(d) == true);
            }
            return Task.FromResult(query);
        }

        public Task<bool> UpdateAsync(string id, Employee employee)
        {
            if (!_store.ContainsKey(id)) return Task.FromResult(false);
            _store[id] = employee;
            return Task.FromResult(true);
        }
    }
}
