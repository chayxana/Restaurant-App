using System.Threading.Tasks;
using IdentityModel.Client;
using Restaurant.Abstractions.Providers;
using TokenResponse = Restaurant.DataTransferObjects.TokenResponse;

namespace Restaurant.Droid.Providers
{
    public class TokenProvider : ITokenProvider
    {
        public async Task<TokenResponse> RequestResourceOwnerPasswordAsync(string userName, string password)
        {
            var disco = await DiscoveryClient.GetAsync("http://localhost:62798");
            var tokenClient = new TokenClient(disco.TokenEndpoint, "ro.client", "secret");
            var tokenResponse = await tokenClient.RequestResourceOwnerPasswordAsync(userName, password, "api1");

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