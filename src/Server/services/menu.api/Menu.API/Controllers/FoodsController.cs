using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Menu.API.Abstraction.Managers;
using Menu.API.Abstraction.Repositories;
using Menu.API.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Menu.API.DataTransferObjects;
using Menu.API.Abstraction.Services;

namespace Menu.API.Controllers
{
    [Produces("application/json")]
    [Route("api/v1/[controller]")]
    public class FoodsController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IRepository<Food> _repository;
        private readonly IFoodPictureService _foodPictureService;

        public FoodsController(
            IMapper mapper,
            IRepository<Food> repository,
            IFoodPictureService foodPictureService)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _foodPictureService = foodPictureService ?? throw new ArgumentNullException(nameof(foodPictureService));
        }

        [HttpGet("{count?}/{skip?}")]
        public IEnumerable<FoodDto> Get(int? count = 10, int? skip = 0)
        {
            var entities = _repository.GetAll()
                .Skip(skip.Value)
                .Take(count.Value)
                .ToList();
            var result = _mapper.Map<IEnumerable<FoodDto>>(entities);
            return result;
        }

        [HttpGet("{id}")]
        public FoodDto Get(Guid id)
        {
            return _mapper.Map<FoodDto>(_repository.Get(id));
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Post([FromBody] FoodDto foodDto)
        {
            var food = _mapper.Map<Food>(foodDto);
            _repository.Create(food);
            var result = await _repository.Commit();
            if (result)
            {
                return Ok();
            }
            return BadRequest();
        }

        [HttpPost]
        [Route("UploadFoodImage")]
        [Authorize(Roles = "Admin")]
        public Task Post([Bind] List<IFormFile> files, [Bind] string foodId)
        {
            return _foodPictureService.UploadAndCreatePictures(files, foodId);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(304)]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> Put(Guid id, [FromBody] FoodDto foodDto)
        {
            if (id != foodDto.Id)
            {
                return BadRequest();
            }

            var food = _mapper.Map<Food>(foodDto);
            _repository.Update(id, food);
            
            if (foodDto.DeletedPictures?.Any() == true)
            {
                await _foodPictureService.RemovePictures(foodDto.DeletedPictures);
            }

            if (!_repository.HasChanges() && foodDto.DeletedPictures?.Any() == false)
            {
                return StatusCode(304);
            }

            if(!_repository.HasChanges() && foodDto.DeletedPictures?.Any() == true) 
            {
                return Ok();
            }

            return await _repository.Commit() ? Ok() : (IActionResult)BadRequest();
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var food = _repository.Get(id);
            _repository.Delete(food);
            return await _repository.Commit() ? Ok() : (IActionResult)BadRequest();
        }
    }
}