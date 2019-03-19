using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Moq;
using NUnit.Framework;
using Restaurant.Abstractions.Api;
using Restaurant.Abstractions.DataTransferObjects;
using Restaurant.Abstractions.Facades;
using Restaurant.Abstractions.Providers;
using Restaurant.Core.Providers;

namespace Restaurant.Core.UnitTests.Providers
{
    public class AuthenticationProviderTests : BaseAutoMockedTest<AuthenticationProvider>
    {
        [Test, AutoDomainData]
        public async Task Given_valid_login_dto_Login_should_return_response_and_update_settings(LoginDto login, TokenResponse response)
        {
            // given
            var authenticationProviderprovider = ClassUnderTest;
            response.IsError = false;
            GetMock<ITokenProvider>().Setup(x => x.RequestResourceOwnerPasswordAsync(login.Login, login.Password))
                .Returns(Task.FromResult(response));
            GetMock<IDateTimeFacade>().SetupGet(x => x.Now).Returns(DateTime.Today);

            // when
            var result = await authenticationProviderprovider.Login(login);

            // then
            Assert.That(result, Is.EqualTo(response));
            Assert.That(authenticationProviderprovider.LastAuthenticatedTokenResponse, Is.EqualTo(result));
            GetMock<ISettingsProvider>().VerifySet(x => x.RefreshToken = result.RefreshToken, Times.Once);
            GetMock<ISettingsProvider>().VerifySet(x => x.LastUpdatedRefreshTokenTime = DateTime.Today, Times.Once);
        }

        [Test, AutoDomainData]
        public async Task Given_invalid_login_dto_login_should_return_IsError_true_and_should_not_update_settings(LoginDto login, TokenResponse response)
        {
            // given
            var authenticationProviderprovider = ClassUnderTest;
            response.IsError = true;
            GetMock<ITokenProvider>().Setup(x => x.RequestResourceOwnerPasswordAsync(login.Login, login.Password))
                .Returns(Task.FromResult(response));
            GetMock<IDateTimeFacade>().SetupGet(x => x.Now).Returns(DateTime.Today);

            // when
            var result = await authenticationProviderprovider.Login(login);

            // then
            Assert.That(result, Is.EqualTo(response));
            Assert.That(authenticationProviderprovider.LastAuthenticatedTokenResponse, Is.Not.EqualTo(result));
            GetMock<ISettingsProvider>().VerifySet(x => x.RefreshToken = result.RefreshToken, Times.Never);
            GetMock<ISettingsProvider>().VerifySet(x => x.LastUpdatedRefreshTokenTime = DateTime.Today, Times.Never);
        }

        [Test, AutoDomainData]
        public async Task Given_valid_register_dto_Register_should_return_Ok_response(RegisterDto registerDto)
        {
            // Given 
            var response = new HttpResponseMessage(HttpStatusCode.OK);
            GetMock<IAccountApi>().Setup(x => x.Register(registerDto)).Returns(Task.FromResult(response));

            // when
            var result = await ClassUnderTest.Register(registerDto);

            // then
            Assert.That(result.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        }

        [Test, AutoDomainData]
        public async Task Given_valid_refresh_token_Should_return_new_token_response_and_update_settings(string refreshToken, TokenResponse tokenResponse)
        {
            // Given
            tokenResponse.IsError = false;
            var authenticationProviderprovider = ClassUnderTest;
            GetMock<ITokenProvider>().Setup(x => x.RequestRefreshToken(refreshToken)).Returns(Task.FromResult(tokenResponse));
            GetMock<IDateTimeFacade>().SetupGet(x => x.Now).Returns(DateTime.Today);

            // when
            var result = await authenticationProviderprovider.RefreshToken(refreshToken);

            // then
            Assert.That(result, Is.EqualTo(tokenResponse));
            Assert.That(authenticationProviderprovider.LastAuthenticatedTokenResponse, Is.EqualTo(result));
            GetMock<ISettingsProvider>().VerifySet(x => x.RefreshToken = result.RefreshToken, Times.Once);
            GetMock<ISettingsProvider>().VerifySet(x => x.LastUpdatedRefreshTokenTime = DateTime.Today, Times.Once);
        }

        [Test, AutoDomainData]
        public async Task Given_invalid_refresh_token_Should_return_IsError_response_and_should_not_update_settings(string refreshToken, TokenResponse tokenResponse)
        {
            // Given
            tokenResponse.IsError = true;
            var authenticationProviderprovider = ClassUnderTest;
            GetMock<ITokenProvider>().Setup(x => x.RequestRefreshToken(refreshToken)).Returns(Task.FromResult(tokenResponse));

            // when
            var result = await authenticationProviderprovider.RefreshToken(refreshToken);

            // then
            Assert.That(result, Is.EqualTo(tokenResponse));
            Assert.That(authenticationProviderprovider.LastAuthenticatedTokenResponse, Is.Not.EqualTo(result));
            GetMock<ISettingsProvider>().VerifySet(x => x.RefreshToken = result.RefreshToken, Times.Never);
            GetMock<ISettingsProvider>().VerifySet(x => x.LastUpdatedRefreshTokenTime = DateTime.Today, Times.Never);
        }

        [Test]
        public async Task LogOut_should_log_out()
        {
            // Given
            GetMock<IAccountApi>().Setup(x => x.LogOut())
                .Returns(Task.FromResult<object>(new HttpResponseMessage(HttpStatusCode.OK)));

            // when
            var result = await ClassUnderTest.LogOut();

            // then
            Assert.That(result, Is.Not.Null);
        }

        [Test]
        public async Task Given_LastAuthenticatedTokenResponse_null_GetAccessToken_should_return_null()
        {
            // Given
            var authenticationProviderprovider = ClassUnderTest;
            authenticationProviderprovider.LastAuthenticatedTokenResponse = null;

            // when
            var result = await authenticationProviderprovider.GetAccessToken();

            // then
            Assert.That(result, Is.Null);
        }

        [Test, AutoDomainData]
        public async Task Given_expired_access_token_GetAccessToken_should_refresh_token(TokenResponse expiredTokenResponse, TokenResponse refreshedTokenResponse, string oldRefreshToken)
        {
            // given
            var authenticationProviderprovider = ClassUnderTest;
            expiredTokenResponse.ExpiresIn = 3600;
            refreshedTokenResponse.IsError = false;
            authenticationProviderprovider.LastAuthenticatedTokenResponse = expiredTokenResponse;
            GetMock<IDateTimeFacade>().SetupGet(x => x.Now).Returns(DateTime.Today.AddHours(2));
            GetMock<ISettingsProvider>().SetupGet(x => x.LastUpdatedRefreshTokenTime).Returns(DateTime.Today);
            GetMock<ISettingsProvider>().SetupGet(x => x.RefreshToken).Returns(oldRefreshToken);
            GetMock<ITokenProvider>().Setup(x => x.RequestRefreshToken(oldRefreshToken))
                .Returns(Task.FromResult(refreshedTokenResponse));

            // when
            var result = await authenticationProviderprovider.GetAccessToken();

            // then
            
            Assert.That(result, Is.EqualTo(refreshedTokenResponse.AccessToken));
        }
    }
}
