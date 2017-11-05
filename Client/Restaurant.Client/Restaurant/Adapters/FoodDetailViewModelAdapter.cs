using Autofac;
using Restaurant.Abstractions.Adapters;
using Restaurant.Common.DataTransferObjects;
using Restaurant.ViewModels;

namespace Restaurant.Adapters
{
	public class FoodDetailViewModelAdapter : IFoodDetailViewModelAdapter
	{
		public FoodDetailViewModel GetFoodDetailViewModel(FoodDto selectedFood)
		{
			return Bootstrapper.Container.Resolve<FoodDetailViewModel>(new NamedParameter("selectedFood", selectedFood));
		}
	}
}