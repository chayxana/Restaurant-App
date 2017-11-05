using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Restaurant.Server.Api.Abstractions.Providers;
using Restaurant.Server.Api.Models;

namespace Restaurant.Server.Api.Providers
{
	public class UserBootstrapper : IUserBootstrapper
	{
		private readonly IConfiguration _configuration;
		private readonly RoleManager<IdentityRole> _roleManager;
		private readonly UserManager<User> _userManager;

		public UserBootstrapper(
			RoleManager<IdentityRole> roleManager,
			UserManager<User> userManager,
			IConfiguration configuration)
		{
			_roleManager = roleManager;
			_userManager = userManager;
			_configuration = configuration;
		}

		public async Task CreateDefaultUsersAndRoles()
		{
			await CreateRoles();
			await CreateAdmin();
		}

		private async Task CreateRoles()
		{
			var roleNames = _configuration.GetSection("UserSettings:DefaultRoles").Get<List<string>>();
			foreach (var roleName in roleNames)
			{
				var roleExist = await _roleManager.RoleExistsAsync(roleName);
				if (!roleExist)
					await _roleManager.CreateAsync(new IdentityRole(roleName));
			}
		}

		private async Task CreateAdmin()
		{
			var user = await _userManager.FindByEmailAsync(_configuration["UserSettings:AdminEmail"]);

			if (user == null)
			{
				var admin = new User
				{
					UserName = _configuration["UserSettings:AdminUserName"],
					Email = _configuration["UserSettings:AdminEmail"]
				};

				var password = _configuration["UserSettings:AdminPassword"];
				var createPowerUser = await _userManager.CreateAsync(admin, password);

				if (createPowerUser.Succeeded)
					await _userManager.AddToRoleAsync(admin, "Admin");
			}
		}
	}
}