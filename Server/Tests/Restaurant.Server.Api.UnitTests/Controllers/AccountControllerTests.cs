using System.Security.Claims;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Restaurant.Common.DataTransferObjects;
using Restaurant.Server.Api.Abstractions.Facades;
using Restaurant.Server.Api.Controllers;
using Restaurant.Server.Api.Models;
using Xunit;

namespace Restaurant.Server.Api.UnitTests.Controllers
{
    public class AccountControllerTests : BaseAutoMockedTest<AccountController>
    {
		[Fact]
	    public async Task Given_valid_parameters_Register_should_return_Ok()
		{
			// Given
			var dto = new RegisterDto();
			GetMock<IUserManagerFacade>()
				.Setup(x => x.Create(It.IsAny<User>(), It.IsAny<string>()))
				.Returns(Task.FromResult(IdentityResult.Success));

			// When
			var result = await ClassUnderTest.Register(dto);

			// Then
			result.Should().BeOfType<OkResult>();
		}

		[Fact]
	    public async Task Given_invalid_parameters_Register_should_return_BadRequest()
	    {
			// Gevin 
		    var dto = new RegisterDto();
			GetMock<IUserManagerFacade>()
			    .Setup(x => x.Create(It.IsAny<User>(), It.IsAny<string>()))
			    .Returns(Task.FromResult(IdentityResult.Failed(new IdentityError{ Code = "404", Description = "Error"})));

			// When
		    var result = await ClassUnderTest.Register(dto);

			// Then
		    result.Should().BeOfType<BadRequestObjectResult>();
	    }

		[Fact]
	    public async Task Given_authorized_user_GetUserInfo_should_return_user_info()
	    {
		    // Given
			var userDto = new UserDto();
		    GetMock<IUserManagerFacade>().Setup(x => x.GetAsync(It.IsAny<ClaimsPrincipal>())).Returns(Task.FromResult(new User()));
		    GetMock<IMapperFacade>().Setup(x => x.Map<UserDto>(It.IsAny<User>())).Returns(userDto);

			// when
		    var result = await ClassUnderTest.GetUserInfo();

			// then
		    result.Should().Be(userDto);
	    }
    }
}
