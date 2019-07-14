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

namespace Menu.API.Controllers
{
    [Produces("application/json")]
    [Route("api/v1/[controller]")]
    public class FoodsController : Controller
    {
        private readonly IFileUploadManager _fileUploadManager;
        private readonly IMapper _mapper;
        private readonly IRepository<Food> _repository;
        private readonly IRepository<FoodPicture> _pictureRepository;

        public FoodsController(
            IMapper mapper,
            IRepository<Food> repository,
            IRepository<FoodPicture> pictureRepository,
            IFileUploadManager fileUploadManager)
        {
            _mapper = mapper;
            _repository = repository;
            _pictureRepository = pictureRepository;
            _fileUploadManager = fileUploadManager;
        }

        [HttpGet("{count?}/{skip?}")]
        public IEnumerable<FoodDto> Get(int? count = 10, int? skip = 0)
        {
            var entities = _repository.GetAll()
                .Skip(skip.Value)
                .Take(count.Value)
                .ToList();

            return _mapper.Map<IEnumerable<FoodDto>>(entities);
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
        public async Task Post([Bind] List<IFormFile> files, [Bind] string foodId)
        {
            foreach (var file in files)
            {
                var fileName = await _fileUploadManager.Upload(file);

                var foodPicture = new FoodPicture()
                {
                    Id = Guid.NewGuid(),
                    FoodId = Guid.Parse(foodId),
                    OriginalFileName = file.FileName,
                    FileName = fileName,
                    Length = file.Length,
                    ContentType = file.ContentType
                };

                _pictureRepository.Create(foodPicture);
                await _pictureRepository.Commit();
            }
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Put(Guid id, [FromBody] FoodDto foodDto)
        {
            if (id != foodDto.Id)
            {
                return BadRequest();
            }

            var food = _mapper.Map<Food>(foodDto);
            _repository.Update(id, food);
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