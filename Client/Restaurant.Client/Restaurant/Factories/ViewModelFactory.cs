using System;
using Autofac;
using Restaurant.Abstractions;
using Restaurant.Abstractions.Factories;

namespace Restaurant.Factories
{
    public class ViewModelFactory : IViewModelFactory
    {
        private readonly IContainer _container;

        public ViewModelFactory() : this(Bootstrapper.Container)
        {
        }

        public ViewModelFactory(IContainer container)
        {
            _container = container;
        }

        public INavigatableViewModel GetViewModel(Type viewModelType)
        {
            return _container.Resolve(viewModelType) as INavigatableViewModel;
        }
    }
}
