using System.Collections.ObjectModel;
using ReactiveUI;

namespace Restaurant.Abstractions.ViewModels
{
    public interface IBasketViewModel : INavigatableViewModel
    {
        ObservableCollection<IBasketItemViewModel> Items { get; }

        string OrdersCount { get; }
    }
}