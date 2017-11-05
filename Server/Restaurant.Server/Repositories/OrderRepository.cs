using System.Diagnostics.CodeAnalysis;
using Microsoft.Extensions.Logging;
using Restaurant.Server.Api.Models;

namespace Restaurant.Server.Api.Repositories
{
	[ExcludeFromCodeCoverage]
	public class OrderRepository : RepositoryBase<Order>
	{
		public OrderRepository(DatabaseContext context, ILogger<Order> logger) : base(context, logger)
		{
		}
	}
}