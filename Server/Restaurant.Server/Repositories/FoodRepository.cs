using Microsoft.Extensions.Logging;
using Restaurant.Server.Api.Models;

namespace Restaurant.Server.Api.Repositories
{
	public class FoodRepository : RepositoryBase<Food>
    {
		public FoodRepository(DatabaseContext context, ILogger<FoodRepository> logger)
			: base(context, logger)
		{
		}
    }
}
