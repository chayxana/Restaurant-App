using System;
using System.Reactive.Subjects;
using Restaurant.Abstractions.Publishers;
using Restaurant.Abstractions.ViewModels;

namespace Restaurant.Core.Publishers
{
    public class BasketItemViewModelPublisher : IBasketItemViewModelPublisher
    {
        private readonly Subject<IBasketItemViewModel> _handler = new Subject<IBasketItemViewModel>();

        public IObservable<IBasketItemViewModel> Handler => _handler;
        
        public void Publish(IBasketItemViewModel data)
        {
            _handler.OnNext(data);
        }
    }
}