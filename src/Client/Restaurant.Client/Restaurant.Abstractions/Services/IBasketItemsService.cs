using System;
using System.Collections.ObjectModel;
using Restaurant.Abstractions.Subscribers;
using Restaurant.Abstractions.ViewModels;

namespace Restaurant.Abstractions.Services
{
    public interface IBasketItemsService
    {
        ObservableCollection<IBasketItemViewModel> Items { get; }

        string ItemsCount { get; }

        IObservable<int> ItemsCountChange { get; }

        void Clear();

        void Add(IBasketItemViewModel item);
    }
}