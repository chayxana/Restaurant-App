using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
			return App.Container.Resolve<FoodDetailViewModel>(new NamedParameter("selectedFood", selectedFood));
		}
	}
}
