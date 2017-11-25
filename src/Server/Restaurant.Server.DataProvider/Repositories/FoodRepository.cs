using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Restaurant.Server.Models;

namespace Restaurant.Server.DataProvider.Repositories
{
	[ExcludeFromCodeCoverage]
	public class FoodRepository : RepositoryBase<Food>
	{
		public FoodRepository(DatabaseContext context, ILogger<FoodRepository> logger)
			: base(context, logger)
		{
		}

		public override IQueryable<Food> GetAll()
		{
			return base.GetAll().Include(x => x.Category);
		}
	}
}