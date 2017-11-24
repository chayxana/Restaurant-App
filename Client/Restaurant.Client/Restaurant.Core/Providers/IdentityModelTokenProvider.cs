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
            var disco = await DiscoveryClient.GetAsync(ApiConstants.AzureClientUrl);
            var tokenClient = new TokenClient(disco.TokenEndpoint, ApiConstants.ClientId, ApiConstants.ClientSecret);
            var tokenResponse = await tokenClient.RequestResourceOwnerPasswordAsync(userName, password, ApiConstants.ApiName);

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

        public async Task<bool> ValidateToken(string token)
        {
            var disco = await DiscoveryClient.GetAsync(ApiConstants.AzureClientUrl);

            var introspectionClient = new IntrospectionClient(disco.IntrospectionEndpoint, ApiConstants.ClientId, ApiConstants.ClientSecret);

            var response = await introspectionClient.SendAsync( new IntrospectionRequest { Token = token });

            return response.IsActive;
        }
    }
}
