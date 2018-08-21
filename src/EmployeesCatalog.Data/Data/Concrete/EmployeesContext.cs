using EmployeesCatalog.Data.Entities;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;

namespace EmployeesCatalog.Data.Concrete
{
    public class EmployeesContext : DbContext
    {
        public EmployeesContext(DbContextOptions<EmployeesContext> options) : base(options)
        {
        }

        public DbSet<Organization> Organizations { get; set; }

        public DbSet<Department> Departments { get; set; }

        public DbSet<Employee> Employees { get; set; }
    }
}
