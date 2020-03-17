using System.Net.Http;
using System.Threading.Tasks;
using Restaurant.Abstractions.Api;
using Restaurant.Abstractions.DataTransferObjects;
using Restaurant.Abstractions.Facades;
using Restaurant.Abstractions.Providers;

namespace Restaurant.Core.Providers
{
    public class AuthenticationProvider : IAuthenticationProvider
    {
        private readonly ITokenProvider _tokenProvider;
        private readonly IAccountApi _accountApi;
	    private readonly ISettingsProvider _settingsProvider;
        private readonly IDateTimeFacade _dateTimeFacade;
        internal TokenResponse LastAuthenticatedTokenResponse;

		public AuthenticationProvider(
            ITokenProvider tokenProvider,
            IAccountApi accountApi,
			ISettingsProvider settingsProvider,
            IDateTimeFacade dateTimeFacade)
        {
            _tokenProvider = tokenProvider;
            _accountApi = accountApi;
	        _settingsProvider = settingsProvider;
            _dateTimeFacade = dateTimeFacade;
        }

        public async Task<TokenResponse> Login(LoginDto loginDto)
        {
	        var result = await _tokenProvider.RequestResourceOwnerPasswordAsync(loginDto.Login, loginDto.Password);

	        if (!result.IsError)
	        {
		        UpdateRefreshToken(result.RefreshToken);
		        LastAuthenticatedTokenResponse = result;
	        }

	        return result;
        }

        public Task<HttpResponseMessage> Register(RegisterDto registerDto)
        {
            return _accountApi.Register(registerDto);
        }

	    public async Task<TokenResponse> RefreshToken(string refreshToken)
	    {
		    var result = await _tokenProvider.RequestRefreshToken(refreshToken);

		    if (!result.IsError)
		    {
			    UpdateRefreshToken(result.RefreshToken);
			    LastAuthenticatedTokenResponse = result;
			}

			return result;
	    }

	    public async Task<string> GetAccessToken()
	    {
		    if (LastAuthenticatedTokenResponse == null)
			    return null;

			if (IsAccessTokenExpired())
			{
				await RefreshToken(_settingsProvider.RefreshToken);
			}

			return LastAuthenticatedTokenResponse.AccessToken;
	    }

		public Task<object> LogOut()
        {
            return _accountApi.LogOut();
        }

	    private void UpdateRefreshToken(string refreshToken)
	    {
		    _settingsProvider.RefreshToken = refreshToken;
		    _settingsProvider.LastUpdatedRefreshTokenTime = _dateTimeFacade.Now;
	    }

	    private bool IsAccessTokenExpired()
	    {
		    return (_dateTimeFacade.Now - _settingsProvider.LastUpdatedRefreshTokenTime).TotalSeconds >
		           LastAuthenticatedTokenResponse?.ExpiresIn;
	    }
	}
}
