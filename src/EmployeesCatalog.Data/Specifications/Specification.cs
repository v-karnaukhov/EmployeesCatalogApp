using System;
using System.Linq.Expressions;

namespace EmployeesCatalog.Data.Specifications
{
    public abstract class Specification<T>
    {
        public abstract Expression<Func<T, bool>> IsSatisfiedBy();
    }
}
