using ReactiveUI;
using Restaurant.Abstractions.ViewModels;

namespace Restaurant.Core.ViewModels
{
	public class UserViewModel : ReactiveObject, IUserViewModel
	{
		private string _email;

		public string Email
		{
			get => _email;
			set => this.RaiseAndSetIfChanged(ref _email, value);
		}

		private IUserInfoViewModel _userInfoViewModel;

		public IUserInfoViewModel UserInfoViewModel
		{
			get => _userInfoViewModel;
			set => this.RaiseAndSetIfChanged(ref _userInfoViewModel, value);
		}
	}
}
