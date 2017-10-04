using System.Threading.Tasks;
using Refit;
using Restaurant.Common.DataTransferObjects;

namespace Restaurant.Abstractions.Api
{
	public interface IAccountApi
	{
		[Get("/api/Account/GetUser")]
		Task<UserDto> GetUser([Header("Authorization: bearer")] string accessToken);

		[Post("/api/Account/Register")]
		Task<object> Register([Body]RegisterDto registerDto);
	}
}
