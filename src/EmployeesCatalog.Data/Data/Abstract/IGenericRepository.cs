using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace EmployeesCatalog.Data.Data.Abstract
{
    public interface IGenericRepository<T> where T : class
    {
        T Add(T entity);
        Task<T> AddAsync(T entity);
        void Delete(T entity);
        void Update(T entity);
        T Find(Expression<Func<T, bool>> findPredicate);
        ICollection<T> FindAll(Expression<Func<T, bool>> findPredicate);
        Task<ICollection<T>> FindAllAsync(Expression<Func<T, bool>> findPredicate);
        Task<T> FindAsync(Expression<Func<T, bool>> findPredicate);
        IQueryable<T> FindBy(Expression<Func<T, bool>> predicate);
        Task<ICollection<T>> FindByAsync(Expression<Func<T, bool>> predicate);
        T Get(int id);
        IQueryable<T> GetAll();
        Task<ICollection<T>> GetAllAsync();
        Task<T> GetAsync(int id);
        void Save();
        Task<int> SaveAsync();
    }
}
