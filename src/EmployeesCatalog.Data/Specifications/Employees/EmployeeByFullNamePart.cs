using System;
using System.Linq.Expressions;
using EmployeesCatalog.Data.Entities;

namespace EmployeesCatalog.Data.Specifications.Employees
{
    public class EmployeeByFullNamePart : Specification<Employee>
    {
        private readonly string _searchTerm;

        public EmployeeByFullNamePart(string searchTerm)
        {
            _searchTerm = searchTerm;
        }

        public override Expression<Func<Employee, bool>> IsSatisfiedBy()
        {
            if(string.IsNullOrEmpty(_searchTerm))
            {
                return c => true;
            }

            return c => c.FirstName.Contains(_searchTerm)
                        || c.Surname.Contains(_searchTerm)
                        || c.Patronymic.Contains(_searchTerm);
        }
    }
}
