using System;
using System.Diagnostics.CodeAnalysis;
using Autofac;
using Restaurant.Abstractions;
using Restaurant.Abstractions.Factories;

namespace Restaurant.Core.Factories
{
    [ExcludeFromCodeCoverage]
    public class ViewModelFactory : IViewModelFactory
    {
        private readonly IContainer _container;

        public ViewModelFactory(IContainer container)
        {
            _container = container;
        }

        public INavigatableViewModel GetViewModel(Type viewModelType)
        {
            try
            {
                return _container.Resolve(viewModelType) as INavigatableViewModel;
            }
#pragma warning disable CS0168 // Variable is declared but never used
			catch (Exception e)
#pragma warning restore CS0168 // Variable is declared but never used
			{
                throw;
            }
        }

        public INavigatableViewModel GetMainViewModel(Type viewModelType, string platform)
        {
            try
            {
                return _container.ResolveNamed(platform, viewModelType) as INavigatableViewModel;
            }
#pragma warning disable CS0168 // Variable is declared but never used
			catch (Exception e)
#pragma warning restore CS0168 // Variable is declared but never used
			{
                throw;
            }
        }
    }
}