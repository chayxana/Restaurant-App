using System;
using System.Diagnostics.CodeAnalysis;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Restaurant.Abstractions.DataTransferObjects;
using Restaurant.Abstractions.Providers;

namespace Restaurant.Core.MockData
{
    [ExcludeFromCodeCoverage]
    public class MockAuthenticationProvider : IAuthenticationProvider
    {
        public Task<TokenResponse> Login(LoginDto loginDto)
        {
            return Task.FromResult(new TokenResponse {HttpStatusCode = HttpStatusCode.OK, IsError = false});
        }

        public Task<HttpResponseMessage> Register(RegisterDto registerDto)
        {
            return Task.FromResult(new HttpResponseMessage());
        }

	    public Task<TokenResponse> RefreshToken(string refreshToken)
	    {
		    return Task.FromResult(new TokenResponse());
	    }

	    public Task<object> LogOut()
	    {
		    return Task.FromResult(new object());

	    }

	    public void SaveRefreshToken(string refreshToken)
	    {

	    }

	    public bool IsAccessTokenExpired()
	    {
		    return false;
	    }

	    public Task<string> GetAccessToken()
	    {
		    return Task.FromResult("xxxx");
	    }

	    private TokenResponse LastAuthenticatedTokenResponse { get; set; }
    }
}