using EmployeesCatalog.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace EmployeesCatalog.Data.Data.InitialData
{
    public static class ModelBuilderExtensions
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Organization>().HasData(
                new Organization
                {
                    OrganizationId = 1,
                    Name = "Тестовая организация 1"
                }, new Organization
                {
                    OrganizationId = 2,
                    Name = "Тестовая организация 2"
                }, new Organization
                {
                    OrganizationId = 3,
                    Name = "Тестовая организация 3"
                });

            modelBuilder.Entity<Department>().HasData(
                new Department
                {
                    DepartmentId = 1,
                    OrganizationId = 1,
                    Name = "Департамент 1"
                },
                new Department
                {
                    DepartmentId = 2,
                    OrganizationId = 1,
                    Name = "Департамент 2"
                },
                new Department
                {
                    DepartmentId = 3,
                    OrganizationId = 1,
                    Name = "Департамент 3"
                },
                new Department
                {
                    DepartmentId = 4,
                    OrganizationId = 2,
                    Name = "Департамент 1"
                },
                new Department
                {
                    DepartmentId = 5,
                    OrganizationId = 2,
                    Name = "Департамент 2"
                },
                new Department
                {
                    DepartmentId = 6,
                    OrganizationId = 3,
                    Name = "Департамент 1"
                });

            modelBuilder.Entity<Employee>().HasData(
                new Employee
                {
                    EmployeeId = 1,
                    FirstName = "Иван",
                    Patronymic = "Иванович",
                    Surname = "Иванов",
                    Email = "ivanov@test.ru",
                    DepartmentId = 1
                },
                new Employee
                {
                    EmployeeId = 2,
                    FirstName = "Петр",
                    Patronymic = "Петрович",
                    Surname = "Петров",
                    DepartmentId = 4,
                    Email = "petrov@test.ru"
                },
                new Employee
                {
                    DepartmentId = 6,
                    EmployeeId = 3,
                    FirstName = "Сидор",
                    Patronymic = "Сидорович",
                    Surname = "Сидоров",
                    Email = "sidorov@test.ru"
                });
        }
    }
}
