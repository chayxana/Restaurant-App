using Restaurant.Abstractions.Enums;

namespace Restaurant.Abstractions.ViewModels
{
    public interface IMasterViewModel : IRouteViewModel
    {
        NavigationItem SelectedNavigationItem { get; set; }

	    IUserViewModel UserViewModel { get; set; }
    }
}