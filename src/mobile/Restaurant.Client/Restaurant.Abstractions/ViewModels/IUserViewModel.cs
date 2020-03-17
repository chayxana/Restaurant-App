namespace Restaurant.Abstractions.ViewModels
{
	public interface IUserViewModel
	{
		string Email { get; set; }
		IUserInfoViewModel UserInfoViewModel { get; set; }
	}
}