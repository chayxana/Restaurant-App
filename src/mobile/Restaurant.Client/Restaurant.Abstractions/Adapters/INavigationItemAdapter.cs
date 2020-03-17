using Restaurant.Abstractions.Enums;

namespace Restaurant.Abstractions.Adapters
{
    public interface INavigationItemAdapter
    {
        INavigatableViewModel GetViewModelFromNavigationItem(NavigationItem navigationItem);
    }
}