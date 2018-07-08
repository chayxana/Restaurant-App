using System.Diagnostics.CodeAnalysis;
using System.Net;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Restaurant.Server.Api.Data;
using Restaurant.Server.Api.Database;
using Microsoft.Extensions.DependencyInjection;
using Restaurant.Server.Api.Models;

namespace Restaurant.Server.Api
{
	[ExcludeFromCodeCoverage]
	public class Program
	{
		public static void Main(string[] args)
		{
			BuildWebHost(args)
				.MigrateDbContext<RestaurantDbContext>((context, services) =>
				{
					var logger = services.GetService<ILogger<RestaurantDbContextSeed>>();
					var configuration = services.GetService<IConfiguration>();
					var roleManager = services.GetService<RoleManager<IdentityRole>>();
					var userManager = services.GetService<UserManager<User>>();

					new RestaurantDbContextSeed()
						.SeedAsync(logger, configuration, roleManager, userManager)
						.Wait();
				})
				.Run();
		}

		private static IWebHost BuildWebHost(string[] args)
		{
            return WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .UseKestrel(options =>
                {
	                options.Listen(IPAddress.Loopback, 5000);  // http:localhost:5000
	                options.Listen(IPAddress.Any, 80);         // http:*:80
					options.Listen(IPAddress.Loopback, 443, listenOptions =>
					{
						listenOptions.UseHttps();
					});
				}).Build();
		}
	}
}