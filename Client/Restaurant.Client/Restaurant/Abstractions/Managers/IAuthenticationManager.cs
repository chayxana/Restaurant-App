using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Restaurant.DataTransferObjects;
using Restaurant.Model;

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
