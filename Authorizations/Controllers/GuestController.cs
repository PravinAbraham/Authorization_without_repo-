using Authorizations.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Authorizations.Controllers
{
    [Authorize(Roles = "Guest,AssistantManager,Manager,Owner")]
    [Route("api/[controller]")]
    [ApiController]
    public class GuestController : ControllerBase
    {
        private readonly EmployeeDbContext _employeeDbContext;
        public GuestController(EmployeeDbContext employeeDbContext)
        {
            _employeeDbContext = employeeDbContext;
        }


        [HttpGet("GetById")]
        public Employee GetById(int Id)
        {
            var employee = _employeeDbContext.employees.Find(Id);
            return employee;
        }
    }
}