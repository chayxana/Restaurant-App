using System;
using System.ComponentModel.DataAnnotations;

namespace Restaurant.Server.Models
{
	public abstract class BaseEntity
	{
		protected BaseEntity()
		{
			Id = Guid.NewGuid();
		}

		[Key]
		public virtual Guid Id { get; set; }
	}
}