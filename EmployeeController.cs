using EmployeeApi.Models;
using EmployeeApi.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeApi.Controllers
{
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeRepository _repo;
        public EmployeeController(IEmployeeRepository repo) { _repo = repo; }

        [HttpGet("Get")]
        public async Task<IActionResult> GetAll()
        {
            var list = await _repo.GetAllAsync();
            return Ok(list);
        }

        [HttpGet("Get/{employeeId}")]
        public async Task<IActionResult> GetById(string employeeId)
        {
            var item = await _repo.GetByIdAsync(employeeId);
            if (item == null) return NotFound(new { message = "Employee not found" });
            return Ok(item);
        }

        [HttpGet("Get/employees/search")]
        public async Task<IActionResult> Search([FromQuery] string? name, [FromQuery] string? department)
        {
            var results = await _repo.SearchAsync(name, department);
            return Ok(results);
        }

        [HttpPost("employees")]
        public async Task<IActionResult> Create([FromBody] Employee emp)
        {
            if (emp == null) return BadRequest(new { message = "Invalid payload" });
            if (string.IsNullOrWhiteSpace(emp.EmployeeId)) return BadRequest(new { message = "EmployeeId is required" });

            var existing = await _repo.GetByIdAsync(emp.EmployeeId);
            if (existing != null) return BadRequest(new { message = "EmployeeId already exists" });

            await _repo.AddAsync(emp);
            return CreatedAtAction(nameof(GetById), new { employeeId = emp.EmployeeId }, emp);
        }

        [HttpPut("employees/{employeeId}")]
        public async Task<IActionResult> Update(string employeeId, [FromBody] Employee emp)
        {
            if (emp == null || employeeId != emp.EmployeeId) return BadRequest(new { message = "Invalid payload or ID mismatch" });

            var updated = await _repo.UpdateAsync(employeeId, emp);
            if (!updated) return NotFound(new { message = "Employee not found" });
            return Ok(emp);
        }

        [HttpDelete("employees/{employeeId}")]
        public async Task<IActionResult> Delete(string employeeId)
        {
            var deleted = await _repo.DeleteAsync(employeeId);
            if (!deleted) return NotFound(new { message = "Employee not found" });
            return Ok(new { message = "Deleted" });
        }
    }
}
