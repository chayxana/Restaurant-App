using Identity.API.Data;
using Identity.API.Model.Entities;
using IdentityServer4.EntityFramework.DbContexts;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Events;
using Serilog.Sinks.SystemConsole.Themes;

namespace Identity.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
                .MinimumLevel.Override("System", LogEventLevel.Information)
                .MinimumLevel.Override("Microsoft.AspNetCore.Authentication", LogEventLevel.Debug)
                .Enrich.FromLogContext()
                .WriteTo.Console(outputTemplate: "[{Timestamp:HH:mm:ss} {Level}] {SourceContext}{NewLine}{Message:lj}{NewLine}{Exception}{NewLine}", theme: AnsiConsoleTheme.Literate)
                .CreateLogger();
            
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
                .MigrateDbContext<PersistedGrantDbContext>((c,s) => {})
                .MigrateDbContext<ConfigurationDbContext>((context, services) =>
                    {
                        var configuration = services.GetRequiredService<IConfiguration>();
                        var logger = services.GetRequiredService<ILogger<ConfigurationDbContextSeed>>();
                        new ConfigurationDbContextSeed().SeedAsync(logger, context, configuration).Wait();
                    })
                .Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();
    }
}
