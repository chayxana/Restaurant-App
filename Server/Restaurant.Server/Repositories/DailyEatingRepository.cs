using Microsoft.Extensions.Logging;
using Restaurant.Server.Api.Abstractions.Repositories;
using Restaurant.Server.Api.Models;

namespace Restaurant.Server.Api.Repositories
{
	public class DailyEatingRepository : RepositoryBase<DailyEating>, IRepository<DailyEating>
    {

        public DailyEatingRepository(DatabaseContext context, ILogger<DailyEatingRepository> logger) 
            : base(context, logger)
        {
        }		
    }
}
