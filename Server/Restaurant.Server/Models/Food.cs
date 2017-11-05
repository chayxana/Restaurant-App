using System;
using System.ComponentModel.DataAnnotations;

namespace Restaurant.Server.Api.Models
{
	public class Food : BaseEntity
	{
		[Required]
		public string Name { get; set; }

		public string Description { get; set; }

		public string Recept { get; set; }

		[Required]
		public decimal Price { get; set; }

		[Required]
		public string Picture { get; set; }

		public string Currency { get; set; }

		public Guid CategoryId { get; set; }

		public virtual Category Category { get; set; }
	}
}