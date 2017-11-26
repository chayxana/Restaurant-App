using Restaurant.Abstractions.ViewModels;
using Restaurant.Common.DataTransferObjects;

namespace Restaurant.Abstractions.Factories
{
    public interface IFoodDetailViewModelFactory
    {
        IFoodDetailViewModel GetFoodDetailViewModel(FoodDto selectedFood);
    }
}