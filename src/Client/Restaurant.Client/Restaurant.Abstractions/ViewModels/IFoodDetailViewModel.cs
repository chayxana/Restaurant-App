using System.Windows.Input;
using Restaurant.Abstractions;
using Restaurant.Abstractions.ViewModels;
using Restaurant.Common.DataTransferObjects;

namespace Restaurant.ViewModels
{
    public interface IFoodDetailViewModel : INavigatableViewModel
    {
        ICommand AddToBasket { get; }
        IBasketViewModel BasketViewModel { get; set; }
        IOrderViewModel CurrentOrder { get; }
        ICommand GoToBasket { get; }
        FoodDto SelectedFood { get; }
        string Title { get; }
    }
}