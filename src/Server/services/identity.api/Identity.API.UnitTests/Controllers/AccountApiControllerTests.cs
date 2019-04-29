using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using BaseUnitTests;
using FluentAssertions;
using Identity.API.Abstraction;
using Identity.API.Controllers.Account;
using Identity.API.Model.DataTransferObjects;
using Identity.API.Model.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace Identity.API.UnitTests.Controllers
{
    public class AccountApiControllerTests : BaseAutoMockedTest<AccountApiController>
    {
		[Theory, AutoDomainData]
	    public async Task Given_valid_parameters_Register_should_return_Ok(RegisterDto dto)
		{
			// Given
			GetMock<IUserManagerFacade>()
				.Setup(x => x.Create(It.IsAny<ApplicationUser>(), It.IsAny<string>()))
				.Returns(Task.FromResult(IdentityResult.Success));

			// When
			var result = await ClassUnderTest.Register(dto);

			// Then
			result.Should().BeOfType<OkResult>();
		}

		[Theory, AutoDomainData]
	    public async Task Given_invalid_parameters_Register_should_return_BadRequest(RegisterDto dto, IdentityError identityError)
	    {
			// Given 
			GetMock<IUserManagerFacade>()
			    .Setup(x => x.Create(It.IsAny<ApplicationUser>(), It.IsAny<string>()))
			    .Returns(Task.FromResult(IdentityResult.Failed(identityError)));

			// When
		    var result = await ClassUnderTest.Register(dto);

			// Then
		    result.Should().BeOfType<BadRequestObjectResult>();
	    }

		[Theory, AutoDomainData]
	    public async Task Given_authorized_user_GetUserInfo_should_return_user_info(UserDto userDto)
	    {
		    // Given
		    GetMock<IUserManagerFacade>()
			    .Setup(x => x.GetAsync(It.IsAny<ClaimsPrincipal>()))
			    .Returns(Task.FromResult(new ApplicationUser()));
		    
		    GetMock<IMapper>().Setup(x => x.Map<UserDto>(It.IsAny<ApplicationUser>())).Returns(userDto);

			// when
		    var result = await ClassUnderTest.GetUserInfo();

			// then
		    result.Should().Be(userDto);
	    }
    }
}
