using System;
using System.Net.Http;
using System.Threading.Tasks;
using Restaurant.Abstractions.Api;
using Restaurant.Abstractions.Providers;
using Restaurant.Common.DataTransferObjects;

namespace Restaurant.Core.Providers
{
    public class AuthenticationProvider : IAuthenticationProvider
    {
        private readonly ITokenProvider _tokenProvider;
        private readonly IAccountApi _accountApi;
	    private readonly ISettingsProvider _settingsProvider;
	    private TokenResponse _lastAuthenticatedTokenResponse;

		public AuthenticationProvider(
            ITokenProvider tokenProvider,
            IAccountApi accountApi,
			ISettingsProvider settingsProvider)
        {
            _tokenProvider = tokenProvider;
            _accountApi = accountApi;
	        _settingsProvider = settingsProvider;
        }

        public async Task<TokenResponse> Login(LoginDto loginDto)
        {
	        var result = await _tokenProvider.RequestResourceOwnerPasswordAsync(loginDto.Login, loginDto.Password);

	        if (!result.IsError)
	        {
		        UpdateRefreshToken(result.RefreshToken);
		        _lastAuthenticatedTokenResponse = result;
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
			    _lastAuthenticatedTokenResponse = result;
			}

			return result;
	    }

	    public async Task<string> GetAccessToken()
	    {
		    if (_lastAuthenticatedTokenResponse == null)
			    return null;

			if (IsAccessTokenExpired())
			{
				await RefreshToken(_settingsProvider.RefreshToken);
			}

			return _lastAuthenticatedTokenResponse.AccessToken;
	    }

		public Task<object> LogOut()
        {
            return _accountApi.LogOut();
        }

	    private void UpdateRefreshToken(string refreshToken)
	    {
		    _settingsProvider.RefreshToken = refreshToken;
		    _settingsProvider.LastUpdatedRefreshTokenTime = DateTime.Now;
	    }

	    private bool IsAccessTokenExpired()
	    {
		    return (DateTime.Now - _settingsProvider.LastUpdatedRefreshTokenTime).TotalSeconds >
		           _lastAuthenticatedTokenResponse.ExpiresIn;
	    }
	}
}
