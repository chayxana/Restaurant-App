using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;
using Menu.API.Abstraction.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Menu.API.Repositories
{
    [ExcludeFromCodeCoverage]
    public abstract class RepositoryBase<T> : IRepository<T> where T : class
    {
        private readonly DbContext _context;
        private readonly ILogger _logger;

        protected RepositoryBase(DbContext context, ILogger logger)
        {
            _context = context;
            _logger = logger;
        }

        public virtual void Create(T entity)
        {
            _context.Add(entity);
        }

        public virtual void Update(Guid id, T entity)
        {
            var oldEntity = _context.Set<T>().Find(id);
            _context.Entry(oldEntity).CurrentValues.SetValues(entity);
        }

        public virtual void Delete(T entity)
        {
            _context.Remove(entity);
        }

        public virtual T Get(Guid id)
        {
            return _context.Set<T>().Find(id);
        }

        public virtual IQueryable<T> GetAll()
        {
            return _context.Set<T>();
        }

        public virtual async Task<bool> Commit()
        {
            try
            {
                return await _context.SaveChangesAsync() == 1;
            }
            catch (Exception ex)
            {
                _logger.LogDebug(GetHashCode(), ex, "DbContext Validation Errors!");
                return false;
            }
        }

        public bool HasChanges()
        {
            return _context.ChangeTracker.HasChanges();
        }
    }
}