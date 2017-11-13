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
	    private readonly IViewModelFactory _viewModelFactory;
	    private readonly IMainPageFactory _mainPageFactory;
	    private readonly IViewResolverService _viewResolverService;
	    private readonly INavigationFacade _navigationFacade;

		public NavigationService(
		    IViewModelFactory viewModelFactory,
            IMainPageFactory mainPageFactory,
            IViewResolverService viewResolverService, 
            INavigationFacade navigationFacade)
		{
		    _viewModelFactory = viewModelFactory;
		    _mainPageFactory = mainPageFactory;
		    _viewResolverService = viewResolverService;
		    _navigationFacade = navigationFacade;
		}


		public IViewFor CurrentView { get; private set; }

		public Task NavigateAsync(INavigatableViewModel viewModel)
		{
			CurrentView = _viewResolverService.ResolveView(viewModel);
			return _navigationFacade.PushAsync(CurrentView);
		}

		public Task NavigateAsync(Type viewModelType)
		{
			var vm = _viewModelFactory.GetViewModel(viewModelType);
			CurrentView = _viewResolverService.ResolveView(vm);
			return _navigationFacade.PushAsync(CurrentView);
		}

		public Task NavigateModalAsync(INavigatableViewModel viewModel)
		{
			CurrentView = _viewResolverService.ResolveView(viewModel);
			return _navigationFacade.PushModalAsync(CurrentView);
		}

		public Task NavigateModalAsync(Type viewModelType)
		{
			var vm = _viewModelFactory.GetViewModel(viewModelType);
            CurrentView = _viewResolverService.ResolveView(vm);
			return _navigationFacade.PushModalAsync(CurrentView);
		}

		public Task CloseModalAsync(bool animated)
		{
			return _navigationFacade.PopModalAsync(animated);
		}

	    public Task NavigateToMainPage(INavigatableViewModel viewModel)
	    {
	        CurrentView = _mainPageFactory.GetMainPage(viewModel);
            return _navigationFacade.NavigateToMainPage(CurrentView);
	    }

	    public Task NavigateToMainPage(Type viewModelType)
	    {
	        var vm = _viewModelFactory.GetViewModel(viewModelType);
	        CurrentView = _mainPageFactory.GetMainPage(vm);
	        return _navigationFacade.NavigateToMainPage(CurrentView);
        }
	}
}