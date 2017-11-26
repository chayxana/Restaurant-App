using System;
using System.Threading.Tasks;
using IdentityModel.Client;
using Restaurant.Abstractions.Providers;
using Restaurant.Common.Constants;
using TokenResponse = Restaurant.Common.DataTransferObjects.TokenResponse;

namespace Restaurant.Core.Providers
{
	public class IdentityModelTokenProvider : ITokenProvider
	{
		public async Task<TokenResponse> RequestResourceOwnerPasswordAsync(string userName, string password)
		{
			try
			{
				var disco = await DiscoveryClient.GetAsync(ApiConstants.ApiClientUrl);
				var tokenClient = new TokenClient(disco.TokenEndpoint, ApiConstants.ClientId, ApiConstants.ClientSecret);
				var tokenResponse = await tokenClient.RequestResourceOwnerPasswordAsync(userName, password, $"{ApiConstants.ApiName} offline_access");

				return new TokenResponse()
				{
					AccessToken = tokenResponse.AccessToken,
					ExpiresIn = tokenResponse.ExpiresIn,
					RefreshToken = tokenResponse.RefreshToken,
					TokenType = tokenResponse.TokenType,
					IsError = tokenResponse.IsError,
					Error = tokenResponse.Error,
					HttpStatusCode = tokenResponse.HttpStatusCode
				};
			}
#pragma warning disable CS0168 // Variable is declared but never used
			catch (Exception e)
#pragma warning restore CS0168 // Variable is declared but never used
			{
				return null;
			}
		}

		public async Task<bool> ValidateToken(string token)
		{
			try
			{
				var disco = await DiscoveryClient.GetAsync(ApiConstants.ApiClientUrl);

				var introspectionClient = new IntrospectionClient(disco.IntrospectionEndpoint, ApiConstants.ClientId, ApiConstants.ClientSecret);

				var response = await introspectionClient.SendAsync(new IntrospectionRequest
				{
					ClientId = ApiConstants.ClientId,
					ClientSecret = ApiConstants.ClientSecret,
					Token = token
				});

				return response.IsActive;
			}
#pragma warning disable CS0168 // Variable is declared but never used
			catch (Exception e)
#pragma warning restore CS0168 // Variable is declared but never used
			{
				return false;
			}
		}
	}
}
