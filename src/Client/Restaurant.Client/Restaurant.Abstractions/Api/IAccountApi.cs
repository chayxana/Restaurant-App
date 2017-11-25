using System.Net.Http;
using System.Threading.Tasks;
using Refit;
using Restaurant.Common.DataTransferObjects;

namespace Restaurant.Abstractions.Api
{
    public interface IAccountApi : IApi
    {
        [Get("/api/Account/GetUser")]
        Task<UserDto> GetUser([Header("Authorization: bearer")] string accessToken);

        [Post("/api/Account/Register")]
        Task<HttpResponseMessage> Register([Body] RegisterDto registerDto);

        [Post("/api/Account/LogOut")]
        Task<object> LogOut();
    }
}