using Restaurant.Abstractions.ViewModels;

namespace Restaurant.Abstractions.Factories
{
    public interface IFoodDetailViewModelFactory
    {
        IFoodDetailViewModel GetFoodDetailViewModel(IFoodViewModel selectedFood);
    }
}