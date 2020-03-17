using System;

namespace Restaurant.Abstractions.ViewModels
{
	public interface IUserInfoViewModel
	{
		DateTime BirthDate { get; set; }
		string LastName { get; set; }
		string Name { get; set; }
		string Picture { get; set; }
	}
}