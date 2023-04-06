using Authorizations.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Authorizations.Controllers
{
    [Authorize(Roles = "Manager,Owner")]
    [Route("api/[controller]")]
    [ApiController]
    public class ManagerController : ControllerBase
    {
        private readonly EmployeeDbContext _employeeDbContext;
        public ManagerController(EmployeeDbContext employeeDbContext)
        {
            _employeeDbContext = employeeDbContext;
        }

        [HttpGet("GetAll")]
        public List<Manager> GetAll()
        {
            var managers = _employeeDbContext.manager.ToList();

            return managers.ToList();
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


        [HttpGet("GetById")]
        public Employee GetById(int Id)
        {
            var employee = _employeeDbContext.employees.Find(Id);
            return employee;
        }
    }
}
