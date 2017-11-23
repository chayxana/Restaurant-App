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

        public ViewModelFactory() : this(BootstrapperBase.Container)
        {
        }

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
            catch (Exception e)
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
            catch (Exception e)
            {
                throw;
            }
        }
    }
}