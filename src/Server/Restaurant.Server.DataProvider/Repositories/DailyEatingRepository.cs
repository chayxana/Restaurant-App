using System.Diagnostics.CodeAnalysis;
using Microsoft.Extensions.Logging;
using Restaurant.Server.Models;

namespace Restaurant.Server.DataProvider.Repositories
{
	[ExcludeFromCodeCoverage]
	public class DailyEatingRepository : RepositoryBase<DailyEating>
	{
		public DailyEatingRepository(DatabaseContext context, ILogger<DailyEatingRepository> logger)
			: base(context, logger)
		{
		}
	}
}