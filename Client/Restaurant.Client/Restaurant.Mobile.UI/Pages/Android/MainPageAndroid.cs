using Autofac;
using ReactiveUI;
using Restaurant.Abstractions.Factories;
using Restaurant.Abstractions.ViewModels;
using Restaurant.Core;
using Restaurant.Core.ViewModels;
using Restaurant.Core.ViewModels.Android;
using Xamarin.Forms;

namespace Restaurant.Mobile.UI.Pages.Android
{
    public class MainPageAndroid : MasterDetailPage, IViewFor<MasterDetailedMainViewModel>
    {
        public MainPageAndroid()
        {
            var container = BootstrapperBase.Container;
            var viewFactory = container.Resolve<IViewFactory>();

            var masterViewModel = container.Resolve<IMasterViewModel>();
            var masterPage = viewFactory.ResolveView(masterViewModel);

            var foodsViewModel = container.Resolve<FoodsViewModel>();
            var foodsPage = viewFactory.ResolveView(foodsViewModel);

            Master = masterPage as Page;
            Detail = new NavigationPage(foodsPage as Page);
        }

        object IViewFor.ViewModel
        {
            get => ViewModel;
            set => ViewModel = (MasterDetailedMainViewModel) value;
        }

        public MasterDetailedMainViewModel ViewModel { get; set; }
    }
}