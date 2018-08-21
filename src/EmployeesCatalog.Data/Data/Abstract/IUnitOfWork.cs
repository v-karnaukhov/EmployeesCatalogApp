﻿using System;
using System.Threading.Tasks;
using EmployeesCatalog.Data.Entities;

namespace EmployeesCatalog.Data.Data.Abstract
{
    public interface IUnitOfWork : IDisposable
    {
        IGenericRepository<Organization> Organizations { get; }
        IGenericRepository<Department> Departments { get; }
        IGenericRepository<Employee> Employees { get; }

        int Save();
        Task<int> SaveAsync();
    }
}
