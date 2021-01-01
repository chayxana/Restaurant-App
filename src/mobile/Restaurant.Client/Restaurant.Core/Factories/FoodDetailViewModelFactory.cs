using System.Diagnostics.CodeAnalysis;
using Autofac;
using Restaurant.Abstractions.Factories;
using Restaurant.Abstractions.ViewModels;
using Restaurant.Core.ViewModels.Food;

namespace Restaurant.Core.Factories
{
    [ExcludeFromCodeCoverage]
    public class FoodDetailViewModelFactory : IFoodDetailViewModelFactory
    {
        private readonly ILifetimeScope _container;

        public FoodDetailViewModelFactory(ILifetimeScope container)
        {
            _container = container;
        }

        public IFoodDetailViewModel GetFoodDetailViewModel(IFoodViewModel selectedFood)
        {
            return _container.Resolve<FoodDetailViewModel>(new NamedParameter("selectedFood", selectedFood));
        }
    }
}