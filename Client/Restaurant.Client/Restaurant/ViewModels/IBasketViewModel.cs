using ReactiveUI;
using Restaurant.Abstractions;
using Restaurant.Models;

namespace Restaurant.ViewModels
{
    public interface IBasketViewModel : INavigatableViewModel
    {
        ReactiveList<OrderViewModel> Orders { get; set; }

        string OrdersCount { get; set; }
    }
}