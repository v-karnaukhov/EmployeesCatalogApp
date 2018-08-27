using System;
using System.Linq.Expressions;
using EmployeesCatalog.Data.Entities;

namespace EmployeesCatalog.Data.Specifications.Organizations
{
    public class OrganizationNameStartsWith : Specification<Organization>
    {
        private readonly string _searchTerm;

        public OrganizationNameStartsWith(string searchTerm)
        {
            _searchTerm = searchTerm;
        }

        public override Expression<Func<Organization, bool>> IsSatisfiedBy()
        {
            if (string.IsNullOrEmpty(_searchTerm))
            {
                return org => true;
            }

            return org => org.Name.StartsWith(_searchTerm);
        }
    }
}
