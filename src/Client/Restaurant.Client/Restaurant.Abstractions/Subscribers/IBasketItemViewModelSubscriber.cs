using Restaurant.Abstractions.ViewModels;

namespace Restaurant.Abstractions.Subscribers
{
    /// <summary>
    /// Subscriber for IBasketItemViewModel
    /// </summary>
    public interface IBasketItemViewModelSubscriber : ISubscriber<IBasketItemViewModel>
    {
    }
}