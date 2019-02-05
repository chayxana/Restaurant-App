using System.Diagnostics.CodeAnalysis;
using Menu.API.Data;
using Menu.API.Models;
using Microsoft.Extensions.Logging;

namespace Menu.API.Repositories
{
	[ExcludeFromCodeCoverage]
	public class CategoryRepository : RepositoryBase<Category>
	{
		public CategoryRepository(ApplicationDbContext context, ILogger<Category> logger) : base(context, logger)
		{
		}
	}
}