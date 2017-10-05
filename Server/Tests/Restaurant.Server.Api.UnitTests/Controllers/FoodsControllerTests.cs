using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using Restaurant.Common.DataTransferObjects;
using Restaurant.Server.Api.Abstractions.Facades;
using Restaurant.Server.Api.Abstractions.Repositories;
using Restaurant.Server.Api.Controllers;
using Restaurant.Server.Api.Models;
using Xunit;

namespace Restaurant.Server.Api.UnitTests.Controllers
{
    public class FoodsControllerTests : BaseAutoMockedTest<FoodsController>
    {
		[Fact]
	    public void Get_should_return_foods()
	    {
		    // Given
		    var foods = Enumerable.Repeat(new Food(), 5);
		    var foodDtos = Enumerable.Repeat(new FoodDto(), 5);

			GetMock<IRepository<Food>>().Setup(x => x.GetAll()).Returns(foods.AsQueryable());
		    GetMock<IMapperFacade>().Setup(x => x.Map<IEnumerable<FoodDto>>(foods.ToList())).Returns(foodDtos);

			// When
		    var result = ClassUnderTest.Get();

			// Then

		    result.Should().Equal(foodDtos);
	    }
    }
}
