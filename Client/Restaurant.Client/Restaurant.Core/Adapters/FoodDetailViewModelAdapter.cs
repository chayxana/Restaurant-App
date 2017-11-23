using System.Diagnostics.CodeAnalysis;
using Autofac;
using Restaurant.Abstractions.Adapters;
using Restaurant.Common.DataTransferObjects;
using Restaurant.Core.ViewModels;
using Restaurant.ViewModels;

namespace Restaurant.Core.Adapters
{
    [ExcludeFromCodeCoverage]
    public class FoodDetailViewModelAdapter : IFoodDetailViewModelAdapter
    {
        public FoodDetailViewModelAdapter() : this(BootstrapperBase.Container)
        {
        }

        private readonly IContainer _container;
        public FoodDetailViewModelAdapter(IContainer container)
        {
            _container = container;
        }

        public IFoodDetailViewModel GetFoodDetailViewModel(FoodDto selectedFood)
        {
            return _container.Resolve<FoodDetailViewModel>(new NamedParameter("selectedFood", selectedFood));
        }
    }
}