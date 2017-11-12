using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using ReactiveUI;
using Restaurant.Abstractions.Services;
using Restaurant.ViewModels;
using Xamarin.Forms;

namespace Restaurant.Pages.iOS
{
    // ReSharper disable once InconsistentNaming
    public class MainPageiOS : TabbedPage, IViewFor<MainViewModel>
    {
        public MainPageiOS()
        {
	        var foodsViewModel = Bootstrapper.Container.Resolve<FoodsViewModel>();
	        var foodsPage = Bootstrapper.Container.Resolve<IViewResolverService>().ResolveView(foodsViewModel);


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
