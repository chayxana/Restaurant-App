using ReactiveUI;
using Restaurant.ViewModels;

namespace Restaurant.Abstractions.ViewModels
{
    public interface IBasketViewModel : INavigatableViewModel
    {
        ReactiveList<IOrderViewModel> Orders { get; }

        string OrdersCount { get; set; }

        void AddOrder(IOrderViewModel order);
    }
}