using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using EmployeesCatalog.Data.Data.Abstract;
using Microsoft.EntityFrameworkCore;

namespace EmployeesCatalog.Data.Data.Concrete
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly DbContext _context;

        public GenericRepository(DbContext context)
        {
            _context = context;
        }

        public virtual T Add(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            _context.Set<T>().Add(entity);

            return entity;
        }

        public virtual async Task<T> AddAsync(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            await _context.Set<T>().AddAsync(entity);

            return entity;
        }

        public void Delete(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            _context.Set<T>().Remove(entity);
        }

        public void Update(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            _context.Set<T>().Attach(entity);
            var entry = _context.Entry(entity);
            entry.State = EntityState.Modified;
        }

        public virtual T Find(Expression<Func<T, bool>> findPredicate)
        {
            return _context.Set<T>().SingleOrDefault(findPredicate);
        }

        public ICollection<T> FindAll(Expression<Func<T, bool>> findPredicate)
        {
            return _context.Set<T>().Where(findPredicate).ToList();
        }

        public async Task<ICollection<T>> FindAllAsync(Expression<Func<T, bool>> findPredicate)
        {
            return await _context.Set<T>().Where(findPredicate).ToListAsync();
        }

        public async Task<T> FindAsync(Expression<Func<T, bool>> findPredicate, params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> query = _context.Set<T>();

            foreach (Expression<Func<T, object>> include in includes)
            {
                query = query.Include(include);
            }

            return  await query.SingleOrDefaultAsync(findPredicate);
        }

        public virtual IQueryable<T> FindBy(Expression<Func<T, bool>> predicate)
        {
            var query = _context.Set<T>().Where(predicate);

            return query;
        }

        public virtual async Task<ICollection<T>> FindByAsync(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> query = _context.Set<T>();

            foreach (Expression<Func<T, object>> include in includes)
            {
                query = query.Include(include);
            }

            query =  query.Where(predicate);

            return await query.ToListAsync();
        }

        public T Get(int id)
        {
            return _context.Set<T>().Find(id);
        }

        public IQueryable<T> GetAll()
        {
            return _context.Set<T>();
        }

        public virtual async Task<ICollection<T>> GetAllAsync(params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> query = _context.Set<T>();

            foreach (Expression<Func<T, object>> include in includes)
            {
                query = query.Include(include);
            }

            return await query.ToListAsync();
        }

        public virtual async Task<T> GetAsync(int id)
        {
            return await _context.Set<T>().FindAsync(id);
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public virtual async Task<int> SaveAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }
}
