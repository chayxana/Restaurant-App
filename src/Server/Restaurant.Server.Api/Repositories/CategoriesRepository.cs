using System.Diagnostics.CodeAnalysis;
using Microsoft.Extensions.Logging;
using Restaurant.Server.Api.Data;
using Restaurant.Server.Api.Models;

namespace Restaurant.Server.Api.Repositories
{
	[ExcludeFromCodeCoverage]
	public class CategoryRepository : RepositoryBase<Category>
	{
		public CategoryRepository(RestaurantDbContext context, ILogger<Category> logger) : base(context, logger)
		{
		}
	}
}