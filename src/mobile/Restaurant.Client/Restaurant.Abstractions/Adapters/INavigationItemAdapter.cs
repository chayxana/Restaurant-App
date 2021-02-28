using Restaurant.Abstractions.Enums;

namespace Restaurant.Abstractions.Adapters
{
    public interface INavigationItemAdapter
    {
        IRouteViewModel GetViewModelFromNavigationItem(NavigationItem navigationItem);
    }
}