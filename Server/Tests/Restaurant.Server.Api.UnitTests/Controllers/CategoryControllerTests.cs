using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Restaurant.Common.DataTransferObjects;
using Restaurant.Server.Api.Abstractions.Facades;
using Restaurant.Server.Api.Abstractions.Repositories;
using Restaurant.Server.Api.Controllers;
using Restaurant.Server.Api.Models;
using Xunit;

namespace Restaurant.Server.Api.UnitTests.Controllers
{
	public class CategoryControllerTests : BaseAutoMockedTest<CategoriesController>
	{
		[Fact]
		public void Get_should_return_category_dtos()
		{
			var categories = new List<Category>()
			{
				new Category {Color = "#123", Name = "Category 1"},
				new Category {Color = "#124", Name = "Category 2"},
				new Category {Color = "#125", Name = "Category 3"},
			};

			var categoryDtos = new List<CategoryDto>
			{
				new CategoryDto {Color = "#123", Name = "Category 1"},
				new CategoryDto {Color = "#124", Name = "Category 2"},
				new CategoryDto {Color = "#125", Name = "Category 3"},
			};

			GetMock<IRepository<Category>>().Setup(x => x.GetAll()).Returns(categories.AsQueryable());
			GetMock<IMapperFacade>().Setup(x => x.Map<IEnumerable<CategoryDto>>(categories)).Returns(categoryDtos);

			var result = ClassUnderTest.Get();

			result.Should().Equal(categoryDtos);
		}

		[Fact]
		public void Given_valid_id_Get_should_return_some_specific_Category_Dto()
		{
			var id = Guid.NewGuid();
			var category = new Category { Id = id, Color = "#123", Name = "Category 1" };
			var categoryDto = new CategoryDto { Id = id, Color = "#123", Name = "Category 1" };

			GetMock<IRepository<Category>>().Setup(x => x.Get(id)).Returns(category);
			GetMock<IMapperFacade>().Setup(x => x.Map<CategoryDto>(category)).Returns(categoryDto);

			var result = ClassUnderTest.Get(id);

			result.Id.Should().Be(id);
			result.Color.Should().Be(category.Color);
			result.Name.Should().Be(category.Name);
		}

		[Fact]
		public async Task Given_valid_dto_Post_should_create_category_and_should_return_OkResult()
		{
			var category = new Category { Color = "#123", Name = "Category 1" };
			var categoryDto = new CategoryDto { Color = "#123", Name = "Category 1" };

			GetMock<IMapperFacade>().Setup(x => x.Map<Category>(categoryDto)).Returns(category);
			GetMock<IRepository<Category>>().Setup(x => x.Commit()).Returns(Task.FromResult(true));

			var result = await ClassUnderTest.Post(categoryDto);

			result.Should().BeOfType<OkResult>();
			GetMock<IRepository<Category>>().Verify(x => x.Create(category), Times.Once);
		}

		[Fact]
		public async Task Given_invalid_dto_Post_should_not_create_category_and_should_return_BadResult()
		{
			var category = new Category { Color = "#123", Name = "Category 1" };
			var categoryDto = new CategoryDto { Color = "#123", Name = "Category 1" };

			GetMock<IMapperFacade>().Setup(x => x.Map<Category>(categoryDto)).Returns(category);
			GetMock<IRepository<Category>>().Setup(x => x.Commit()).Returns(Task.FromResult(false));

			var result = await ClassUnderTest.Post(categoryDto);

			result.Should().BeOfType<BadRequestResult>();
			GetMock<IRepository<Category>>().Verify(x => x.Create(category), Times.Once);
		}

		[Fact]
		public async Task Given_invalid_dto_Post_should_should_throw()
		{
			var category = new Category { Color = "#123", Name = "Category 1" };
			var categoryDto = new CategoryDto { Color = "#123", Name = "Category 1" };

			GetMock<IMapperFacade>().Setup(x => x.Map<Category>(categoryDto)).Returns(category);
			GetMock<IRepository<Category>>().Setup(x => x.Create(category)).Throws<Exception>();

			var result = await ClassUnderTest.Post(categoryDto);

			result.Should().BeOfType<BadRequestResult>();
		}

