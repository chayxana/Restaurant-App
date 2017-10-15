using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using Moq;
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
		    GetMock<IMapperFacade>().Setup(x => x.Map<IEnumerable<FoodDto>>(It.IsAny<List<Food>>())).Returns(foodDtos);

			// When
		    var result = ClassUnderTest.Get();

			// Then
		    result.Should().Equal(foodDtos);
	    }

		[Fact]
	    public void Given_food_id_Get_should_return_food()
	    {
			// Given
		    var food = new Food();
			var foodDto = new FoodDto();
		    var id = Guid.NewGuid();

		    GetMock<IRepository<Food>>().Setup(x => x.Get(id)).Returns(food);
		    GetMock<IMapperFacade>().Setup(x => x.Map<FoodDto>(It.IsAny<Food>())).Returns(foodDto);

			// When
		    var result = ClassUnderTest.Get(id);

		    // Then
		    result.Should().Be(foodDto);
	    }
    }
}
