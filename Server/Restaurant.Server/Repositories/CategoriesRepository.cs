using Microsoft.Extensions.Logging;
using Restaurant.Server.Api.Abstractions.Repositories;
using Restaurant.Server.Api.Models;

namespace Restaurant.Server.Api.Repositories
{
	public class CategoryRepository : RepositoryBase<Category>
	{
		public CategoryRepository(DatabaseContext context, ILogger<Category> logger) : base(context, logger)
		{
		}
	}
}
