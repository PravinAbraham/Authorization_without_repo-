using Authorizations.Models;
using Azure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Authorizations.Controllers
{
    [Authorize(Roles = "Owner")]
    [Route("api/[controller]")]
    [ApiController]
    public class OwnerController : ControllerBase
    {
        private readonly EmployeeDbContext _employeeDbContext;
        public OwnerController(EmployeeDbContext employeeDbContext)
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


        [HttpPut("Update")]
        public ActionResult<Employee> Update([FromBody] Employee employee)
        {
            _employeeDbContext.employees.Update(employee);
            _employeeDbContext.SaveChanges();
            return Ok(employee);
        }


        [HttpDelete]
        public ActionResult DeleteById(int id)
        {
            var customer = _employeeDbContext.employees.Find(id);
            _employeeDbContext.employees.Remove(customer);
            _employeeDbContext.SaveChanges();
            return Ok(customer);
        }


        [HttpGet("GetById")]
        public Employee GetById(int Id)
        {
            var employee = _employeeDbContext.employees.Find(Id);
            return employee;
        }
    }
}
