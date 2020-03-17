using System.Net.Http;
using System.Threading.Tasks;
using Restaurant.Abstractions.DataTransferObjects;

namespace Restaurant.Abstractions.Providers
{
    public interface IAuthenticationProvider
	{
		Task<TokenResponse> Login(LoginDto loginDto);

		Task<HttpResponseMessage> Register(RegisterDto registerDto);

		Task<TokenResponse> RefreshToken(string refreshToken);

		Task<object> LogOut();

		Task<string> GetAccessToken();
	}
}