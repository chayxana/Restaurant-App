using System;
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
	public class DailyEatingsControllerTests : BaseAutoMockedTest<DailyEatingsController>
	{
		[Fact]
		public void Get_should_return_daily_eating_dtos()
		{
			var dailyEatings = new List<DailyEating>()
			{
				new DailyEating { AdditionalAmount = 10, Amount = 100, DateTime = DateTime.Now}
			};

			var dailyEatingDtos = new List<DailyEatingDto>
			{
				new DailyEatingDto { AdditionalAmount = 10, Amount = 100, DateTime = DateTime.Now}
			};

			GetMock<IRepository<DailyEating>>().Setup(x => x.GetAll()).Returns(dailyEatings.AsQueryable());
			GetMock<IMapperFacade>().Setup(x => x.Map<IEnumerable<DailyEatingDto>>(dailyEatings)).Returns(dailyEatingDtos);

			var result = ClassUnderTest.Get();

			result.Should().Equal(dailyEatingDtos);
		}

		[Fact]
		public void Given_valid_id_Get_should_return_some_specific_DailyEatingDto()
		{
			var id = Guid.NewGuid();
			var dailyEating = new DailyEating { Id = id, AdditionalAmount = 10, Amount = 100, DateTime = DateTime.Now };
			var dailyEatingDto = new DailyEatingDto { Id = id, AdditionalAmount = 10, Amount = 100, DateTime = DateTime.Now };

			GetMock<IRepository<DailyEating>>().Setup(x => x.Get(id)).Returns(dailyEating);
			GetMock<IMapperFacade>().Setup(x => x.Map<DailyEatingDto>(dailyEating)).Returns(dailyEatingDto);

			var result = ClassUnderTest.Get(id);

			result.Id.Should().Be(id);
			result.AdditionalAmount.Should().Be(dailyEatingDto.AdditionalAmount);
			result.Amount.Should().Be(dailyEatingDto.Amount);
		}
	}
}
