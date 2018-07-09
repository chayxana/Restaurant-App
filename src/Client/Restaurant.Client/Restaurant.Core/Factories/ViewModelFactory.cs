using Autofac;
using Restaurant.Abstractions;
using Restaurant.Abstractions.Facades;
using Restaurant.Abstractions.Factories;
using System;
using System.Diagnostics.CodeAnalysis;

namespace Restaurant.Core.Factories
{
    [ExcludeFromCodeCoverage]
    public class ViewModelFactory : IViewModelFactory
    {
        private readonly IContainer _container;
        private readonly IDiagnosticsFacade _diagnosticsFacade;

        public ViewModelFactory(
            IContainer container,
            IDiagnosticsFacade diagnosticsFacade)
        {
            _container = container;
            _diagnosticsFacade = diagnosticsFacade;
        }

        public INavigatableViewModel GetViewModel(Type viewModelType)
        {
            try
            {
                return _container.Resolve(viewModelType) as INavigatableViewModel;
            }
            catch (Exception e)
            {
                _diagnosticsFacade.TrackError(e);
            }

            return null;
        }

        public INavigatableViewModel GetMainViewModel(Type viewModelType, string platform)
        {
            try
            {
                return _container.ResolveNamed(platform, viewModelType) as INavigatableViewModel;
            }
            catch (Exception ex)
            {
                _diagnosticsFacade.TrackError(ex);
            }

            return null;
        }
    }
}