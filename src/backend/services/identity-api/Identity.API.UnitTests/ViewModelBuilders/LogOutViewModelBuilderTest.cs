using System.Security.Claims;
using System.Threading.Tasks;
using BaseUnitTests;
using FluentAssertions;
using Identity.API.ViewModelBuilders;
using IdentityServer4.Models;
using IdentityServer4.Services;
using Microsoft.AspNetCore.Http;
using Moq;
using Xunit;

namespace Identity.API.UnitTests.ViewModelBuilders
{
    public class LogOutViewModelBuilderTest : BaseAutoMockedTest<LogOutViewModelBuilder>
    {
        [Fact]
        public async Task Build_ViewModel_Test()
        {
            var user = new ClaimsPrincipal(new ClaimsIdentity("loggedIn"));
            var mockHttpContext = new Mock<HttpContext>();
            mockHttpContext.SetupGet(x => x.User).Returns(user);

            GetMock<IHttpContextAccessor>().SetupGet(x => x.HttpContext)
                .Returns(mockHttpContext.Object);

            var result = await ClassUnderTest.Build("test");

            result.Should().NotBeNull();
            result.LogoutId.Should().Be("test");
        }

        [Fact]
        public async Task given_not_Authenticated_ShowLogoutPrompt_shouldBe_false()
        {
            var user = new ClaimsPrincipal();
            var mockHttpContext = new Mock<HttpContext>();
            mockHttpContext.SetupGet(x => x.User).Returns(user);

            GetMock<IHttpContextAccessor>().SetupGet(x => x.HttpContext)
                .Returns(mockHttpContext.Object);


            var result = await ClassUnderTest.Build("test");

            result.Should().NotBeNull();
            result.LogoutId.Should().Be("test");
            result.ShowLogoutPrompt.Should().BeFalse();
        }

        [Fact]
        public async Task given_GetLogoutContextAsync_ShowSignoutPrompt_false_ShowLogoutPrompt_shouldBe_false()
        {
            var user = new ClaimsPrincipal(new ClaimsIdentity("loggedIn"));
            var mockHttpContext = new Mock<HttpContext>();
            mockHttpContext.SetupGet(x => x.User).Returns(user);

            var request = new LogoutRequest("url", null) {ClientId = "test value"};

            GetMock<IHttpContextAccessor>().SetupGet(x => x.HttpContext)
                .Returns(mockHttpContext.Object);

            GetMock<IIdentityServerInteractionService>().Setup(x => x.GetLogoutContextAsync("test"))
                .Returns(Task.FromResult(request));

            var result = await ClassUnderTest.Build("test");

            result.Should().NotBeNull();
            result.LogoutId.Should().Be("test");
            result.ShowLogoutPrompt.Should().BeFalse();
        }
    }
}