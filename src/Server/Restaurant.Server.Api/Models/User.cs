using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace Restaurant.Server.Api.Models
{
	public class User : IdentityUser
	{
		public User()
		{
			Orders = new List<Order>();
			UserProfile = new UserProfile();
		}
		public virtual UserProfile UserProfile { get; set; }

		public virtual ICollection<Order> Orders { get; set; }
	}
}