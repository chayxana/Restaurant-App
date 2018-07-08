using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Restaurant.Server.Api.Abstraction.Repositories;
using Restaurant.Server.Api.Data;
using Restaurant.Server.Api.Models;

namespace Restaurant.Server.Api.Repositories
{
	[ExcludeFromCodeCoverage]
	public abstract class RepositoryBase<T> : IRepository<T> where T : BaseEntity
	{
		private readonly RestaurantDbContext _context;
		private readonly ILogger _logger;

		protected RepositoryBase(RestaurantDbContext context, ILogger logger)
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
			if (_context.ChangeTracker.HasChanges())
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
			_logger.LogDebug("No changes to commit!");
			return false;
		}
	}
}