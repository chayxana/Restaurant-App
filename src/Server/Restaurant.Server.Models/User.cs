using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace Restaurant.Server.Models
{
	public class User : IdentityUser
	{
		public virtual UserProfile UserProfile { get; set; }

		public virtual ICollection<Order> Orders { get; set; }
	}
}