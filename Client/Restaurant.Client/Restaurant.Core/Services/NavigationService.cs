using System;
using System.Threading.Tasks;
using JetBrains.Annotations;
using ReactiveUI;
using Restaurant.Abstractions;
using Restaurant.Abstractions.Facades;
using Restaurant.Abstractions.Factories;
using Restaurant.Abstractions.Services;

namespace Restaurant.Core.Services
{
    [UsedImplicitly]
    public class NavigationService : INavigationService
    {
        private readonly INavigationFacade _navigationFacade;
        private readonly IPlatformFacade _platformFacade;
        private readonly IViewFactory _viewFactory;
        private readonly IViewModelFactory _viewModelFactory;

        public NavigationService(
            IViewModelFactory viewModelFactory,
            IViewFactory viewFactory,
            INavigationFacade navigationFacade,
            IPlatformFacade platformFacade)
        {
            _viewModelFactory = viewModelFactory;
            _viewFactory = viewFactory;
            _navigationFacade = navigationFacade;
            _platformFacade = platformFacade;
        }


        public IViewFor CurrentView { get; private set; }

        public Task NavigateAsync(INavigatableViewModel viewModel)
        {
            CurrentView = _viewFactory.ResolveView(viewModel);
            return _navigationFacade.PushAsync(CurrentView);
        }

        public Task NavigateAsync(Type viewModelType)
        {
            var vm = _viewModelFactory.GetViewModel(viewModelType);
            CurrentView = _viewFactory.ResolveView(vm);
            return _navigationFacade.PushAsync(CurrentView);
        }

        public Task NavigateModalAsync(INavigatableViewModel viewModel)
        {
            CurrentView = _viewFactory.ResolveView(viewModel);
            return _navigationFacade.PushModalAsync(CurrentView);
        }

        public Task NavigateModalAsync(Type viewModelType)
        {
            var vm = _viewModelFactory.GetViewModel(viewModelType);
            CurrentView = _viewFactory.ResolveView(vm);
            return _navigationFacade.PushModalAsync(CurrentView);
        }

        public Task CloseModalAsync(bool animated)
        {
            return _navigationFacade.PopModalAsync(animated);
        }

        public Task NavigateToMainPage(INavigatableViewModel viewModel)
        {
            CurrentView = _viewFactory.ResolveView(viewModel);
            return _navigationFacade.NavigateToMainPage(CurrentView);
        }

        public Task NavigateToMainPage(Type viewModelType)
        {
            var vm = _viewModelFactory.GetMainViewModel(viewModelType, _platformFacade.RuntimePlatform);
            CurrentView = _viewFactory.ResolveView(vm);
            return _navigationFacade.NavigateToMainPage(CurrentView);
        }

        public Task NavigateToMainPageContent(INavigatableViewModel viewModel)
        {
            CurrentView = _viewFactory.ResolveView(viewModel);
            return _navigationFacade.NavigateToMainPageContent(CurrentView);
        }
    }
}