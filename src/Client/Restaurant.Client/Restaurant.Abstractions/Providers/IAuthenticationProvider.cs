using System.Net.Http;
using System.Threading.Tasks;
using Restaurant.Common.DataTransferObjects;

namespace Restaurant.Abstractions.Providers
{
    public interface IAuthenticationProvider
    {
        Task<TokenResponse> Login(LoginDto loginDto);

        Task<HttpResponseMessage> Register(RegisterDto registerDto);

        Task<bool> ValidateToken(string accessToken);

        Task<object> LogOut();
    }
}