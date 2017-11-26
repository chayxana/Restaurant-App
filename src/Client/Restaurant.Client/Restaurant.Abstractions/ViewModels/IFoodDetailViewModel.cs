using System.Windows.Input;
using Restaurant.Common.DataTransferObjects;

namespace Restaurant.Abstractions.ViewModels
{
    public interface IFoodDetailViewModel : INavigatableViewModel
    {
        ICommand AddToBasket { get; }
        IBasketViewModel BasketViewModel { get; set; }
        IOrderViewModel CurrentOrder { get; }
        ICommand GoToBasket { get; }
        FoodDto SelectedFood { get; }
    }
}