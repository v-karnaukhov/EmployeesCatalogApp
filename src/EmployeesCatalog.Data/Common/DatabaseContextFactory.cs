using System;
using EmployeesCatalog.Data.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace EmployeesCatalog.Data.Common
{
    public class DatabaseContextFactory : IDesignTimeDbContextFactory<EmployeesContext>
    {
        private static string _connectionString;

        public EmployeesContext CreateDbContext()
        {
            return CreateDbContext(args: null);
        }

        public EmployeesContext CreateDbContext(string[] args)
        {
            if (string.IsNullOrEmpty(_connectionString))
            {
                LoadConnectionString();
            }

            var builder = new DbContextOptionsBuilder<EmployeesContext>();
            builder.UseSqlServer(_connectionString);

            return new EmployeesContext(builder.Options);
        }

        public EmployeesContext CreateDbContext(string connectionString)
        {
            if (string.IsNullOrEmpty(_connectionString))
            {
                throw new ArgumentNullException(nameof(connectionString));
            }

            var builder = new DbContextOptionsBuilder<EmployeesContext>();
            builder.UseSqlServer(connectionString);

            return new EmployeesContext(builder.Options);
        }

        private static void LoadConnectionString()
        {
            var builder = new ConfigurationBuilder();
            builder.AddJsonFile("appsettings.json", optional: false);

            var configuration = builder.Build();

            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }
    }
}
