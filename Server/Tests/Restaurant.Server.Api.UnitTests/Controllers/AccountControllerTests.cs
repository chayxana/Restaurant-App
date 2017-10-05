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
			    .Returns(Task.FromResult(IdentityResult.Failed()));

			// When
		    var result = await ClassUnderTest.Register(dto);

			// Then
		    result.Should().BeOfType<BadRequestObjectResult>();
	    }
    }
}
