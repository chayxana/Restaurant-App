using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using BaseUnitTests;
using Identity.API.Abstraction.Providers;
using Identity.API.Abstraction.ViewModelBuilders;
using Identity.API.Controllers.Account;
using Identity.API.ViewModelBuilders;
using IdentityServer4.Models;
using IdentityServer4.Services;
using IdentityServer4.Stores;
using Microsoft.AspNetCore.Mvc;
using Xunit;

namespace Identity.API.UnitTests.Controllers
{
    public class AccountControllerTest : BaseAutoMockedTest<AccountController>
    {
        [Fact]
        public async Task When_ReturnUrl_Login_should_return_view_with_viewModel()
        {
            // act
            var returnUrl = "test_url";
            var loginViewModel = new LoginViewModel
            {
                ReturnUrl = returnUrl
            };

            GetMock<ILoginViewModelBuilder>()
                .Setup(x => x.Build(returnUrl))
                .Returns(Task.FromResult(loginViewModel));

            // arrange
            var result = await ClassUnderTest.Login(returnUrl);

            // assert
            Assert.IsType<ViewResult>(result);
            Assert.Equal(loginViewModel, ((ViewResult)result).Model);
        }

        [Fact]
        public async Task Given_IsExternalLoginOnly_Login_should_redirect_to_External_controller()
        {
            // act
            var returnUrl = "test_url";
            var loginViewModel = new LoginViewModel
            {
                ReturnUrl = returnUrl,
                EnableLocalLogin = false,
                ExternalProviders = new List<ExternalProvider>() { new ExternalProvider() }
            };

            GetMock<ILoginViewModelBuilder>()
                .Setup(x => x.Build(returnUrl))
                .Returns(Task.FromResult(loginViewModel));

            // arrange
            var result = await ClassUnderTest.Login(returnUrl);

            // assert
            Assert.IsType<RedirectToActionResult>(result);

            var redirectToActionResult = result as RedirectToActionResult;
            Assert.NotNull(redirectToActionResult);
            Assert.Equal("Challenge", redirectToActionResult.ActionName);
            Assert.Equal("External", redirectToActionResult.ControllerName);
        }

        [Fact]
        public async Task Given_invalid_LoginInput_LoginPost_should_return_ModelStateError()
        {
            var controller = ClassUnderTest;
            var returnUrl = "return_url";
            var loginInputViewModel = new LoginInputModel
            {
                Username = "test",
                Password = null,
                RememberLogin = false,
                ReturnUrl = returnUrl
            };

            var loginViewModel = new LoginViewModel
            {
                ReturnUrl = returnUrl,
                EnableLocalLogin = false,
                ExternalProviders = new List<ExternalProvider>() { new ExternalProvider() }
            };

            GetMock<ILoginViewModelBuilder>()
              .Setup(x => x.Build(returnUrl))
              .Returns(Task.FromResult(loginViewModel));

            controller.ModelState.AddModelError("Password", "Password required!");


            var result = await controller.Login(loginInputViewModel);
            Assert.IsType<ViewResult>(result);
            Assert.Equal(loginViewModel, ((ViewResult)result).Model);
        }

        [Fact]
        public async Task Given_invalid_LoginOrPassword_LoginPost_should_return_ModelStateError()
        {
            var returnUrl = "return_url";
            var loginInputViewModel = new LoginInputModel
            {
                Username = "test",
                Password = null,
                ReturnUrl = returnUrl
            };

            var loginViewModel = new LoginViewModel
            {
                ReturnUrl = returnUrl,
                EnableLocalLogin = false,
                ExternalProviders = new List<ExternalProvider>() { new ExternalProvider() }
            };

            GetMock<ILoginViewModelBuilder>()
                .Setup(x => x.Build(returnUrl))
                .Returns(Task.FromResult(loginViewModel));

            GetMock<ILoginProvider>()
                .Setup(x => x.LoginUser(loginInputViewModel))
                .Returns(Task.FromResult(Microsoft.AspNetCore.Identity.SignInResult.Failed));

            var result = await ClassUnderTest.Login(loginInputViewModel);
            Assert.IsType<ViewResult>(result);

            var viewResult = ((ViewResult)result);

            Assert.False(viewResult.ViewData.ModelState.IsValid);
        }

        [Fact]
        public async Task Given_valid_input_login_and_IsPkceClient_redirect_to_Redirect_Page()
        {
            var returnUrl = "return_url";
            var loginInputViewModel = new LoginInputModel
            {
                Username = "test",
                Password = "valid",
                ReturnUrl = returnUrl
            };

            var loginViewModel = new LoginViewModel
            {
                ReturnUrl = returnUrl,
                EnableLocalLogin = false,
                ExternalProviders = new List<ExternalProvider>() { new ExternalProvider() }
            };

            var authContext = new AuthorizationRequest()
            {
                ClientId = "my_client_id"
            };

            var identityClient = new Client() 
            {
                 RequirePkce = true
            };

            GetMock<IIdentityServerInteractionService>()
                .Setup(x => x.GetAuthorizationContextAsync(loginInputViewModel.ReturnUrl))
                .Returns(Task.FromResult(authContext));

            GetMock<ILoginViewModelBuilder>()
                .Setup(x => x.Build(returnUrl))
                .Returns(Task.FromResult(loginViewModel));

            GetMock<ILoginProvider>()
                .Setup(x => x.LoginUser(loginInputViewModel))
                .Returns(Task.FromResult(Microsoft.AspNetCore.Identity.SignInResult.Success));

            GetMock<IClientStore>()
                .Setup(x => x.FindClientByIdAsync(authContext.ClientId))
                .Returns(Task.FromResult(identityClient));

            var result = await ClassUnderTest.Login(loginInputViewModel);

            Assert.IsType<ViewResult>(result);

            var viewResult = ((ViewResult)result);

            Assert.Equal("Redirect", viewResult.ViewName);
        }

