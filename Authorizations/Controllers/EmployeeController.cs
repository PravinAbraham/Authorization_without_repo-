using Authorizations.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Authorizations.Controllers
{
    [Authorize (Roles = "Employee,AssistantManager,Manager,Owner")]
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly EmployeeDbContext _employeeDbContext;
        public EmployeeController(EmployeeDbContext employeeDbContext)
        {
            _employeeDbContext = employeeDbContext;
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            var response = await _employeeDbContext.employees.ToListAsync();
            return Ok(response);
        }


        [HttpGet("GetById")]
        public Employee GetById(int Id)
        {
            var employee = _employeeDbContext.employees.Find(Id);
            return employee;
        }
    }
}

