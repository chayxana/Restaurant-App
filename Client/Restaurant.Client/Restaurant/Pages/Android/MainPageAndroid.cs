using Autofac;
using ReactiveUI;
using Restaurant.Abstractions.Services;
using Restaurant.Abstractions.ViewModels;
using Restaurant.ViewModels;
using Xamarin.Forms;

namespace Restaurant.Pages.Android
{
    public class MainPageAndroid : MasterDetailPage, IViewFor<MainViewModel>
    {
	    public MainPageAndroid()
	    {
		    Master = new MenuPage();
		    var foodsViewModel = Bootstrapper.Container.Resolve<FoodsViewModel>();
			var foodsPage = Bootstrapper.Container.Resolve<IViewResolverService>().ResolveView(foodsViewModel);
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
