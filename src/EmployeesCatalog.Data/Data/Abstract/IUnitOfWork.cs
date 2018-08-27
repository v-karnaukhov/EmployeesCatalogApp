using System;
using System.Threading.Tasks;
using EmployeesCatalog.Data.Data.Entities;
using EmployeesCatalog.Data.Entities;

namespace EmployeesCatalog.Data.Data.Abstract
{
    public interface IUnitOfWork : IDisposable
    {
        IGenericRepository<Organization> Organizations { get; }
        IGenericRepository<Department> Departments { get; }
        IGenericRepository<Employee> Employees { get; }
        IGenericRepository<EmployeeDepartmentsChangesHistory> EmployeeChangeDepartmentHistory { get; }

        int Save();
        Task<int> SaveAsync();
    }
}
