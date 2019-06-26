using System.Collections.Generic;
using System.Threading.Tasks;
using BaseUnitTests;
using Identity.API.Abstraction.ViewModelBuilders;
using Identity.API.Controllers.Account;
using Identity.API.ViewModelBuilders;
using Microsoft.AspNetCore.Mvc;
using Xunit;

namespace Identity.API.UnitTests.Controllers
{
    public class AccountMvcControllerTest : BaseAutoMockedTest<AccountMvcController>
    {
        [Fact]
        public async Task Given_return_url_Login_should_return_view_with_viewModel()
        {
            // Given
            var returnUrl = "test_url";
            var loginViewModel = new LoginViewModel
            {
                ReturnUrl = returnUrl
            };

            GetMock<ILoginViewModelBuilder>()
                .Setup(x => x.Build(returnUrl))
                .Returns(Task.FromResult(loginViewModel));

            var result = await ClassUnderTest.Login(returnUrl);
            
            Assert.IsType<ViewResult>(result);
            Assert.Equal(loginViewModel, ((ViewResult)result).Model);
        }

        [Fact]
        public async Task Given_IsExternalLoginOnly_Login_should_redirect_to_External_controller()
        {
            // Given
            var returnUrl = "test_url";
            var loginViewModel = new LoginViewModel
            {
                ReturnUrl = returnUrl,
                EnableLocalLogin = false,
                ExternalProviders =  new List<ExternalProvider>() { new ExternalProvider() }
            };
            
            GetMock<ILoginViewModelBuilder>()
                .Setup(x => x.Build(returnUrl))
                .Returns(Task.FromResult(loginViewModel));
            
            var result = await ClassUnderTest.Login(returnUrl);

            Assert.IsType<RedirectToActionResult>(result);
                
            var redirectToActionResult = result as RedirectToActionResult;
            Assert.NotNull(redirectToActionResult);
            Assert.Equal("Challenge", redirectToActionResult.ActionName);
            Assert.Equal("External", redirectToActionResult.ControllerName);  
        }

        [Fact]
        public async Task Given_input_LoginPost()
        {
        }
    }
}