		[Fact]
		public async Task Given_valid_dto_Put_should_update_category_and_should_return_OkResult()
		{
			var id = Guid.NewGuid();
			var category = new Category { Id = id, Color = "#123", Name = "Category 1" };
			var categoryDto = new CategoryDto { Id = id, Color = "#123", Name = "Category 1" };

			GetMock<IMapperFacade>().Setup(x => x.Map<Category>(categoryDto)).Returns(category);
			GetMock<IRepository<Category>>().Setup(x => x.Commit()).Returns(Task.FromResult(true));

			var result = await ClassUnderTest.Put(id, categoryDto);

			result.Should().BeOfType<OkResult>();
			GetMock<IRepository<Category>>().Verify(x => x.Update(id, category), Times.Once);
		}


		[Fact]
		public async Task Given_invalid_dto_Put_should_not_update_category_and_should_return_BadResult()
		{
			var id = Guid.NewGuid();
			var category = new Category { Id = id, Color = "#123", Name = "Category 1" };
			var categoryDto = new CategoryDto { Id = id, Color = "#123", Name = "Category 1" };

			GetMock<IMapperFacade>().Setup(x => x.Map<Category>(categoryDto)).Returns(category);
			GetMock<IRepository<Category>>().Setup(x => x.Commit()).Returns(Task.FromResult(false));

			var result = await ClassUnderTest.Put(id, categoryDto);

			result.Should().BeOfType<BadRequestResult>();
			GetMock<IRepository<Category>>().Verify(x => x.Update(id, category), Times.Once);
		}

		[Fact]
		public async Task Given_invalid_dto_Put_should_should_throw()
		{
			var id = Guid.NewGuid();
			var category = new Category { Id = id, Color = "#123", Name = "Category 1" };
			var categoryDto = new CategoryDto { Id = id, Color = "#123", Name = "Category 1" };

			GetMock<IMapperFacade>().Setup(x => x.Map<Category>(categoryDto)).Returns(category);
			GetMock<IRepository<Category>>().Setup(x => x.Update(id, category)).Throws<Exception>();

			var result = await ClassUnderTest.Put(id, categoryDto);

			result.Should().BeOfType<BadRequestResult>();
		}

		[Fact]
		public async Task Given_id_not_equal_dto_id_Put_should_should_return_bad_request()
		{
			var id = Guid.NewGuid();
			var categoryDto = new CategoryDto { Id = Guid.Empty, Color = "#123", Name = "Category 1" };

			var result = await ClassUnderTest.Put(id, categoryDto);

			result.Should().BeOfType<BadRequestResult>();
		}

		[Fact]
		public async Task Given_valid_id_Delete_should_remove_category_and_should_return_OkResult()
		{
			var id = Guid.NewGuid();
			var category = new Category { Id = id, Color = "#123", Name = "Category 1" };
			
			GetMock<IRepository<Category>>().Setup(x => x.Get(id)).Returns(category);
			GetMock<IRepository<Category>>().Setup(x => x.Commit()).Returns(Task.FromResult(true));

			var result = await ClassUnderTest.Delete(id);

			result.Should().BeOfType<OkResult>();
			GetMock<IRepository<Category>>().Verify(x => x.Delete(category), Times.Once);
		}

		[Fact]
		public async Task Given_invalid_id_Delete_should_not_remove_category_and_should_return_BadResult()
		{
			var id = Guid.NewGuid();
			var category = new Category { Id = id, Color = "#123", Name = "Category 1" };

			GetMock<IRepository<Category>>().Setup(x => x.Get(id)).Returns(category);
			GetMock<IRepository<Category>>().Setup(x => x.Commit()).Returns(Task.FromResult(false));

			var result = await ClassUnderTest.Delete(id);

			result.Should().BeOfType<BadRequestResult>();
			GetMock<IRepository<Category>>().Verify(x => x.Delete(category), Times.Once);
		}

		[Fact]
		public async Task Given_invalid_id_Delete_should_throw()
		{
			var id = Guid.NewGuid();
			var category = new Category { Id = id, Color = "#123", Name = "Category 1" };

			GetMock<IRepository<Category>>().Setup(x => x.Get(id)).Returns(category);
			GetMock<IRepository<Category>>().Setup(x => x.Commit()).Returns(Task.FromResult(false));
			GetMock<IRepository<Category>>().Setup(x => x.Delete(category)).Throws<Exception>();

			var result = await ClassUnderTest.Delete(id);

			result.Should().BeOfType<BadRequestResult>();
		}
	}
}
