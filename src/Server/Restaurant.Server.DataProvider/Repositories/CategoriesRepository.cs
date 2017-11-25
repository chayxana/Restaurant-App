using System.Diagnostics.CodeAnalysis;
using Microsoft.Extensions.Logging;
using Restaurant.Server.Models;

namespace Restaurant.Server.DataProvider.Repositories
{
	[ExcludeFromCodeCoverage]
	public class CategoryRepository : RepositoryBase<Category>
	{
		public CategoryRepository(DatabaseContext context, ILogger<Category> logger) : base(context, logger)
		{
		}
	}
}