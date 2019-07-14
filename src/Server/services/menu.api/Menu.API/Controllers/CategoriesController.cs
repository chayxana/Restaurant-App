using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Menu.API.Abstraction.Repositories;
using Menu.API.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Menu.API.DataTransferObjects;

namespace Menu.API.Controllers
{
    [Produces("application/json")]
    [Route("/api/v1/[controller]")]
    public class CategoriesController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IRepository<Category> _repository;

        public CategoriesController(
            IMapper mapper,
            IRepository<Category> repository)
        {
            _mapper = mapper;
            _repository = repository;
        }

        [HttpGet]
        public IEnumerable<CategoryDto> Get()
        {
            return _mapper.Map<IEnumerable<CategoryDto>>(_repository.GetAll());
        }

        [HttpGet("{id}")]
        public CategoryDto Get(Guid id)
        {
            return _mapper.Map<CategoryDto>(_repository.Get(id));
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Post([FromBody] CategoryDto category)
        {
            try
            {
                var entity = _mapper.Map<Category>(category);
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
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Put(Guid id, [FromBody] CategoryDto categoryDto)
        {
            try
            {
                if (id != categoryDto.Id)
                    return BadRequest();

                var category = _mapper.Map<Category>(categoryDto);

                _repository.Update(id, category);
                return await _repository.Commit() ? Ok() : (IActionResult)BadRequest();
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
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