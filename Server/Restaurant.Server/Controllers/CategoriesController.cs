using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Restaurant.DataTransferObjects;
using Restaurant.Server.Abstractions.Facades;
using Restaurant.Server.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Restaurant.Server.Controllers
{
	[Route("api/[controller]")]
	[AllowAnonymous]
	public class CategoriesController : Controller
    {
		private readonly IMapperFacade mapperFacade;
		private readonly DatabaseContext context;

		public CategoriesController(
			IMapperFacade mapperFacade, 
			DatabaseContext context)
		{
			this.mapperFacade = mapperFacade;
			this.context = context;
		}
		[HttpPost]
		public void Post(CategoryDto category)
		{
			var entity = mapperFacade.Map<Category>(category);
			context.Add(entity);
			context.SaveChanges();
		}
    }
}
