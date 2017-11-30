using System.Threading.Tasks;
using IdentityModel.Client;
using Restaurant.Abstractions.Providers;
using Restaurant.Common.Constants;
using TokenResponse = Restaurant.Common.DataTransferObjects.TokenResponse;

namespace Restaurant.Core.Providers
{
	public class IdentityModelTokenProvider : ITokenProvider
	{
		private readonly DiscoveryClient _client;

		public IdentityModelTokenProvider()
		{
			_client = new DiscoveryClient(ApiConstants.ApiClientUrl) { Policy = { RequireHttps = false } };
		}
		public async Task<TokenResponse> RequestResourceOwnerPasswordAsync(string userName, string password)
		{
			var disco = await _client.GetAsync();
			var tokenClient = new TokenClient(disco.TokenEndpoint, ApiConstants.ClientId, ApiConstants.ClientSecret);
			var tokenResponse = await tokenClient.RequestResourceOwnerPasswordAsync(userName, password, $"{ApiConstants.ApiName} offline_access");
			return MapIdentityTokenResponseToTokenResponse(tokenResponse);
		}

		public async Task<TokenResponse> RequestRefreshToken(string refreshToken)
		{
			var disco = await _client.GetAsync();
			var tokenClient = new TokenClient(disco.TokenEndpoint, ApiConstants.ClientId, ApiConstants.ClientSecret);
			var tokenResponse = await tokenClient.RequestRefreshTokenAsync(refreshToken);
			return MapIdentityTokenResponseToTokenResponse(tokenResponse);
		}

		private TokenResponse MapIdentityTokenResponseToTokenResponse(IdentityModel.Client.TokenResponse tokenResponse)
		{
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
	}
}
