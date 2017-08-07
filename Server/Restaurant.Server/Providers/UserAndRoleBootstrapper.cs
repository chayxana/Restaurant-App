using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper.Configuration;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Restaurant.Server.Api.Abstractions.Providers;
using Restaurant.Server.Api.Models;

namespace Restaurant.Server.Api.Providers
{
    [UsedImplicitly]
    public class UserAndRoleBootstrapper : IUserAndRoleBootstrapper
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<User> _userManager;
        private readonly IConfigurationRoot _configuration;

        public UserAndRoleBootstrapper(
            RoleManager<IdentityRole> roleManager,
            UserManager<User> userManager,
            IConfigurationRoot configuration)
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
            var roleNames = _configuration.GetSection("AppSettings:DefaultRoles").Get<List<string>>();
            foreach (var roleName in roleNames)
            {
                var roleExist = await _roleManager.RoleExistsAsync(roleName);
                if (!roleExist)
                {
                    await _roleManager.CreateAsync(new IdentityRole(roleName));
                }
            }
        }

        private async Task CreateAdmin()
        {
            var user = await _userManager.FindByEmailAsync(_configuration["AppSettings:AdminEmail"]);

            if (user == null)
            {
                var admin = new User
                {
                    UserName = _configuration["UserSettings:AdminUserName"],
                    Email = _configuration["UserSettings:AdminEmail"],
                };

                var password = _configuration["UserSettings:AdminPassword"];
                var createPowerUser = await _userManager.CreateAsync(admin, password);

                if (createPowerUser.Succeeded)
                {
                    await _userManager.AddToRoleAsync(admin, "Admin");
                }
            }
        }
    }
}
