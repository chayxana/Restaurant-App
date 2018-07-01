using System;

namespace Restaurant.Server.Api.Models
{
	public class UserProfile : BaseEntity
	{
		public string Name { get; set; }
		public string LastName { get; set; }
		public DateTime BirthDate { get; set; }
		public string Picture { get; set; }
		public User User { get; set; }
		public string UserId { get; set; }
	}
}