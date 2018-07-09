using System.Collections.Generic;
using IdentityServer4;
using IdentityServer4.Models;

namespace Restaurant.Server.Api.IntegrationTests.Config
{
    public static class Clients
    {
        public static IEnumerable<Client> Get()
        {
            return new List<Client>
            {
                new Client
                {
                    ClientId = "client",
                    ClientSecrets = new List<Secret>
                    {
                        new Secret("secret".Sha256())
                    },
                    AllowedScopes = {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        "api"
                    },

                    AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,
                },
            };
        }
    }
}