using System.Collections.ObjectModel;
using Restaurant.Abstractions.Subscribers;
using Restaurant.Abstractions.ViewModels;

namespace Restaurant.Abstractions.Services
{
    public interface IBasketItemsService : IBasketItemsCountSubscriber
    {
        ObservableCollection<IBasketItemViewModel> Items { get; }

        string ItemsCount { get; }

        void Clear();

        void Add(IBasketItemViewModel item);
    }
}