using ReactiveUI;
using Restaurant.Abstractions;
using Restaurant.Models;

namespace Restaurant.ViewModels
{
    public interface IBasketViewModel : INavigatableViewModel
    {
        ReactiveList<OrderViewModel> Orders { get; set; }

        int OrdersCount { get; set; }
    }
}