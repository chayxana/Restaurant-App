using ReactiveUI;
using Restaurant.ViewModels;

namespace Restaurant.Abstractions.ViewModels
{
    public interface IBasketViewModel : INavigatableViewModel
    {
        ReactiveList<OrderViewModel> Orders { get; }

        string OrdersCount { get; set; }

	    void AddOrder(OrderViewModel order);
    }
}