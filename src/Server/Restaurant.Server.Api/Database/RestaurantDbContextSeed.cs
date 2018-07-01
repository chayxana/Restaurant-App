using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Polly;
using Restaurant.Server.Api.Models;

namespace Restaurant.Server.Api.Database
{
	[ExcludeFromCodeCoverage]
	public class RestaurantDbContextSeed
	{
		public async Task SeedAsync(
			ILogger<RestaurantDbContextSeed> logger,
			IConfiguration configuration,
			RoleManager<IdentityRole> roleManager, 
			UserManager<User> userManager)
		{
			var policy = CreatePolicy(logger, nameof(RestaurantDbContextSeed));

			await policy.ExecuteAsync(async () =>
			{
				var roleNames = configuration.GetSection("UserSettings:DefaultRoles").Get<List<string>>();
				foreach (var roleName in roleNames)
				{
					var roleExist = await roleManager.RoleExistsAsync(roleName);
					if (!roleExist)
						await roleManager.CreateAsync(new IdentityRole(roleName));
				}

				var user = await userManager.FindByEmailAsync(configuration["UserSettings:AdminEmail"]);

				if (user == null)
				{
					var admin = new User
					{
						UserName = configuration["UserSettings:AdminEmail"],
						Email = configuration["UserSettings:AdminEmail"]
					};

					var password = configuration["UserSettings:AdminPassword"];
					var createPowerUser = await userManager.CreateAsync(admin, password);

					if (createPowerUser.Succeeded)
						await userManager.AddToRoleAsync(admin, "Admin");
				}
			});
		}

		private Policy CreatePolicy(ILogger<RestaurantDbContextSeed> logger, string prefix, int retries = 3)
		{
			return Policy.Handle<SqlException>().
				WaitAndRetryAsync(
					retryCount: retries,
					sleepDurationProvider: retry => TimeSpan.FromSeconds(5),
					onRetry: (exception, timeSpan, retry, ctx) =>
					{
						logger.LogTrace($"[{prefix}] Exception {exception.GetType().Name} with message ${exception.Message} detected on attempt {retry} of {retries}");
					}
				);
		}
	}
}