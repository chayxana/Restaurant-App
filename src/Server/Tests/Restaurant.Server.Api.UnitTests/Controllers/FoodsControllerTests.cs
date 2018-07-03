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
using Restaurant.Server.Api.Abstraction.Facades;
using Restaurant.Server.Api.Abstraction.Providers;
using Restaurant.Server.Api.Abstraction.Repositories;
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
            var result = ClassUnderTest.Get(10, 6);

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

        [Fact]
        public async Task Given_id_and_FoodDto_Id_not_equla_Put_should_retunr_bad_request()
        {
            var id = Guid.NewGuid();
            var foodDto = new FoodDto
            {
                Id = Guid.NewGuid()
            };

            var result = await ClassUnderTest.Put(id, foodDto);

            result.Should().BeOfType<BadRequestResult>();
        }

        [Fact]
        public async Task Given_valid_food_dto_and_has_file_should_remove_file_and_should_create_food()
        {
            var id = Guid.NewGuid();
            var foodDto = new FoodDto { Id = id };
            var food = new Food { Id = id, Picture = "old_picture" };

            GetMock<IMapperFacade>().Setup(x => x.Map<Food>(foodDto)).Returns(food);
            GetMock<IRepository<Food>>().Setup(x => x.Commit()).Returns(Task.FromResult(true));

            var fileUploader = GetMock<IFileUploadProvider>();
            fileUploader.Setup(x => x.HasFile(id.ToString())).Returns(true);
            fileUploader.Setup(x => x.GetUploadedFileByUniqId(id.ToString())).Returns("picture");

            var result = await ClassUnderTest.Put(id, foodDto);

            result.Should().BeOfType<OkResult>();

            GetMock<IRepository<Food>>().Verify(x => x.Update(id, food), Times.Once);
            GetMock<IFileUploadProvider>().Verify(x => x.Remove("old_picture"), Times.Once);
        }

        [Fact]
        public async Task Given_valid_food_dto_and_has_file_should_not_remove_file_and_should_and_food_picture_should_be_empty_create_food()
        {
            var id = Guid.NewGuid();
            var foodDto = new FoodDto { Id = id };
            var food = new Food { Id = id, Picture = "old_picture" };

            GetMock<IMapperFacade>().Setup(x => x.Map<Food>(foodDto)).Returns(food);
            GetMock<IRepository<Food>>().Setup(x => x.Commit()).Returns(Task.FromResult(true));

            var fileUploader = GetMock<IFileUploadProvider>();
            fileUploader.Setup(x => x.HasFile(id.ToString())).Returns(false);
            fileUploader.Setup(x => x.GetUploadedFileByUniqId(id.ToString())).Returns("picture");

            var result = await ClassUnderTest.Put(id, foodDto);

            result.Should().BeOfType<OkResult>();

            GetMock<IRepository<Food>>().Verify(x => x.Update(id, food), Times.Once);
            GetMock<IFileUploadProvider>().Verify(x => x.Remove("old_picture"), Times.Never);
        }

        [Fact]
        public async Task Given_invalid_params_repository_Update_should_throw_exception_Put_should_catch_this_exception_and_should_return_bad_request()
        {
            var id = Guid.NewGuid();
            var foodDto = new FoodDto { Id = id };
            var food = new Food { Id = id, Picture = "old_picture" };

            GetMock<IMapperFacade>().Setup(x => x.Map<Food>(foodDto)).Returns(food);
            var repository = GetMock<IRepository<Food>>();
            repository.Setup(x => x.Commit()).Returns(Task.FromResult(true));
            repository.Setup(x => x.Update(id, food)).Throws<Exception>();
            var fileUploader = GetMock<IFileUploadProvider>();
            fileUploader.Setup(x => x.HasFile(id.ToString())).Returns(true);
            fileUploader.Setup(x => x.GetUploadedFileByUniqId(id.ToString())).Returns("picture");

            var result = await ClassUnderTest.Put(id, foodDto);

            result.Should().BeOfType<BadRequestResult>();

            GetMock<IRepository<Food>>().Verify(x => x.Update(id, food), Times.Once);
            GetMock<IRepository<Food>>().Verify(x => x.Commit(), Times.Never);
            GetMock<IFileUploadProvider>().Verify(x => x.Remove("old_picture"), Times.Once);
        }

        [Fact]
        public async Task Given_valid_id_Delete_should_delete_food_and_picture_from_folder_and_return_Ok_result()
        {
            var id = Guid.NewGuid();
            var food = new Food { Picture = "pic" };
            var repository = GetMock<IRepository<Food>>();
            repository.Setup(x => x.Get(id)).Returns(food);
            repository.Setup(x => x.Commit()).Returns(Task.FromResult(true));

            var result = await ClassUnderTest.Delete(id);

            result.Should().BeOfType<OkResult>();
            GetMock<IFileUploadProvider>().Verify(x => x.Remove(food.Picture), Times.Once);
            GetMock<IRepository<Food>>().Verify(x => x.Delete(food), Times.Once);
        }

        [Fact]
        public async Task Given_invalid_id_Delete_should_throw()
        {
            var id = Guid.Empty;
            var foodDto = new FoodDto { Id = id };
            GetMock<IMapperFacade>().Setup(x => x.Map<Food>(foodDto)).Throws<Exception>();

            var result = await ClassUnderTest.Delete(id);
            result.Should().BeOfType<BadRequestResult>();
        }
    }
}
