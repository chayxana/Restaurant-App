using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Restaurant.DataTransferObjects;
using Restaurant.Server.Abstractions.Facades;
using Restaurant.Server.Abstractions.Repositories;
using Restaurant.Server.Models;
using Restaurant.Server.Repositories;

namespace Restaurant.Server.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class FoodsController : Controller
    {
        private readonly IMapperFacade _mapperFacade;
        private readonly IRepository<Food> _repository;

        public FoodsController(
            IMapperFacade mapperFacade,
            IRepository<Food> repository)
        {
            _mapperFacade = mapperFacade;
            _repository = repository;
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
                var files = HttpContext.Request.Form.Files;
                foreach (var Image in files)
                {
                    if (Image != null && Image.Length > 0)
                    {
                    }
                }

                var food = _mapperFacade.Map<Food>(foodDto);
                _repository.Create(food);
                return await _repository.Commit() ? Ok() : (IActionResult)BadRequest();

            }
            catch (Exception)
            {
                return BadRequest();
            }
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
