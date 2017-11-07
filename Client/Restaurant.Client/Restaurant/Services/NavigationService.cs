using System;
using System.Threading.Tasks;
using Autofac;
using JetBrains.Annotations;
using ReactiveUI;
using Restaurant.Abstractions;
using Restaurant.Abstractions.Facades;
using Restaurant.Abstractions.Services;
using Xamarin.Forms;

namespace Restaurant.Services
{
	[UsedImplicitly]
	public class NavigationService : INavigationService
	{
		private readonly IContainer _container;
		private readonly INavigationFacade _navigationFacade;

		public NavigationService(INavigationFacade navigationFacade)
			: this(Bootstrapper.Container, navigationFacade)
		{
		}

		public NavigationService(IContainer container, INavigationFacade navigationFacade)
		{
			_container = container;
			_navigationFacade = navigationFacade;
		}


		public IViewFor CurrentPage { get; private set; }

		public Task NavigateAsync(INavigatableViewModel viewModel)
		{
			CurrentPage = ResolveView(viewModel);
			return _navigationFacade.PushAsync(CurrentPage);
		}

		public Task NavigateAsync(Type viewModelType)
		{
			var vm = _container.Resolve(viewModelType);
			CurrentPage = ResolveView(vm as INavigatableViewModel);
			return _navigationFacade.PushAsync(CurrentPage);
		}

		public Task NavigateModalAsync(INavigatableViewModel viewModel)
		{
			CurrentPage = ResolveView(viewModel);
			return _navigationFacade.PushModalAsync(CurrentPage);
		}

		public Task NavigateModalAsync(Type viewModelType)
		{
			var vm = _container.Resolve(viewModelType);
			CurrentPage = ResolveView(vm as INavigatableViewModel);
			return _navigationFacade.PushModalAsync(CurrentPage);
		}

		public Task PopModalAsync(bool animated)
		{
			return _navigationFacade.PopModalAsync(animated);
		}

	    public Task NavigateToMainPage(INavigatableViewModel viewModel)
	    {
	        CurrentPage = ResolveView(viewModel);
	        return _navigationFacade.NavigateToMainPage(CurrentPage);
	    }

	    public Task NavigateToMainPage(Type viewModelType)
	    {
	        var vm = _container.Resolve(viewModelType);
	        CurrentPage = ResolveView(vm as INavigatableViewModel);
	        return _navigationFacade.NavigateToMainPage(CurrentPage);
        }

	    public IViewFor ResolveView(INavigatableViewModel vm)
		{
			var viewType = typeof(IViewFor<>).MakeGenericType(vm.GetType());
			var view = _container.Resolve(viewType) as Page;

			if (!(view is IViewFor ret))
				throw new Exception(
					$"Resolve service type '{viewType.FullName}' does not implement '{typeof(IViewFor).FullName}'.");

			view.Title = vm.Title;
			ret.ViewModel = vm;
			return ret;
		}
	}
}