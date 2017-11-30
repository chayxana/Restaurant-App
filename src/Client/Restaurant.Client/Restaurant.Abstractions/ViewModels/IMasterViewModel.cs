using Restaurant.Abstractions.Enums;
using Restaurant.Common.DataTransferObjects;

namespace Restaurant.Abstractions.ViewModels
{
    public interface IMasterViewModel : INavigatableViewModel
    {
        NavigationItem SelectedNavigationItem { get; set; }

		UserDto UserInfo { get; set; }
    }
}