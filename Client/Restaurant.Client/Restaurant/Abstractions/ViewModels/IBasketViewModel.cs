using ReactiveUI;
using Restaurant.ViewModels;

namespace Restaurant.Abstractions.ViewModels
{
    public interface IBasketViewModel : INavigatableViewModel
    {
        ReactiveList<OrderViewModel> Orders { get; set; }

        string OrdersCount { get; set; }
    }
}