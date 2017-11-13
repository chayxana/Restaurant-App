using Autofac;
using ReactiveUI;
using Restaurant.Abstractions.Services;
using Restaurant.Core;
using Restaurant.Core.ViewModels;
using Xamarin.Forms;

namespace Restaurant.Mobile.UI.Pages.iOS
{
    // ReSharper disable once InconsistentNaming
    public class MainPageiOS : TabbedPage, IViewFor<MainViewModel>
    {
        public MainPageiOS()
        {
	        var foodsViewModel = BootstrapperBase.Container.Resolve<FoodsViewModel>();
	        var foodsPage = BootstrapperBase.Container.Resolve<IViewResolverService>().ResolveView(foodsViewModel);


			Children.Add(new NavigationPage(foodsPage as Page) { Title = "Foods", Icon = "ic_restaurant_menu_black.png" });
	        Children.Add(new ChatPage());
			Children.Add(new OrdersPage());
		}

		object IViewFor.ViewModel
	    {
		    get { return ViewModel; }
		    set { ViewModel = (MainViewModel)value; }
	    }

	    public MainViewModel ViewModel { get; set; }
	}
}
