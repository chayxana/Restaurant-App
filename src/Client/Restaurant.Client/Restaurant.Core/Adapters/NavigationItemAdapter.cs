using Restaurant.Abstractions;
using Restaurant.Abstractions.Adapters;
using Restaurant.Abstractions.Enums;
using Restaurant.Abstractions.Factories;
using Restaurant.Core.ViewModels;

namespace Restaurant.Core.Adapters
{
    public class NavigationItemAdapter : INavigationItemAdapter
    {
        private readonly IViewModelFactory _viewModelFactory;

        public NavigationItemAdapter(IViewModelFactory viewModelFactory)
        {
            _viewModelFactory = viewModelFactory;
        }

        public INavigatableViewModel GetViewModelFromNavigationItem(NavigationItem navigationItem)
        {
            switch (navigationItem)
            {
                case NavigationItem.Foods:
                    return _viewModelFactory.GetViewModel(typeof(FoodsViewModel));
                case NavigationItem.Orders:
                    return _viewModelFactory.GetViewModel(typeof(OrdersViewModel)); // TODO:
                case NavigationItem.Chat:
                    return _viewModelFactory.GetViewModel(typeof(OrdersViewModel)); // TODO:
                //case NavigationItem.Settings:
                //    return _viewModelFactory.GetViewModel(typeof(object)); // TODO:
                default:
                    return null;
            }
        }
    }
}