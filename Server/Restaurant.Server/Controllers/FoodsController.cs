using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Restaurant.DataTransferObjects;
using Restaurant.Server.Api.Abstractions.Facades;
using Restaurant.Server.Api.Abstractions.Repositories;
using Restaurant.Server.Api.Models;
using Restaurant.Server.Api.Providers;
using System.IO;

namespace Restaurant.Server.Api.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class FoodsController : Controller
    {
        private readonly IMapperFacade _mapperFacade;
        private readonly IRepository<Food> _repository;
	    private readonly IFileUploadProvider _fileUploadProvider;

	    public FoodsController(
            IMapperFacade mapperFacade,
            IRepository<Food> repository,
			IFileUploadProvider fileUploadProvider)
        {
            _mapperFacade = mapperFacade;
            _repository = repository;
	        _fileUploadProvider = fileUploadProvider;
        }

        [HttpGet]
        public IEnumerable<FoodDto> Get()
        {
            return _mapperFacade.Map<IEnumerable<FoodDto>>(_repository.GetAll());
        }

        [HttpGet("{id}", Name = "Get")]
        public FoodDto Get(Guid id)
        {
            return _mapperFacade.Map<FoodDto>(_repository.Get(id));
        }

		[HttpPost]
		public async Task<IActionResult> Post([FromBody]FoodDto foodDto)
		{
			try
			{
				var food = _mapperFacade.Map<Food>(foodDto);
				food.Picture = _fileUploadProvider.UploadedFileName;
				_repository.Create(food);
				return await _repository.Commit() ? Ok() : (IActionResult)BadRequest();

			}
			catch (Exception ex)
			{
				return BadRequest();
			}
		}

		[HttpPost]
		[Route("UploadFile")]
	    public async Task Post(IFormFile file)
	    {
			await _fileUploadProvider.Upload(file);
	    }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(Guid id, [FromBody]FoodDto foodDto)
        {
            try
            {
                var food = _mapperFacade.Map<Food>(foodDto);
                _repository.Update(food);

                return await _repository.Commit() ? Ok() : (IActionResult)BadRequest();

            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                var food = _repository.Get(id);
                _repository.Delete(food);
                return await _repository.Commit() ? Ok() : (IActionResult)BadRequest();
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
    }
}
