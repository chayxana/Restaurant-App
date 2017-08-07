using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Restaurant.DataTransferObjects;
using Restaurant.Server.Api.Abstractions.Facades;
using Restaurant.Server.Api.Abstractions.Repositories;
using Restaurant.Server.Api.Models;

namespace Restaurant.Server.Api.Controllers
{
	[Produces("application/json")]
	[Route("/api/[controller]")]
	[AllowAnonymous]
	public class CategoriesController : Controller
    {
		private readonly IMapperFacade mapperFacade;
		private readonly IRepository<Category> repository;

		public CategoriesController(
			IMapperFacade mapperFacade, 
			IRepository<Category> repository)
		{
			this.mapperFacade = mapperFacade;
			this.repository = repository;
		}

		[HttpGet]
		public IEnumerable<CategoryDto> Get()
		{
			return mapperFacade.Map<IEnumerable<CategoryDto>>(repository.GetAll());
		}

		[HttpPost]
		public void Post([FromBody]CategoryDto category)
		{
			var entity = mapperFacade.Map<Category>(category);
			repository.Create(entity);
			repository.Commit().Wait();
		}
    }
}
