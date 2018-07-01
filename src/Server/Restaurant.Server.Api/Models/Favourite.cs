using System;

namespace Restaurant.Server.Api.Models
{
	public class Favorite
	{
		public string UserId { get; set; }

		public Guid FoodId { get; set; }
	}
}