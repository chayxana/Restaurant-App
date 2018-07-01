using System.Diagnostics.CodeAnalysis;
using Microsoft.Extensions.Logging;
using Restaurant.Server.Api.Data;
using Restaurant.Server.Api.Models;

namespace Restaurant.Server.Api.Repositories
{
	[ExcludeFromCodeCoverage]
	public class OrderRepository : RepositoryBase<Order>
	{
		public OrderRepository(RestaurantDbContext context, ILogger<Order> logger) : base(context, logger)
		{
		}
	}
}