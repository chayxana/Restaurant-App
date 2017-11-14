using Autofac;
using ReactiveUI;
using Restaurant.Abstractions.Services;
using Restaurant.Core;
using Restaurant.Core.ViewModels;
using Xamarin.Forms;

namespace Restaurant.Mobile.UI.Pages.Android
{
    public class MainPageAndroid : MasterDetailPage, IViewFor<MainViewModel>
    {
	    public MainPageAndroid()
	    {
		    Master = new MenuPage();
		    var foodsViewModel = BootstrapperBase.Container.Resolve<FoodsViewModel>();
			var foodsPage = BootstrapperBase.Container.Resolve<IViewResolverService>().ResolveView(foodsViewModel);
			Detail = new NavigationPage(foodsPage as Page);
	    }

	    object IViewFor.ViewModel
	    {
		    get { return ViewModel; }
		    set { ViewModel = (MainViewModel)value; }
	    }

	    public MainViewModel ViewModel { get; set; }
    }
}
