using Autofac;
using ReactiveUI;
using Restaurant.Abstractions.Factories;
using Restaurant.Core;
using Restaurant.Core.ViewModels;
using Restaurant.Core.ViewModels.iOS;
using Xamarin.Forms;

namespace Restaurant.Mobile.UI.Pages.iOS
{
    public class MainPageiOS : TabbedPage, IViewFor<TabbedMainViewModel>
    {
        public MainPageiOS()
        {
            NavigationPage.SetHasNavigationBar(this, false);
            var foodsViewModel = BootstrapperBase.Container.Resolve<FoodsViewModel>();
            var foodsPage = BootstrapperBase.Container.Resolve<IViewFactory>().ResolveView(foodsViewModel);


            Children.Add(new NavigationPage(foodsPage as Page) {Title = "Foods", Icon = "restaurant"});
            Children.Add(new ChatPage());
            Children.Add(new OrdersPage());
        }

        object IViewFor.ViewModel
        {
            get => ViewModel;
            set => ViewModel = (TabbedMainViewModel) value;
        }

        public TabbedMainViewModel ViewModel { get; set; }
    }
}