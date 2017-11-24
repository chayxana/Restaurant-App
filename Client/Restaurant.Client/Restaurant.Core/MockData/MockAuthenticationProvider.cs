using System;
using System.Diagnostics.CodeAnalysis;
using System.Net;
using System.Threading.Tasks;
using Restaurant.Abstractions.Providers;
using Restaurant.Common.DataTransferObjects;

namespace Restaurant.Core.MockData
{
    [ExcludeFromCodeCoverage]
    public class MockAuthenticationProvider : IAuthenticationProvider
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

        public Task<object> LogOut()
        {
            throw new NotImplementedException();
        }
    }
}