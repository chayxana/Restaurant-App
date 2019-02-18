using System.Linq;
using System.Threading.Tasks;
using Identity.API.IdentityServer;
using IdentityServer4.EntityFramework.DbContexts;
using IdentityServer4.EntityFramework.Mappers;
using Microsoft.Extensions.Configuration;

namespace Identity.API.Data
{

    public class ConfigurationDbContextSeed
    {
        public async Task SeedAsync(ConfigurationDbContext context, IConfiguration configuration)
        {
            if (!context.Clients.Any())
            {
                foreach (var client in Config.GetClients())
                {
                    await context.Clients.AddAsync(client.ToEntity());
                }
            }

            if (!context.IdentityResources.Any())
            {
                foreach (var identityResource in Config.GetIdentityResources())
                {
                    await context.IdentityResources.AddAsync(identityResource.ToEntity());
                }
            }

            if (!context.ApiResources.Any())
            {
                foreach (var apiResource in Config.GetApiResources())
                {
                    await context.ApiResources.AddAsync(apiResource.ToEntity());
                }
            }
        }
    }
}