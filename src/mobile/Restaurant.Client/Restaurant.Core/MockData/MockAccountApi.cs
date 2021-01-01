using System.Diagnostics.CodeAnalysis;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Restaurant.Abstractions.Api;
using Restaurant.Abstractions.DataTransferObjects;

namespace Restaurant.Core.MockData
{
    [ExcludeFromCodeCoverage]
    public class MockAccountApi : IAccountApi
    {
        public Task<UserDto> GetUserInfo(string accessToken)
        {
            return Task.FromResult(Data.User);
        }

        public Task<HttpResponseMessage> Register(RegisterDto registerDto)
        {
            return Task.FromResult(new HttpResponseMessage(HttpStatusCode.OK));
        }

        public Task<object> LogOut()
        {
            return Task.FromResult(new object());
        }
    }
}