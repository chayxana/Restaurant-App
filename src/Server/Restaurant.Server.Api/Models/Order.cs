using System;
using System.Collections.Generic;

namespace Restaurant.Server.Api.Models
{
	public class Order : BaseEntity
	{
		public DateTime DateTime { get; set; }
        
		public string UserId { get; set; }

		public ICollection<OrderItem> OrderItems { get; set; }
	}
}