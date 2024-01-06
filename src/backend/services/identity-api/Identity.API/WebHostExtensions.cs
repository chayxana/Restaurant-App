using System;
using System.Data.SqlClient;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Polly;

namespace Identity.API
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

                var configuration = services.GetService<IConfiguration>();
                var orchestrationType = configuration.GetValue<string>("OrchestrationType");
                var k8s = orchestrationType.ToUpper().Equals("k8s");

                try
                {
                    logger.LogInformation($"Migrating database associated with context {typeof(TContext).Name}");

                    if (k8s)
                    {
                        Migrate(seeder, services, context);
                    }
                    else
                    {
                        var retry = Policy.Handle<SqlException>()
                             .WaitAndRetry(new TimeSpan[]
                             {
                                TimeSpan.FromSeconds(5),
                                TimeSpan.FromSeconds(10),
                                TimeSpan.FromSeconds(15),
                             });

                        retry.Execute(() =>
                        {
                            //if the sql server container is not created on run docker compose this
                            //migration can't fail for network related exception. The retry options for DbContext only 
                            //apply to transient exceptions.
                            Migrate(seeder, services, context);
                        });
                    }
                    logger.LogInformation($"Migrated database associated with context {typeof(TContext).Name}");
                }
                catch (Exception ex)
                {
                    logger.LogError(ex, $"An error occurred while migrating the database used on context {typeof(TContext).Name}");
                    throw ex;
                }
            }

            return webHost;
        }

        private static void Migrate<TContext>(Action<TContext, IServiceProvider> seeder, IServiceProvider services, TContext context) where TContext : DbContext
        {
            context.Database.Migrate();
            seeder(context, services);
        }
    }
}
