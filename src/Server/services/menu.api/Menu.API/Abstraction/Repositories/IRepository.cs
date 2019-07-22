using System;
using System.Linq;
using System.Threading.Tasks;

namespace Menu.API.Abstraction.Repositories
{
	public interface IRepository<T>
	{
		void Create(T entity);

		void Update(Guid id, T entity);

		void Delete(T entity);

		T Get(Guid id);

		IQueryable<T> GetAll();

		Task<bool> Commit();

		bool HasChanges();
	}
}