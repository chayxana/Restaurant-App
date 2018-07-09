using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Identity.API.IntegrationTests;
using Xunit;

namespace Restaurant.Server.Api.IntegrationTests.Controllers
{
    public class AccountControllerTests : IntegrationTestBase
    {
        //[Fact]
        //public async Task GetUserInfo_should_return_unauthorized_for_unauthorized_user()
        //{
        //    // Act
        //    var result = await HttpClient.GetAsync("/api/account/GetUserInfo");

        //    // Assert
        //    Assert.Equal(HttpStatusCode.Unauthorized, result.StatusCode);
        //}

        //[Fact]
        //public async Task GetUserInfo_should_return_OK_for_authorized_user()
        //{
        //    // Arrange
        //    await SetUpTokenFor("admin", "Test@123");

        //    // Act
        //    var result = await HttpClient.GetAsync("/api/values/");

        //    // Assert
        //    Assert.Equal(HttpStatusCode.OK, result.StatusCode);
        //}
    }
}