        [Fact]
        public async Task Given_valid_input_login_and_not_IsPkceClient_redirect_to_return_url()
        {
            var returnUrl = "return_url";
            var loginInputViewModel = new LoginInputModel
            {
                Username = "test",
                Password = "valid",
                ReturnUrl = returnUrl
            };

            var loginViewModel = new LoginViewModel
            {
                ReturnUrl = returnUrl,
                EnableLocalLogin = false,
                ExternalProviders = new List<ExternalProvider>() { new ExternalProvider() }
            };

            var authContext = new AuthorizationRequest()
            {
                ClientId = "my_client_id"
            };

            var identityClient = new Client() 
            {
                 RequirePkce = false
            };

            GetMock<IIdentityServerInteractionService>()
                .Setup(x => x.GetAuthorizationContextAsync(loginInputViewModel.ReturnUrl))
                .Returns(Task.FromResult(authContext));

            GetMock<ILoginViewModelBuilder>()
                .Setup(x => x.Build(returnUrl))
                .Returns(Task.FromResult(loginViewModel));

            GetMock<ILoginProvider>()
                .Setup(x => x.LoginUser(loginInputViewModel))
                .Returns(Task.FromResult(Microsoft.AspNetCore.Identity.SignInResult.Success));

            GetMock<IClientStore>()
                .Setup(x => x.FindClientByIdAsync(authContext.ClientId))
                .Returns(Task.FromResult(identityClient));

            var result = await ClassUnderTest.Login(loginInputViewModel);

            Assert.IsType<RedirectResult>(result);

            var viewResult = ((RedirectResult)result);

            Assert.Equal("return_url", viewResult.Url);
        }

        [Fact]
        public async Task Given_valid_input_and_empty_returnUrl_then_redirect_to_index_page()
        {
            var returnUrl = "";
            var loginInputViewModel = new LoginInputModel
            {
                Username = "test",
                Password = "valid",
                ReturnUrl = returnUrl
            };

            var loginViewModel = new LoginViewModel
            {
                ReturnUrl = returnUrl,
                EnableLocalLogin = false,
                ExternalProviders = new List<ExternalProvider>() { new ExternalProvider() }
            };

            GetMock<ILoginViewModelBuilder>()
                .Setup(x => x.Build(returnUrl))
                .Returns(Task.FromResult(loginViewModel));

            GetMock<ILoginProvider>()
                .Setup(x => x.LoginUser(loginInputViewModel))
                .Returns(Task.FromResult(Microsoft.AspNetCore.Identity.SignInResult.Success));

            var result = await ClassUnderTest.Login(loginInputViewModel);

            Assert.IsType<RedirectResult>(result);

            var viewResult = ((RedirectResult)result);

            Assert.Equal("~/", viewResult.Url);
        }

        [Fact]
        public async Task Given_valid_input_and_not_empty_returnUrl_and_IsLocalUrl_then_redirect_to_return_url()
        {
            var controller = ClassUnderTest;
            var returnUrl = "local_url";
            var urlHelperMock = GetMock<IUrlHelper>();
            var loginInputViewModel = new LoginInputModel
            {
                Username = "test",
                Password = "valid",
                ReturnUrl = returnUrl
            };

            var loginViewModel = new LoginViewModel
            {
                ReturnUrl = returnUrl,
                EnableLocalLogin = false,
                ExternalProviders = new List<ExternalProvider>() { new ExternalProvider() }
            };

            GetMock<ILoginViewModelBuilder>()
                .Setup(x => x.Build(returnUrl))
                .Returns(Task.FromResult(loginViewModel));

            GetMock<ILoginProvider>()
                .Setup(x => x.LoginUser(loginInputViewModel))
                .Returns(Task.FromResult(Microsoft.AspNetCore.Identity.SignInResult.Success));
            
            urlHelperMock.Setup(x => x.IsLocalUrl(returnUrl))
                .Returns(true);

            controller.Url = urlHelperMock.Object;
            var result = await controller.Login(loginInputViewModel);

            Assert.IsType<RedirectResult>(result);

            var viewResult = ((RedirectResult)result);

            Assert.Equal("local_url", viewResult.Url);
        }

        [Fact]
        public async Task Given_valid_input_and_not_empty_returnUrl_and_not_IsLocalUrl_should_throw()
        {
            var controller = ClassUnderTest;
            var returnUrl = "not_local_url";
            var urlHelperMock = GetMock<IUrlHelper>();
            var loginInputViewModel = new LoginInputModel
            {
                Username = "test",
                Password = "valid",
                ReturnUrl = returnUrl
            };

            var loginViewModel = new LoginViewModel
            {
                ReturnUrl = returnUrl,
                EnableLocalLogin = false,
                ExternalProviders = new List<ExternalProvider>() { new ExternalProvider() }
            };

            GetMock<ILoginViewModelBuilder>()
                .Setup(x => x.Build(returnUrl))
                .Returns(Task.FromResult(loginViewModel));

            GetMock<ILoginProvider>()
                .Setup(x => x.LoginUser(loginInputViewModel))
                .Returns(Task.FromResult(Microsoft.AspNetCore.Identity.SignInResult.Success));
            
            urlHelperMock.Setup(x => x.IsLocalUrl(returnUrl))
                .Returns(false);

            controller.Url = urlHelperMock.Object;

            var ex = await Assert.ThrowsAsync<Exception>(() => controller.Login(loginInputViewModel));

            Assert.Equal("invalid return URL", ex.Message);
        }
    }
}