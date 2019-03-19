using System.Threading.Tasks;
using Restaurant.Abstractions.DataTransferObjects;

namespace Restaurant.Abstractions.Providers
{
    public interface ITokenProvider
    {
        Task<TokenResponse> RequestResourceOwnerPasswordAsync(string userName, string password);

	    Task<TokenResponse> RequestRefreshToken(string refreshToken);
    }
}