using System;
using System.Threading.Tasks;
using Restaurant.Abstractions.Managers;
using Restaurant.Common.DataTransferObjects;

namespace Restaurant.Core.Managers
{
    public class AuthenticationManager : IAuthenticationManager
    {
        public Task<TokenResponse> Login(LoginDto loginDto)
        {
            throw new NotImplementedException();
        }

        public Task<object> Register(RegisterDto registerDto)
        {
            throw new NotImplementedException();
        }

        public Task<bool> ValidateToken(string accessToken)
        {
            throw new NotImplementedException();
        }

        public Task<bool> LogOut()
        {
            throw new NotImplementedException();
        }
    }
}