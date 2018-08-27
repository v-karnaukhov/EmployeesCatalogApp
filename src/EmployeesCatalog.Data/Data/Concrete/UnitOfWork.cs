using System;
using System.Threading.Tasks;
using EmployeesCatalog.Data.Common;
using EmployeesCatalog.Data.Concrete;
using EmployeesCatalog.Data.Data.Abstract;
using EmployeesCatalog.Data.Data.Entities;
using EmployeesCatalog.Data.Entities;

namespace EmployeesCatalog.Data.Data.Concrete
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly EmployeesContext _context;

        private IGenericRepository<Organization> _organizationRepository;
        private IGenericRepository<Department> _departmentRepository;
        private IGenericRepository<Employee> _employeeRepository;
        private IGenericRepository<EmployeeDepartmentsChangesHistory> _employeeChangeDepartmentHistory;

        public UnitOfWork()
        {
            _context = new DatabaseContextFactory().CreateDbContext();
        }

        public UnitOfWork(string connectionString)
        {
            _context = new DatabaseContextFactory().CreateDbContext(connectionString);
        }

        public UnitOfWork(EmployeesContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public IGenericRepository<Organization> Organizations => _organizationRepository ?? (_organizationRepository = new GenericRepository<Organization>(_context));

        public IGenericRepository<Department> Departments =>_departmentRepository ?? (_departmentRepository = new GenericRepository<Department>(_context));

        public IGenericRepository<Employee> Employees => _employeeRepository ?? (_employeeRepository = new GenericRepository<Employee>(_context));

        public IGenericRepository<EmployeeDepartmentsChangesHistory> EmployeeChangeDepartmentHistory =>
            _employeeChangeDepartmentHistory ?? (_employeeChangeDepartmentHistory =
                new GenericRepository<EmployeeDepartmentsChangesHistory>(_context));

        public int Save()
        {
            return _context.SaveChanges();
        }

        public virtual async Task<int> SaveAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
