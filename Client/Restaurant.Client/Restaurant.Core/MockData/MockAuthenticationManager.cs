using System;
using System.Net;
using System.Threading.Tasks;
using Restaurant.Abstractions.Managers;
using Restaurant.Common.DataTransferObjects;

namespace Restaurant.MockData
{
    public class MockAuthenticationManager : IAuthenticationManager
    {
        public Task<TokenResponse> Login(LoginDto loginDto)
        {
            return Task.FromResult(new TokenResponse {HttpStatusCode = HttpStatusCode.OK, IsError = false});
        }

        public Task<object> Register(RegisterDto registerDto)
        {
            return Task.FromResult(new object());
        }

        public Task<bool> ValidateToken(string accessToken)
        {
            return Task.FromResult(true);
        }

        public Task<bool> LogOut()
        {
            throw new NotImplementedException();
        }
    }
}