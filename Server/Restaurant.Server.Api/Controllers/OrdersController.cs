using Microsoft.AspNetCore.Mvc;
using Restaurant.Common.DataTransferObjects;
using Restaurant.Server.Abstraction.Facades;
using Restaurant.Server.Abstraction.Repositories;
using Restaurant.Server.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Restaurant.Server.Api.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class OrdersController : Controller
    {
        private readonly IMapperFacade _mapperFacade;
        private readonly IRepository<Order> _repository;

        public OrdersController(
            IMapperFacade mapperFacade,
            IRepository<Order> repository)
        {
            _mapperFacade = mapperFacade;
            _repository = repository;
        }
        
        [HttpGet]
        public IEnumerable<OrderDto> Get()
        {
            var orders = _repository.GetAll();
            return _mapperFacade.Map<IEnumerable<OrderDto>>(orders);
        }
        
        [HttpGet("{id}")]
        public OrderDto Get(Guid id)
        {
            var order = _repository.Get(id);
            return _mapperFacade.Map<OrderDto>(order);
        }
        
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]OrderDto orderDto)
        {
            var order = _mapperFacade.Map<Order>(orderDto);
            _repository.Create(order);

            return await _repository.Commit() ? Ok() : (IActionResult)BadRequest();
        }
        
        // PUT: api/Orders/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]OrderDto value)
        {
        }
        
        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var order = _repository.Get(id);
            _repository.Delete(order);

            return await _repository.Commit() ? Ok() : (IActionResult) BadRequest();
        }
    }
}
