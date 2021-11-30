using System.Runtime.Serialization;
using System.Threading.Tasks;
using AutoMapper;
using ReactiveUI;
using Restaurant.Abstractions.Api;
using Restaurant.Abstractions.Constants;
using Restaurant.Abstractions.Enums;
using Restaurant.Abstractions.Facades;
using Restaurant.Abstractions.Providers;
using Restaurant.Abstractions.ViewModels;

namespace Restaurant.Core.ViewModels.Android
{
	public class MasterViewModel : ReactiveObject, IMasterViewModel, IRequiredAuthenticationViewModel
	{
		private readonly IAuthenticationProvider _authenticationProvider;
		private readonly IAccountApi _accountApi;
		private readonly IMapper _mapper;
		
		public MasterViewModel(
			IAuthenticationProvider authenticationProvider,
			IAccountApi accountApi,
			IMapper mapper)
		{
			_authenticationProvider = authenticationProvider;
			_accountApi = accountApi;
			_mapper = mapper;
		}

		private NavigationItem _selectedNavigationItem;

		public NavigationItem SelectedNavigationItem
		{
			get => _selectedNavigationItem;
			set => this.RaiseAndSetIfChanged(ref _selectedNavigationItem, value);
		}

		private IUserViewModel _userViewModel;
		public IUserViewModel UserViewModel
		{
			get => _userViewModel;
			set => this.RaiseAndSetIfChanged(ref _userViewModel, value);
		}

		public string Title => "Menu";

		public virtual Task<string> GetAccessToken()
		{
			return _authenticationProvider.GetAccessToken();
		}

		public async Task LoadUserInfo()
		{
			var accessToken = await GetAccessToken();

			var userDto = await _accountApi.GetUserInfo($"{ApiConstants.Bearer} {accessToken}");

			UserViewModel = _mapper.Map<UserViewModel>(userDto);
		}
	}
}