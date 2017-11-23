using System.Threading.Tasks;
using Restaurant.Common.DataTransferObjects;

namespace Restaurant.Abstractions.Managers
{
    public interface IAuthenticationManager
    {
        Task<TokenResponse> Login(LoginDto loginDto);

        Task<object> Register(RegisterDto registerDto);

        Task<bool> ValidateToken(string accessToken);

        Task<bool> LogOut();
    }
}