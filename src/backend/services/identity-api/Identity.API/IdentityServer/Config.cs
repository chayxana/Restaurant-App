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

        public static IEnumerable<ApiScope> GetApiScopes()
        {
            return new[]
            {
                new ApiScope("catalog-api", "Restaurant Catalog Api") { UserClaims = { "role" } },
                new ApiScope("order-api", "Restaurant Order Api") { UserClaims = { "role" } },
                new ApiScope("cart-api", "Restaurant Basket Api") { UserClaims = { "role" } },
                new ApiScope("checkout-api", "Restaurant Checkout Api") { UserClaims = { "role" } },
                new ApiScope("payment-api", "Restaurant Payment Api") { UserClaims = { "role" } }
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
                        "cart-api",
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
                    RedirectUris = { $"{clientUrls["DashboardAppUrl"]}/auth-callback", $"{clientUrls["DashboardAppUrl"]}/assets/silent-renew.html" },
                    PostLogoutRedirectUris = { $"{clientUrls["DashboardAppUrl"]}" },
                    AllowOfflineAccess = true,
                    AllowedScopes =
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        IdentityServerConstants.StandardScopes.Email,
                        "menu-api",
                        "cart-api",
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
                    ClientId = "cart-api-swagger-ui",
                    ClientName = "Basket API Swagger UI",
                    RequireConsent = false,
                    AllowedGrantTypes = GrantTypes.Implicit,
                    AllowAccessTokensViaBrowser = true,
                    RedirectUris = { $"{clientUrls["BasketApiUrl"]}/swagger/oauth2-redirect.html" ,"http://localhost:3200/oauth2-redirect.html" },
                    PostLogoutRedirectUris = { $"{clientUrls["BasketApiUrl"]}/swagger/" },
                    AllowedScopes = { "cart-api" }
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
                // OpenID Connect for next.js web-app 
                new Client
                {
                    ClientId = "nextjs-web-app",
                    ClientName = "Next.js Web-App",
                    AllowedGrantTypes = GrantTypes.CodeAndClientCredentials,
                    RequireConsent = false,
                    ClientSecrets = {new Secret("secret".Sha256())},
                    RedirectUris = {
                        "http://localhost:3001/api/auth/callback/web-app",
                        "http://localhost:8080/api/auth/callback/web-app",
                        "http://localhost:80/api/auth/callback/web-app"
                    },
                    PostLogoutRedirectUris = {"http://localhost:5002/signout-callback-oidc"},
                    AllowedScopes =
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        "catalog-api",
                        "cart-api",
                        "order-api",
                        "payment-api",
                        "checkout-api"
                    },
                    AllowOfflineAccess = true
                }
            };
        }
    }
}