using Authorizations.Models;
using Microsoft.EntityFrameworkCore;

namespace Authorizations
{
    public class EmployeeDbContext : DbContext
    {
        public EmployeeDbContext(DbContextOptions<EmployeeDbContext> options) : base(options)
        {

        }
        public DbSet<Employee> employees { get; set; }
    }
}
