using System;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Reactive.Subjects;
using Restaurant.Abstractions.Publishers;
using Restaurant.Abstractions.Services;
using Restaurant.Abstractions.Subscribers;
using Restaurant.Abstractions.ViewModels;

namespace Restaurant.Core.Services
{
    public class BasketItemsService : IBasketItemsService,
        IBasketItemsCountPublisher,
        IBasketItemsCountSubscriber
    {
        private readonly Subject<int> _handler = new Subject<int>();

        public void Publish(int data)
        {
            _handler.OnNext(data);
        }

        public ObservableCollection<IBasketItemViewModel> Items { get; } =
            new ObservableCollection<IBasketItemViewModel>();

        public string ItemsCount
        {
            get
            {
                var sum = Items.Sum(x => x.Quantity);
                return sum == 0 ? null : sum.ToString(CultureInfo.InvariantCulture);
            }
        }

        public IObservable<int> Handler => _handler;

        public void Clear()
        {
            Items.Clear();
        }

        public void Add(IBasketItemViewModel item)
        {
            var addedItem = Items.FirstOrDefault(x => x.Food.Id == item.Food.Id);
            if (addedItem != null)
                addedItem.Quantity += item.Quantity;
            else
                Items.Add(item);

            var count = Items.Sum(x => x.Quantity);

            Publish((int) count);
        }
    }
}