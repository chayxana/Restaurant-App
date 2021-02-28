using System.Windows.Input;

namespace Restaurant.Abstractions.ViewModels
{
    public interface IFoodDetailViewModel : IRouteViewModel
    {
        ICommand AddToBasket { get; }
        ICommand GoToBasket { get; }
        IBasketItemViewModel CurrentBasketItem { get; }
        IFoodViewModel SelectedFood { get; }
    }
}