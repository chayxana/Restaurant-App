using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoFixture;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Restaurant.Common.DataTransferObjects;
using Restaurant.Server.Api.Abstraction.Facades;
using Restaurant.Server.Api.Abstraction.Repositories;
using Restaurant.Server.Api.Controllers;
using Restaurant.Server.Api.Models;
using Xunit;

namespace Restaurant.Server.Api.UnitTests.Controllers
{
	public class DailyEatingsControllerTests : BaseAutoMockedTest<DailyEatingsController>
	{
		[Theory, AutoDomainData]
		public void Get_should_return_daily_eating_dtos(List<DailyEating> dailyEatings, List<DailyEatingDto> dailyEatingDtos)
		{
			GetMock<IRepository<DailyEating>>().Setup(x => x.GetAll()).Returns(dailyEatings.AsQueryable());
			GetMock<IMapperFacade>().Setup(x => x.Map<IEnumerable<DailyEatingDto>>(dailyEatings)).Returns(dailyEatingDtos);

			var result = ClassUnderTest.Get();

			result.Should().Equal(dailyEatingDtos);
		}

		[Theory, AutoDomainData]
		public void Given_valid_id_Get_should_return_some_specific_DailyEatingDto(Fixture fixture)
		{
			var id = Guid.NewGuid();
			var dailyEating = fixture.Build<DailyEating>()
				.With(x => x.Id, id)
				.Create();

			var dailyEatingDto = fixture.Build<DailyEatingDto>()
				.With(x => x.Id, id)
				.Create();
			
			GetMock<IRepository<DailyEating>>().Setup(x => x.Get(id)).Returns(dailyEating);
			GetMock<IMapperFacade>().Setup(x => x.Map<DailyEatingDto>(dailyEating)).Returns(dailyEatingDto);

			var result = ClassUnderTest.Get(id);

			result.Id.Should().Be(id);
			result.AdditionalAmount.Should().Be(dailyEatingDto.AdditionalAmount);
			result.Amount.Should().Be(dailyEatingDto.Amount);
		}

		[Theory, AutoDomainData]
		public async Task Given_valid_data_Post_should_create_dailyEating_and_should_return_OkResult(DailyEating dailyEating, DailyEatingDto dailyEatingDto)
		{
			// Given
			GetMock<IMapperFacade>().Setup(x => x.Map<DailyEating>(dailyEatingDto)).Returns(dailyEating);
			GetMock<IRepository<DailyEating>>().Setup(x => x.Commit()).Returns(Task.FromResult(true));
			
			// When 
			var result = await ClassUnderTest.Post(dailyEatingDto);

			// then
			result.Should().BeOfType<OkResult>();
			GetMock<IRepository<DailyEating>>().Verify(x => x.Create(dailyEating), Times.Once);
		}

		[Theory, AutoDomainData]
		public async Task Given_invalid_data_Post_should_not_create_dailyEating_and_should_return_BadResult(DailyEating dailyEating, DailyEatingDto dailyEatingDto)
		{
			// Given
			GetMock<IMapperFacade>().Setup(x => x.Map<DailyEating>(dailyEatingDto)).Returns(dailyEating);
			GetMock<IRepository<DailyEating>>().Setup(x => x.Commit()).Returns(Task.FromResult(false));

			// When 
			var result = await ClassUnderTest.Post(dailyEatingDto);

			// then
			result.Should().BeOfType<BadRequestResult>();
			GetMock<IRepository<DailyEating>>().Verify(x => x.Create(dailyEating), Times.Once);
		}

		[Theory, AutoDomainData]
		public async Task Given_invalid_data_Post_should_not_create_dailyEating_and_should_throw_and_should_return_BadResult(DailyEating dailyEating, DailyEatingDto dailyEatingDto)
		{
			// Given
			GetMock<IMapperFacade>().Setup(x => x.Map<DailyEating>(dailyEatingDto)).Returns(dailyEating);
			GetMock<IRepository<DailyEating>>().Setup(x => x.Commit()).Returns(Task.FromResult(false));
			GetMock<IRepository<DailyEating>>().Setup(x => x.Create(dailyEating)).Throws<Exception>();

			// When 
			var result = await ClassUnderTest.Post(dailyEatingDto);

			// then
			result.Should().BeOfType<BadRequestResult>();
		}

		[Theory, AutoDomainData]
		public async Task Given_valid_dto_Put_should_update_DailyEating_and_should_return_OkResult(Fixture fixture)
		{
			var id = Guid.NewGuid();

			var dailyEating = fixture.Build<DailyEating>()
				.With(x => x.Id, id)
				.Create();

			var dailyEatingDto = fixture.Build<DailyEatingDto>()
				.With(x => x.Id, id)
				.Create();

			GetMock<IMapperFacade>().Setup(x => x.Map<DailyEating>(dailyEatingDto)).Returns(dailyEating);
			GetMock<IRepository<DailyEating>>().Setup(x => x.Commit()).Returns(Task.FromResult(true));

			var result = await ClassUnderTest.Put(id, dailyEatingDto);

			result.Should().BeOfType<OkResult>();
			GetMock<IRepository<DailyEating>>().Verify(x => x.Update(id, dailyEating), Times.Once);
		}


