using System.Collections.ObjectModel;

namespace Restaurant.Abstractions.ViewModels
{
    public interface IBasketViewModel : IRouteViewModel
    {
        ObservableCollection<IBasketItemViewModel> Items { get; }

        decimal TotalPrice { get; }
    }
}