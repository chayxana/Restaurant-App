using System;
using System.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Polly;

// ReSharper disable once CheckNamespace
namespace Microsoft.AspNetCore.Hosting
{
	public static class WebHostExtensions
	{
		public static IWebHost MigrateDbContext<TContext>(this IWebHost webHost, Action<TContext, IServiceProvider> seeder) where TContext : DbContext
		{
			using (var scope = webHost.Services.CreateScope())
			{
				var services = scope.ServiceProvider;

				var logger = services.GetRequiredService<ILogger<TContext>>();

				var context = services.GetService<TContext>();

				try
				{
					logger.LogInformation($"Migrating database associated with context {typeof(TContext).Name}");

					var retry = Policy.Handle<SqlException>()
						.WaitAndRetry(new[]
						{
							TimeSpan.FromSeconds(5),
							TimeSpan.FromSeconds(10),
							TimeSpan.FromSeconds(15),
						});

					retry.Execute(() =>
					{
						context.Database
							.Migrate();

						seeder?.Invoke(context, services);
					});


					logger.LogInformation($"Migrated database associated with context {typeof(TContext).Name}");
				}
				catch (Exception ex)
				{
					logger.LogError(ex, $"An error occurred while migrating the database used on context {typeof(TContext).Name}");
				}
			}

			return webHost;
		}
	}
}