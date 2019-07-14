using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BaseUnitTests;
using FluentAssertions;
using Menu.API.Abstraction.Managers;
using Menu.API.Abstraction.Repositories;
using Menu.API.Controllers;
using Menu.API.DataTransferObjects;
using Menu.API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace Menu.API.UnitTests.Controllers
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
            GetMock<IMapper>().Setup(x => x.Map<IEnumerable<FoodDto>>(It.IsAny<List<Food>>())).Returns(foodDtos);

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
            GetMock<IMapper>().Setup(x => x.Map<FoodDto>(It.IsAny<Food>())).Returns(foodDto);

            // When
            var result = ClassUnderTest.Get(id);

            // Then
            result.Should().Be(foodDto);
        }

        [Fact]
        public async Task Given_valid_inputs_Post_should_create_food_and_should_upload_image_before_creating()
        {
            // given
            var foodId = "123";
            var foodDto = new FoodDto();
            var food = new Food();
            var fileMock = GetMock<IFormFile>();
            GetMock<IMapper>().Setup(x => x.Map<Food>(foodDto)).Returns(food);
            GetMock<IRepository<Food>>().Setup(x => x.Commit()).Returns(Task.FromResult(true));

            // when 
            await ClassUnderTest.Post(new List<IFormFile>() { fileMock.Object }, foodId);
            var result = await ClassUnderTest.Post(foodDto);

            // then
            GetMock<IFileUploadManager>().Verify(x => x.Upload(fileMock.Object), Times.Once);
            GetMock<IRepository<Food>>().Verify(x => x.Create(food), Times.Once);
            result.Should().BeOfType<OkResult>();
        }

        [Fact]
        public async Task Given_id_and_FoodDto_Id_not_equal_Put_should_return_bad_request()
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
            var food = new Food { Id = id };

            GetMock<IMapper>().Setup(x => x.Map<Food>(foodDto)).Returns(food);
            GetMock<IRepository<Food>>().Setup(x => x.Commit()).Returns(Task.FromResult(true));


            var result = await ClassUnderTest.Put(id, foodDto);

            result.Should().BeOfType<OkResult>();

            GetMock<IRepository<Food>>().Verify(x => x.Update(id, food), Times.Once);
        }

        [Fact]
        public async Task Given_valid_food_dto_and_has_file_should_not_remove_file_and_should_and_food_picture_should_be_empty_create_food()
        {
            var id = Guid.NewGuid();
            var foodDto = new FoodDto { Id = id };
            var food = new Food { Id = id, };

            GetMock<IMapper>().Setup(x => x.Map<Food>(foodDto)).Returns(food);
            GetMock<IRepository<Food>>().Setup(x => x.Commit()).Returns(Task.FromResult(true));

            var fileUploader = GetMock<IFileUploadManager>();

            var result = await ClassUnderTest.Put(id, foodDto);

            result.Should().BeOfType<OkResult>();

            GetMock<IRepository<Food>>().Verify(x => x.Update(id, food), Times.Once);
        }

        [Fact]
        public async Task Given_invalid_params_repository_Update_should_throw_exception_Put_should_catch_this_exception_and_should_return_bad_request()
        {
            var id = Guid.NewGuid();
            var foodDto = new FoodDto { Id = id };
            var food = new Food { Id = id };

            GetMock<IMapper>().Setup(x => x.Map<Food>(foodDto)).Returns(food);
            var repository = GetMock<IRepository<Food>>();
            repository.Setup(x => x.Commit()).Returns(Task.FromResult(true));
            repository.Setup(x => x.Update(id, food)).Throws<Exception>();

            var result = await Assert.ThrowsAsync<Exception>(() => ClassUnderTest.Put(id, foodDto));
          
            result.Should().BeOfType<Exception>();

            GetMock<IRepository<Food>>().Verify(x => x.Update(id, food), Times.Once);
            GetMock<IRepository<Food>>().Verify(x => x.Commit(), Times.Never);
        }

        [Fact]
        public async Task Given_valid_id_Delete_should_delete_food_and_picture_from_folder_and_return_Ok_result()
        {
            var id = Guid.NewGuid();
            var food = new Food { Id = id };
            var repository = GetMock<IRepository<Food>>();
            repository.Setup(x => x.Get(id)).Returns(food);
            repository.Setup(x => x.Commit()).Returns(Task.FromResult(true));

            var result = await ClassUnderTest.Delete(id);

            result.Should().BeOfType<OkResult>();
            GetMock<IRepository<Food>>().Verify(x => x.Delete(food), Times.Once);
        }

        [Fact]
        public async Task Given_invalid_id_Delete_should_throw()
        {
            var id = Guid.Empty;
            var foodDto = new FoodDto { Id = id };
            GetMock<IMapper>().Setup(x => x.Map<Food>(foodDto)).Throws<Exception>();

            var result = await ClassUnderTest.Delete(id);
            result.Should().BeOfType<BadRequestResult>();
        }
    }
}
