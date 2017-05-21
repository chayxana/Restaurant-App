using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Restaurant.Abstractions.Managers;
using Restaurant.DataTransferObjects;
using Restaurant.Model;

namespace Restaurant.Managers
{
    public class AuthentiticationManager : IAuthenticationManager
    {
        private readonly IRestaurantApi _api;

        public AuthentiticationManager(IRestaurantApi api)
        {
            _api = api;
        }

        public Task<AuthenticationResult> Login(LoginDto loginDto)
        {
            return _api.GetTokenRaw(loginDto);
        }

        public Task<object> Register(RegisterDto registerDto)
        {
            return _api.RegesterRaw(registerDto);
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
