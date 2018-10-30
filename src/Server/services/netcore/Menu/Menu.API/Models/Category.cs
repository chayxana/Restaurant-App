using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Menu.API.Models
{
	public class Category 
	{
	    public Guid Id { get; set; }

		[Required]
		public string Name { get; set; }

		[NotMapped]
		public string ShortName { get; set; }

		public string Color { get; set; }

		public virtual ICollection<Food> Foods { get; set; }
	}
}