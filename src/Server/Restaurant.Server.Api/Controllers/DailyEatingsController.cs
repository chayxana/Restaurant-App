using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Restaurant.Common.DataTransferObjects;
using Restaurant.Server.Api.Abstraction.Facades;
using Restaurant.Server.Api.Abstraction.Repositories;
using Restaurant.Server.Api.Models;

namespace Restaurant.Server.Api.Controllers
{
    [Produces("application/json")]
    [Route("/api/v1/[controller]")]
    [AllowAnonymous]
    public class DailyEatingsController : Controller
    {
        private readonly ILogger _logger;
        private readonly IMapperFacade _mapperFacade;
        private readonly IRepository<DailyEating> _repository;

        public DailyEatingsController(
            IRepository<DailyEating> repository,
            ILogger<DailyEatingsController> logger,
            IMapperFacade mapperFacade)
        {
            _repository = repository;
            _logger = logger;
            _mapperFacade = mapperFacade;
        }

        [HttpGet]
        public IEnumerable<DailyEatingDto> Get()
        {
            var dailyEatings = _repository.GetAll();
            return _mapperFacade.Map<IEnumerable<DailyEatingDto>>(dailyEatings);
        }

        [HttpGet("{id}")]
        public DailyEatingDto Get(Guid id)
        {
            return _mapperFacade.Map<DailyEatingDto>(_repository.Get(id));
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] DailyEatingDto dailyEatingDto)
        {
            try
            {
                var dailyEating = _mapperFacade.Map<DailyEating>(dailyEatingDto);
                _repository.Create(dailyEating);
                return await _repository.Commit() ? Ok() : (IActionResult)BadRequest();
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(Guid id, [FromBody] DailyEatingDto dto)
        {
            try
            {
                var dailyEating = _mapperFacade.Map<DailyEating>(dto);
                _repository.Update(id, dailyEating);
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
                var dailyEating = _repository.Get(id);
                _repository.Delete(dailyEating);
                return await _repository.Commit() ? Ok() : (IActionResult)BadRequest();
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
    }
}