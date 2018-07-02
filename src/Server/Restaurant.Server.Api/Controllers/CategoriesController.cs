using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Restaurant.Common.DataTransferObjects;
using Restaurant.Server.Api.Abstraction.Facades;
using Restaurant.Server.Api.Abstraction.Repositories;
using Restaurant.Server.Api.Models;

namespace Restaurant.Server.Api.Controllers
{
    [Produces("application/json")]
    [Route("/api/v1/[controller]")]
    [AllowAnonymous]
    public class CategoriesController : Controller
    {
        private readonly IMapperFacade _mapperFacade;
        private readonly IRepository<Category> _repository;

        public CategoriesController(
            IMapperFacade mapperFacade,
            IRepository<Category> repository)
        {
            _mapperFacade = mapperFacade;
            _repository = repository;
        }

        [HttpGet]
        public IEnumerable<CategoryDto> Get()
        {
            return _mapperFacade.Map<IEnumerable<CategoryDto>>(_repository.GetAll());
        }

        [HttpGet("{id}")]
        public CategoryDto Get(Guid id)
        {
            return _mapperFacade.Map<CategoryDto>(_repository.Get(id));
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CategoryDto category)
        {
            try
            {
                var entity = _mapperFacade.Map<Category>(category);
                _repository.Create(entity);
                return await _repository.Commit()
                    ? Ok()
                    : (IActionResult)BadRequest();
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> Put(Guid id, [FromBody] CategoryDto categoryDto)
        {
            try
            {
                if (id != categoryDto.Id)
                    return BadRequest();

                var category = _mapperFacade.Map<Category>(categoryDto);

                _repository.Update(id, category);
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
                var category = _repository.Get(id);
                _repository.Delete(category);

                return await _repository.Commit() ? Ok() : (IActionResult)BadRequest();
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
    }
}