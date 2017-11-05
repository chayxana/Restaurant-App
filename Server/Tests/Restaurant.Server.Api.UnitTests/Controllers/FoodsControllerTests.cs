using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Restaurant.Common.DataTransferObjects;
using Restaurant.Server.Api.Abstractions.Facades;
using Restaurant.Server.Api.Abstractions.Providers;
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

		[Fact]
	    public async Task Given_valid_inputs_Post_should_create_food_and_should_upload_image_before_creating()
		{
			// given
			var fileId = "123";
			var foodDto = new FoodDto();
			var food = new Food();
			var fileMock = GetMock<IFormFile>();
			GetMock<IMapperFacade>().Setup(x => x.Map<Food>(foodDto)).Returns(food);
			GetMock<IFileUploadProvider>().Setup(x => x.GetUploadedFileByUniqId(food.Id.ToString())).Returns("picture_name");
			GetMock<IRepository<Food>>().Setup(x => x.Commit()).Returns(Task.FromResult(true));

			// when 
			await ClassUnderTest.Post(fileMock.Object, fileId);
			var result = await ClassUnderTest.Post(foodDto);

			// then
			GetMock<IFileUploadProvider>().Verify(x => x.Upload(fileMock.Object, fileId), Times.Once);
			GetMock<IRepository<Food>>().Verify(x => x.Create(food), Times.Once);
			GetMock<IFileUploadProvider>().Verify(x => x.Reset(), Times.Once);
			result.Should().BeOfType<OkResult>();
		}

		[Fact]
	    public async Task Given_invalid_food_Post_should_return_bad_request_and_remove_uploaded_file()
	    {
		    // given
		    var fileId = "123";
		    var food = new Food();
		    var foodDto = new FoodDto
		    {
			    Id = food.Id
		    };

		    var fileMock = GetMock<IFormFile>();
		    GetMock<IMapperFacade>().Setup(x => x.Map<Food>(foodDto)).Returns(food);
		    GetMock<IFileUploadProvider>().Setup(x => x.GetUploadedFileByUniqId(food.Id.ToString())).Returns("picture_name");
		    GetMock<IRepository<Food>>().Setup(x => x.Commit()).Returns(Task.FromResult(false));
			
			// when 
			await ClassUnderTest.Post(fileMock.Object, fileId);
		    var result = await ClassUnderTest.Post(foodDto);

		    // then
		    GetMock<IFileUploadProvider>().Verify(x => x.Upload(fileMock.Object, fileId), Times.Once);
		    GetMock<IRepository<Food>>().Verify(x => x.Create(food), Times.Once);
		    GetMock<IFileUploadProvider>().Verify(x => x.RemoveUploadedFileByUniqId(foodDto.Id.ToString()), Times.Once);
		    result.Should().BeOfType<BadRequestResult>();
		}

		[Fact]
	    public async Task Given_not_uploaded_file_Post_should_throw_exception_and_catch_exception_and_should_return_bad_request()
	    {
		    var fileId = "123";
		    var food = new Food();
		    var foodDto = new FoodDto
		    {
			    Id = food.Id
		    };

		    var fileMock = GetMock<IFormFile>();
		    GetMock<IMapperFacade>().Setup(x => x.Map<Food>(foodDto)).Returns(food);
		    GetMock<IFileUploadProvider>().Setup(x => x.GetUploadedFileByUniqId(food.Id.ToString())).Throws<FileNotFoundException>();
		    GetMock<IRepository<Food>>().Setup(x => x.Commit()).Returns(Task.FromResult(false));

		    // when 
		    await ClassUnderTest.Post(fileMock.Object, fileId);
		    var result = await ClassUnderTest.Post(foodDto);

		    // then
		    GetMock<IFileUploadProvider>().Verify(x => x.Upload(fileMock.Object, fileId), Times.Once);
		    GetMock<IRepository<Food>>().Verify(x => x.Create(food), Times.Never);
		    GetMock<IFileUploadProvider>().Verify(x => x.RemoveUploadedFileByUniqId(foodDto.Id.ToString()), Times.Once);
		    result.Should().BeOfType<BadRequestResult>();
		}
    }
}
