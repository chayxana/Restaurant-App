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
	public class CategoryControllerTests : BaseAutoMockedTest<CategoriesController>
	{
		[Theory, AutoDomainData]
		public void Get_should_return_category_dtos(List<Category> categories, List<CategoryDto> categoryDtos)
		{
			GetMock<IRepository<Category>>().Setup(x => x.GetAll()).Returns(categories.AsQueryable());
			GetMock<IMapperFacade>().Setup(x => x.Map<IEnumerable<CategoryDto>>(categories)).Returns(categoryDtos);

			var result = ClassUnderTest.Get();

			result.Should().Equal(categoryDtos);
		}

		[Theory, AutoDomainData]
		public void Given_valid_id_Get_should_return_some_specific_Category_Dto(Fixture fixture)
		{
			var id = Guid.NewGuid();

			var category = fixture.Build<Category>()
				.With(x => x.Id, id)
				.Create();

			var categoryDto = fixture.Build<CategoryDto>()
				.With(x => x.Id, id)
				.Create();

			GetMock<IRepository<Category>>().Setup(x => x.Get(id)).Returns(category);
			GetMock<IMapperFacade>().Setup(x => x.Map<CategoryDto>(category)).Returns(categoryDto);

			var result = ClassUnderTest.Get(id);

			result.Id.Should().Be(id);
			result.Color.Should().Be(categoryDto.Color);
			result.Name.Should().Be(categoryDto.Name);
		}

		[Theory, AutoDomainData]
		public async Task Given_valid_dto_Post_should_create_category_and_should_return_OkResult(Category category, CategoryDto categoryDto)
		{
			GetMock<IMapperFacade>().Setup(x => x.Map<Category>(categoryDto)).Returns(category);
			GetMock<IRepository<Category>>().Setup(x => x.Commit()).Returns(Task.FromResult(true));

			var result = await ClassUnderTest.Post(categoryDto);

			result.Should().BeOfType<OkResult>();
			GetMock<IRepository<Category>>().Verify(x => x.Create(category), Times.Once);
		}

		[Theory, AutoDomainData]
		public async Task Given_invalid_dto_Post_should_not_create_category_and_should_return_BadResult(CategoryDto categoryDto, Category category)
		{
			GetMock<IMapperFacade>().Setup(x => x.Map<Category>(categoryDto)).Returns(category);
			GetMock<IRepository<Category>>().Setup(x => x.Commit()).Returns(Task.FromResult(false));

			var result = await ClassUnderTest.Post(categoryDto);

			result.Should().BeOfType<BadRequestResult>();
			GetMock<IRepository<Category>>().Verify(x => x.Create(category), Times.Once);
		}

		[Theory, AutoDomainData]
		public async Task Given_invalid_dto_Post_should_should_throw(Category category, CategoryDto categoryDto)
		{
			GetMock<IMapperFacade>().Setup(x => x.Map<Category>(categoryDto)).Returns(category);
			GetMock<IRepository<Category>>().Setup(x => x.Create(category)).Throws<Exception>();

			var result = await ClassUnderTest.Post(categoryDto);

			result.Should().BeOfType<BadRequestResult>();
		}

		[Theory, AutoDomainData]
		public async Task Given_valid_dto_Put_should_update_category_and_should_return_OkResult(Fixture fixture)
		{
			var id = Guid.NewGuid();

			var category = fixture.Build<Category>()
				.With(x => x.Id, id)
				.Create();

			var categoryDto = fixture.Build<CategoryDto>()
				.With(x => x.Id, id)
				.Create();

			GetMock<IMapperFacade>().Setup(x => x.Map<Category>(categoryDto)).Returns(category);
			GetMock<IRepository<Category>>().Setup(x => x.Commit()).Returns(Task.FromResult(true));

			var result = await ClassUnderTest.Put(id, categoryDto);

			result.Should().BeOfType<OkResult>();
			GetMock<IRepository<Category>>().Verify(x => x.Update(id, category), Times.Once);
		}


		[Theory, AutoDomainData]
		public async Task Given_invalid_dto_Put_should_not_update_category_and_should_return_BadResult(Fixture fixture)
		{
			var id = Guid.NewGuid();

			var category = fixture.Build<Category>()
				.With(x => x.Id, id)
				.Create();

			var categoryDto = fixture.Build<CategoryDto>()
				.With(x => x.Id, id)
				.Create();

			GetMock<IMapperFacade>().Setup(x => x.Map<Category>(categoryDto)).Returns(category);
			GetMock<IRepository<Category>>().Setup(x => x.Commit()).Returns(Task.FromResult(false));

			var result = await ClassUnderTest.Put(id, categoryDto);

			result.Should().BeOfType<BadRequestResult>();
			GetMock<IRepository<Category>>().Verify(x => x.Update(id, category), Times.Once);
		}

		[Theory, AutoDomainData]
		public async Task Given_invalid_dto_Put_should_should_throw(Fixture fixture)
		{
			var id = Guid.NewGuid();

			var category = fixture.Build<Category>()
				.With(x => x.Id, id)
				.Create();

			var categoryDto = fixture.Build<CategoryDto>()
				.With(x => x.Id, id)
				.Create();

			GetMock<IMapperFacade>().Setup(x => x.Map<Category>(categoryDto)).Returns(category);
			GetMock<IRepository<Category>>().Setup(x => x.Update(id, category)).Throws<Exception>();

			var result = await ClassUnderTest.Put(id, categoryDto);

			result.Should().BeOfType<BadRequestResult>();
		}

		[Theory, AutoDomainData]
		public async Task Given_id_not_equal_dto_id_Put_should_should_return_bad_request(Fixture fixture)
		{
			var id = Guid.NewGuid();
			
			var categoryDto = fixture.Build<CategoryDto>()
				.Create();

			var result = await ClassUnderTest.Put(id, categoryDto);

			result.Should().BeOfType<BadRequestResult>();
		}

		[Theory, AutoDomainData]
		public async Task Given_valid_id_Delete_should_remove_category_and_should_return_OkResult(Fixture fixture)
		{
			var id = Guid.NewGuid();

			var category = fixture.Build<Category>()
				.With(x => x.Id, id)
				.Create();

			GetMock<IRepository<Category>>().Setup(x => x.Get(id)).Returns(category);
			GetMock<IRepository<Category>>().Setup(x => x.Commit()).Returns(Task.FromResult(true));

			var result = await ClassUnderTest.Delete(id);

			result.Should().BeOfType<OkResult>();
			GetMock<IRepository<Category>>().Verify(x => x.Delete(category), Times.Once);
		}

		[Theory, AutoDomainData]
		public async Task Given_invalid_id_Delete_should_not_remove_category_and_should_return_BadResult(Fixture fixture)
		{
			var id = Guid.NewGuid();

			var category = fixture.Build<Category>()
				.With(x => x.Id, id)
				.Create();
			
			GetMock<IRepository<Category>>().Setup(x => x.Get(id)).Returns(category);
			GetMock<IRepository<Category>>().Setup(x => x.Commit()).Returns(Task.FromResult(false));

			var result = await ClassUnderTest.Delete(id);

			result.Should().BeOfType<BadRequestResult>();
			GetMock<IRepository<Category>>().Verify(x => x.Delete(category), Times.Once);
		}

		[Theory, AutoDomainData]
		public async Task Given_invalid_id_Delete_should_throw(Fixture fixture)
		{
			var id = Guid.NewGuid();

			var category = fixture.Build<Category>()
				.With(x => x.Id, id)
				.Create();
			
			GetMock<IRepository<Category>>().Setup(x => x.Get(id)).Returns(category);
			GetMock<IRepository<Category>>().Setup(x => x.Commit()).Returns(Task.FromResult(false));
			GetMock<IRepository<Category>>().Setup(x => x.Delete(category)).Throws<Exception>();

			var result = await ClassUnderTest.Delete(id);

			result.Should().BeOfType<BadRequestResult>();
		}
	}
}
