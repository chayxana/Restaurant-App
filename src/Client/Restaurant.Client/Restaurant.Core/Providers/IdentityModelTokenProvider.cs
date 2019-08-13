using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using IdentityModel.Client;
using Restaurant.Abstractions.Constants;
using Restaurant.Abstractions.Facades;
using Restaurant.Abstractions.Providers;
using TokenResponse = Restaurant.Abstractions.DataTransferObjects.TokenResponse;

namespace Restaurant.Core.Providers
{
    [ExcludeFromCodeCoverage]
	public class IdentityModelTokenProvider : ITokenProvider
	{
		private readonly IDiagnosticsFacade _diagnosticsFacade;
		private readonly DiscoveryClient _client;

		public IdentityModelTokenProvider(IDiagnosticsFacade diagnosticsFacade)
		{
			_diagnosticsFacade = diagnosticsFacade;
			_client = new DiscoveryClient(ApiConstants.ApiClientUrl) { Policy = { RequireHttps = false } };
		}
		
		public async Task<TokenResponse> RequestResourceOwnerPasswordAsync(string userName, string password)
		{
			try
			{
				var disco = await _client.GetAsync();
				var tokenClient = new TokenClient(disco.TokenEndpoint, ApiConstants.ClientId, ApiConstants.ClientSecret);
				var tokenResponse = await tokenClient.RequestResourceOwnerPasswordAsync(userName, password, $"{ApiConstants.ApiName} {ApiConstants.OfflineAccess}");
				return MapIdentityTokenResponseToTokenResponse(tokenResponse);
			}
			catch (Exception e)
			{
				_diagnosticsFacade.TrackError(e);
#if DEBUG
				throw;
#endif
			}

			return null;
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
			return new TokenResponse
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
