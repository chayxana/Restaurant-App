using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using IdentityServer4;
using IdentityServer4.Models;

namespace Identity.API.IdentityServer
{
    [ExcludeFromCodeCoverage]
    public static class Config
    {
        // scopes define the resources in your system
        public static IEnumerable<IdentityResource> GetIdentityResources()
        {
            return new IdentityResource[]
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
                new IdentityResources.Email()
            };
        }

        public static IEnumerable<ApiResource> GetApiResources()
        {
            return new[]
            {
                new ApiResource("menu-api", "Restaurant Menu Api") { UserClaims = { "role" } },
                new ApiResource("order-api", "Restaurant Order Api") { UserClaims = { "role" } },
                new ApiResource("basket-api", "Restaurant Basket Api") { UserClaims = { "role" } }
            };
        }

        // clients want to access resources (aka scopes)
        public static IEnumerable<Client> GetClients(IDictionary<string, string> clientUrls)
        {
            // client credentials client
            return new[]
            {
                new Client
                {
                    ClientId = "mobile-client",
                    AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,
                    ClientSecrets = {new Secret("secret".Sha256())},
                    AllowedScopes =
                    {
                        "menu-api",
                        "basket-api",
                        "order-api"
                    },
                    AllowOfflineAccess = true
                },
                new Client
                {
                    ClientId = "dashboard-spa",
                    AllowedGrantTypes = GrantTypes.Implicit,
                    ClientSecrets = {new Secret("secret".Sha256())},
                    AllowAccessTokensViaBrowser = true,
                    RedirectUris = { $"http://localhost:4200/auth-callback" },
                    PostLogoutRedirectUris = { $"http://localhost:4200" },
                    AllowOfflineAccess = true,
                    AllowedScopes =
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        IdentityServerConstants.StandardScopes.Email,
                        "menu-api",
                        "basket-api",
                        "order-api"
                    },
                },
                new Client
                {
                    ClientId = "menu-api-swagger-ui",
                    ClientName = "Menu API Swagger UI",
                    RequireConsent = false,
                    AllowedGrantTypes = GrantTypes.Implicit,
                    AllowAccessTokensViaBrowser = true,
                    RedirectUris = { $"{clientUrls["MenuApiUrl"]}/swagger/oauth2-redirect.html" },
                    PostLogoutRedirectUris = { $"{clientUrls["MenuApiUrl"]}/swagger/" },
                    AllowedScopes = { "menu-api" }
                },
                new Client
                {
                    ClientId = "basket-api-swagger-ui",
                    ClientName = "Basket API Swagger UI",
                    RequireConsent = false,
                    AllowedGrantTypes = GrantTypes.Implicit,
                    AllowAccessTokensViaBrowser = true,
                    RedirectUris = { $"{clientUrls["BasketApiUrl"]}/swagger/oauth2-redirect.html" },
                    PostLogoutRedirectUris = { $"{clientUrls["BasketApiUrl"]}/swagger/" },
                    AllowedScopes = { "basket-api" }
                },
                new Client
                {
                    ClientId = "order-api-swagger-ui",
                    ClientName = "Order API Swagger UI",
                    RequireConsent = false,
                    AllowedGrantTypes = GrantTypes.Implicit,
                    AllowAccessTokensViaBrowser = true,
                    RedirectUris = { $"{clientUrls["OrderApiUrl"]}/webjars/springfox-swagger-ui/oauth2-redirect.html" },
                    PostLogoutRedirectUris = { $"{clientUrls["OrderApiUrl"]}/swagger/" },
                    AllowedScopes = { "order-api" }
                },
                // OpenID Connect hybrid flow and client credentials client (MVC)
                new Client
                {
                    ClientId = "spa-client",
                    ClientName = "MVC Client",
                    AllowedGrantTypes = GrantTypes.HybridAndClientCredentials,
                    RequireConsent = false,
                    ClientSecrets = {new Secret("secret".Sha256())},
                    RedirectUris = {"http://localhost:5002/signin-oidc"},
                    PostLogoutRedirectUris = {"http://localhost:5002/signout-callback-oidc"},
                    AllowedScopes =
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        "menu-api",
                        "basket-api",
                        "order-api"
                    },
                    AllowOfflineAccess = true
                }
            };
        }
    }
}