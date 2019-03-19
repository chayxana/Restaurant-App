using System.Net.Http;
using System.Threading.Tasks;
using Refit;
using Restaurant.Abstractions.DataTransferObjects;

namespace Restaurant.Abstractions.Api
{
    public interface IAccountApi : IApi
    {
        [Get("/api/Account/GetUserInfo")]
        Task<UserDto> GetUserInfo([Header("Authorization")] string accessToken);

        [Post("/api/Account/Register")]
        Task<HttpResponseMessage> Register([Body] RegisterDto registerDto);

        [Post("/api/Account/LogOut")]
        Task<object> LogOut();
    }
}