using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using IdentityServer4;
using IdentityServer4.Models;
using Restaurant.Common.Constants;

namespace Restaurant.Server.Api.IdentityServer
{
	[ExcludeFromCodeCoverage]
	public static class Config
	{
		// scopes define the resources in your system
		public static IEnumerable<IdentityResource> GetIdentityResources()
		{
			return new List<IdentityResource>
			{
				new IdentityResources.OpenId(),
				new IdentityResources.Profile()
			};
		}

		public static IEnumerable<ApiResource> GetApiResources()
		{
			return new List<ApiResource>
			{
				new ApiResource(ApiConstants.ApiName, "Restaurant Manager Api")
				{
					UserClaims = {"role"}
				}
			};
		}

		// clients want to access resources (aka scopes)
		public static IEnumerable<Client> GetClients()
		{
			// client credentials client
			return new List<Client>
			{
				// resource owner password grant client
				new Client
				{
					ClientId = ApiConstants.ClientId,
					AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,

					ClientSecrets =
					{
						new Secret("secret".Sha256())
					},

					AllowedScopes =
					{
						ApiConstants.ApiName
					},

					AllowOfflineAccess = true
				},

			// OpenID Connect hybrid flow and client credentials client (MVC)
			new Client
				{
					ClientId = "mvc_client",
					ClientName = "MVC Client",
					AllowedGrantTypes = GrantTypes.HybridAndClientCredentials,

					RequireConsent = true,

					ClientSecrets = {new Secret("secret".Sha256())},

					RedirectUris = {"http://localhost:5002/signin-oidc"},
					PostLogoutRedirectUris = {"http://localhost:5002/signout-callback-oidc"},

					AllowedScopes =
					{
						IdentityServerConstants.StandardScopes.OpenId,
						IdentityServerConstants.StandardScopes.Profile,
						ApiConstants.ApiName
					},
					AllowOfflineAccess = true
				}
			};
		}
	}
}