using Restaurant.Abstractions.ViewModels;

namespace Restaurant.Abstractions.Subscribers
{
    public interface IBasketItemViewModelSubscriber : ISubscriber<IBasketItemViewModel>
    {
    }
}