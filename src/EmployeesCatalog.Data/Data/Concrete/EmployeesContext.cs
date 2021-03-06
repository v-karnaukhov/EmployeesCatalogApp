﻿using EmployeesCatalog.Data.Data.Entities;
using EmployeesCatalog.Data.Data.InitialData;
using EmployeesCatalog.Data.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace EmployeesCatalog.Data.Concrete
{
    public class EmployeesContext : IdentityDbContext<AppUser>
    {
        public EmployeesContext(DbContextOptions<EmployeesContext> options) : base(options)
        {
        }

        public DbSet<Organization> Organizations { get; set; }

        public DbSet<Department> Departments { get; set; }

        public DbSet<Employee> Employees { get; set; }

        public DbSet<EmployeeDepartmentsChangesHistory> DepartmentsChangesHistories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
          base.OnModelCreating(modelBuilder);
          // наполнение тестовых данных вынесено в расширение. См. ModelBuilderExtensions.cs.
          modelBuilder.Seed();
        }
    }
}
