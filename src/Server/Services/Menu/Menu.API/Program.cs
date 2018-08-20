using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Menu.API.Data;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Menu.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args)
                .Build()
                .MigrateDbContext<ApplicationDbContext>((context, services) => 
                {
                    var logger = services.GetRequiredService<ILogger<Program>>();
                    var configuration = services.GetRequiredService<IConfiguration>();
                    var connectionString = configuration.GetConnectionString("MenuDatabaseConnectionString");
                    logger.LogInformation(connectionString);
                    
                })
                .Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();
    }
}
