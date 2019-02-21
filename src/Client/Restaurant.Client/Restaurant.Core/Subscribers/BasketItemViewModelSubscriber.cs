using System;
using Restaurant.Abstractions.Publishers;
using Restaurant.Abstractions.Subscribers;
using Restaurant.Abstractions.ViewModels;

namespace Restaurant.Core.Subscribers
{
    public class BasketItemViewModelSubscriber : IBasketItemViewModelSubscriber
    {
        public BasketItemViewModelSubscriber(IBasketItemViewModelPublisher publisher)
        {
            Handler = publisher.Handler;
        }

        public IObservable<IBasketItemViewModel> Handler { get; }
    }
}