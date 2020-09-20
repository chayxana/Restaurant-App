using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;
using Identity.API.IdentityServer;
using Identity.API.Utils;
using IdentityServer4.EntityFramework.DbContexts;
using IdentityServer4.EntityFramework.Mappers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using static Identity.API.Utils.Retry;

namespace Identity.API.Data
{

    [ExcludeFromCodeCoverage]
    public class ConfigurationDbContextSeed
    {
        public async Task SeedAsync(
            ILogger<ConfigurationDbContextSeed> logger,
            ConfigurationDbContext context,
            IConfiguration configuration)
        {

            var policy = CreatePolicy(logger, nameof(RestaurantDbContextSeed));
            await policy.ExecuteAsync(async () =>
            {
                var clientUrls = new Dictionary<string, string>();
                clientUrls.Add("MenuApiUrl", configuration["MENU_API_URL"]);
                clientUrls.Add("BasketApiUrl", configuration["BASKET_API_URL"]);
                clientUrls.Add("OrderApiUrl", configuration["ORDER_API_URL"]);
                clientUrls.Add("DashboardAppUrl", configuration["DASHBOARD_APP_URL"]);

                logger.LogDebug("Logging client urls!");
                foreach (var clientUrl in clientUrls)
                {
                    logger.LogDebug($"{clientUrl.Key}:{clientUrl.Value}");
                }

                foreach (var client in Config.GetClients(clientUrls))
                {
                    if (!context.Clients.Any(c => c.ClientId == client.ClientId))
                    {
                        logger.LogInformation($"Client: {client.ClientId} not found, and creating..");
                        await context.Clients.AddAsync(client.ToEntity());
                    }
                }

                foreach (var identityResource in Config.GetIdentityResources())
                {
                    if (!context.IdentityResources.Any(r => r.Name == identityResource.Name))
                    {
                        logger.LogInformation($"Resource: {identityResource.Name} not found, and creating it..");
                        await context.IdentityResources.AddAsync(identityResource.ToEntity());
                    }
                }


                foreach (var apiScope in Config.GetApiScopes())
                {
                    if (!context.ApiScopes.Any(ar => ar.Name == apiScope.Name))
                    {
                        logger.LogInformation($"Resource Api: {apiScope.Name} not found, and creating it..");
                        await context.ApiScopes.AddAsync(apiScope.ToEntity());
                    }
                }

                var _ = context.ChangeTracker.HasChanges() ? await context.SaveChangesAsync() : 0;
            });
        }
    }
}