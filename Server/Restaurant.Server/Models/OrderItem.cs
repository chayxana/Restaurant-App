using System;

namespace Restaurant.Server.Api.Models
{
	public class OrderItem
	{
		public decimal Quantity { get; set; }

		public Guid FoodId { get; set; }

		public Guid OderId { get; set; }
	}
}