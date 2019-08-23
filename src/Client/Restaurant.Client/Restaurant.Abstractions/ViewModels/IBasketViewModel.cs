using System.Collections.ObjectModel;

namespace Restaurant.Abstractions.ViewModels
{
    public interface IBasketViewModel : INavigatableViewModel
    {
        ObservableCollection<IBasketItemViewModel> Items { get; }

        decimal TotalPrice { get; }
    }
}