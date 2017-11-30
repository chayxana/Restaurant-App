using System.Threading.Tasks;
using ReactiveUI;
using Restaurant.Abstractions.Api;
using Restaurant.Abstractions.Enums;
using Restaurant.Abstractions.Providers;
using Restaurant.Abstractions.ViewModels;
using Restaurant.Common.DataTransferObjects;

namespace Restaurant.Core.ViewModels.Android
{
    public class MasterViewModel : ReactiveObject, IMasterViewModel, IRequiredAuthenticationViewModel
    {
	    private readonly IAuthenticationProvider _authenticationProvider;
	    private readonly ISettingsProvider _settingsProvider;
	    private readonly IAccountApi _accountApi;

	    public MasterViewModel(
			IAuthenticationProvider authenticationProvider,
			ISettingsProvider settingsProvider,
			IAccountApi accountApi)
	    {
		    _authenticationProvider = authenticationProvider;
		    _settingsProvider = settingsProvider;
		    _accountApi = accountApi;
	    }

        private NavigationItem _selectedNavigationItem;

        public NavigationItem SelectedNavigationItem
        {
            get => _selectedNavigationItem;
            set => this.RaiseAndSetIfChanged(ref _selectedNavigationItem, value);
        }

	    private UserDto _userInfo;
	    public UserDto UserInfo
	    {
		    get => _userInfo;
		    set => this.RaiseAndSetIfChanged(ref _userInfo, value);
	    }

	    public string Title => "Menu";

		public virtual Task<string> GetAccessToken()
		{
			return _authenticationProvider.GetAccessToken();
		}

	    public async Task LoadUserInfo()
	    {
		    var accessToken = await GetAccessToken();

			UserInfo = await _accountApi.GetUser(accessToken);
	    }
	}
}