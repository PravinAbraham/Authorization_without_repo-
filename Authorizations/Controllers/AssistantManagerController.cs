using Authorizations.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Authorizations.Controllers
{
    [Authorize(Roles = "AssistantManager,Manager,Owner")]
    [Route("api/[controller]")]
    [ApiController]
    public class AssistantManagerController : ControllerBase
    {
        private readonly EmployeeDbContext _employeeDbContext;
        public AssistantManagerController(EmployeeDbContext employeeDbContext)
        {
            _employeeDbContext = employeeDbContext;
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            var response = await _employeeDbContext.employees.ToListAsync();
            return Ok(response);
        }


        [HttpPost("Create")]
        public ActionResult<Employee> Create([FromBody] Employee employee)
        {
            _employeeDbContext.employees.Add(employee);
            _employeeDbContext.SaveChanges();
            return Ok(employee);
        }


        [HttpGet("GetById")]
        public Employee GetById(int Id)
        {
            var employee = _employeeDbContext.employees.Find(Id);
            return employee;
        }
    }
}

