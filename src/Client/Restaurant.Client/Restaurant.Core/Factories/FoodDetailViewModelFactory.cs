using System.Diagnostics.CodeAnalysis;
using Autofac;
using Restaurant.Abstractions.Adapters;
using Restaurant.Abstractions.Factories;
using Restaurant.Abstractions.ViewModels;
using Restaurant.Common.DataTransferObjects;
using Restaurant.Core.ViewModels;

namespace Restaurant.Core.Factories
{
    [ExcludeFromCodeCoverage]
    public class FoodDetailViewModelFactory : IFoodDetailViewModelFactory
    {
        private readonly IContainer _container;
        public FoodDetailViewModelFactory(IContainer container)
        {
            _container = container;
        }

        public IFoodDetailViewModel GetFoodDetailViewModel(IFoodViewModel selectedFood)
        {
            return _container.Resolve<FoodDetailViewModel>(new NamedParameter("selectedFood", selectedFood));
        }
    }
}