using System;
using System.ComponentModel.DataAnnotations;

namespace Restaurant.Server.Api.Models
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