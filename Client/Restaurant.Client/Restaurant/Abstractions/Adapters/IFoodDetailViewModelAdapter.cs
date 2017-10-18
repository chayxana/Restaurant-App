using Restaurant.Common.DataTransferObjects;
using Restaurant.ViewModels;

namespace Restaurant.Abstractions.Adapters
{
	public interface IFoodDetailViewModelAdapter
	{
		FoodDetailViewModel GetFoodDetailViewModel(FoodDto selectedFood);
	}
}