		[Theory, AutoDomainData]
		public async Task Given_invalid_dto_Put_should_not_update_DailyEating_and_should_return_BadResult(Fixture fixture)
		{
			var id = Guid.NewGuid();

			var dailyEating = fixture.Build<DailyEating>()
				.With(x => x.Id, id)
				.Create();

			var dailyEatingDto = fixture.Build<DailyEatingDto>()
				.With(x => x.Id, id)
				.Create();

			GetMock<IMapperFacade>().Setup(x => x.Map<DailyEating>(dailyEatingDto)).Returns(dailyEating);
			GetMock<IRepository<DailyEating>>().Setup(x => x.Commit()).Returns(Task.FromResult(false));

			var result = await ClassUnderTest.Put(id, dailyEatingDto);

			result.Should().BeOfType<BadRequestResult>();
			GetMock<IRepository<DailyEating>>().Verify(x => x.Update(id, dailyEating), Times.Once);
		}

		[Theory, AutoDomainData]
		public async Task Given_invalid_DailyEatingDto_Put_should_should_throw(Fixture fixture)
		{
			var id = Guid.NewGuid();

			var dailyEating = fixture.Build<DailyEating>()
				.With(x => x.Id, id)
				.Create();

			var dailyEatingDto = fixture.Build<DailyEatingDto>()
				.With(x => x.Id, id)
				.Create();

			GetMock<IMapperFacade>().Setup(x => x.Map<DailyEating>(dailyEatingDto)).Returns(dailyEating);
			GetMock<IRepository<DailyEating>>().Setup(x => x.Update(id, dailyEating)).Throws<Exception>();

			var result = await ClassUnderTest.Put(id, dailyEatingDto);

			result.Should().BeOfType<BadRequestResult>();
		}

		[Theory, AutoDomainData]
		public async Task Given_id_not_equal_DailyEatingDto_id_Put_should_return_bad_request(Fixture fixture)
		{
			var id = Guid.NewGuid();
			
			var dailyEatingDto = fixture.Build<DailyEatingDto>()
				.With(x => x.Id, id)
				.Create();

			var result = await ClassUnderTest.Put(id, dailyEatingDto);

			result.Should().BeOfType<BadRequestResult>();
		}


		[Theory, AutoDomainData]
		public async Task Given_valid_DailyEating_id_Delete_should_remove_DailyEating_and_should_return_OkResult(Fixture fixture)
		{
			var id = Guid.NewGuid();

			var dailyEating = fixture.Build<DailyEating>()
				.With(x => x.Id, id)
				.Create();

			GetMock<IRepository<DailyEating>>().Setup(x => x.Get(id)).Returns(dailyEating);
			GetMock<IRepository<DailyEating>>().Setup(x => x.Commit()).Returns(Task.FromResult(true));

			var result = await ClassUnderTest.Delete(id);

			result.Should().BeOfType<OkResult>();
			GetMock<IRepository<DailyEating>>().Verify(x => x.Delete(dailyEating), Times.Once);
		}

		[Theory, AutoDomainData]
		public async Task Given_invalid_DailyEating_id_Delete_should_not_remove_category_and_should_return_BadResult(Fixture fixture)
		{
			var id = Guid.NewGuid();

			var dailyEating = fixture.Build<DailyEating>()
				.With(x => x.Id, id)
				.Create();

			GetMock<IRepository<DailyEating>>().Setup(x => x.Get(id)).Returns(dailyEating);
			GetMock<IRepository<DailyEating>>().Setup(x => x.Commit()).Returns(Task.FromResult(false));

			var result = await ClassUnderTest.Delete(id);

			result.Should().BeOfType<BadRequestResult>();
			GetMock<IRepository<DailyEating>>().Verify(x => x.Delete(dailyEating), Times.Once);
		}

		[Theory, AutoDomainData]
		public async Task Given_invalid_DailyEating_id_Delete_should_throw(Fixture fixture)
		{
			var id = Guid.NewGuid();

			var dailyEating = fixture.Build<DailyEating>()
				.With(x => x.Id, id)
				.Create();


			GetMock<IRepository<DailyEating>>().Setup(x => x.Get(id)).Returns(dailyEating);
			GetMock<IRepository<DailyEating>>().Setup(x => x.Commit()).Returns(Task.FromResult(false));
			GetMock<IRepository<DailyEating>>().Setup(x => x.Delete(dailyEating)).Throws<Exception>();

			var result = await ClassUnderTest.Delete(id);

			result.Should().BeOfType<BadRequestResult>();
		}
	}
}
