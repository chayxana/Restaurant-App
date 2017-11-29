using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Restaurant.Abstractions.Api;
using Restaurant.Common.DataTransferObjects;

namespace Restaurant.Core.MockData
{
    public class MockAccountApi : IAccountApi
    {
        public Task<UserDto> GetUser(string accessToken)
        {
            return Task.FromResult(Data.User);
        }
        
        public Task<HttpResponseMessage> Register(RegisterDto registerDto)
        {
            throw new NotImplementedException();
        }

        public Task<object> LogOut()
        {
            throw new NotImplementedException();
        }
    }
}
