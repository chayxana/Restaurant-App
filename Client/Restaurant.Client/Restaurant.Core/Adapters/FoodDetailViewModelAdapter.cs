using Autofac;
using Restaurant.Abstractions.Adapters;
using Restaurant.Common.DataTransferObjects;
using Restaurant.Core.ViewModels;
using Restaurant.ViewModels;

namespace Restaurant.Core.Adapters
{
	public class FoodDetailViewModelAdapter : IFoodDetailViewModelAdapter
	{
		public IFoodDetailViewModel GetFoodDetailViewModel(FoodDto selectedFood)
		{
			return BootstrapperBase.Container.Resolve<FoodDetailViewModel>(new NamedParameter("selectedFood", selectedFood));
		}
	}
}