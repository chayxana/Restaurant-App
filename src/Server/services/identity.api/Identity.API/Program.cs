using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Identity.API.Data;
using Identity.API.Model.Entities;
using IdentityServer4.EntityFramework.DbContexts;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Identity.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args)
                .Build()
                .MigrateDbContext<ApplicationDbContext>((context, services) => 
                {
                    var logger = services.GetRequiredService<ILogger<RestaurantDbContextSeed>>();
                    var configuration = services.GetRequiredService<IConfiguration>();
                    var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
                    var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();   
                    new RestaurantDbContextSeed().SeedAsync(logger, configuration, roleManager, userManager).Wait();
                })
                .MigrateDbContext<ConfigurationDbContext>((context, services) =>
                    {
                        var configuration = services.GetRequiredService<IConfiguration>();
                        new ConfigurationDbContextSeed().SeedAsync(context, configuration).Wait();
                    })
                .Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();
    }
}
