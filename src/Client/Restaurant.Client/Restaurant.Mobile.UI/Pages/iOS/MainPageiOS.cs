using Autofac;
using ReactiveUI;
using Restaurant.Abstractions.Factories;
using Restaurant.Core.ViewModels;
using Restaurant.Core.ViewModels.iOS;
using Xamarin.Forms;

namespace Restaurant.Mobile.UI.Pages.iOS
{
    public class MainPageiOS : TabbedPage, IViewFor<TabbedMainViewModel>
    {
        public MainPageiOS(IContainer container)
        {
            NavigationPage.SetHasNavigationBar(this, false);
            var viewFactory = container.Resolve<IViewFactory>();
            var foodsPage = viewFactory.ResolveView<FoodsViewModel>() as Page;

            Children.Add(new CustomNavigationPage(foodsPage) { Title = "Foods", Icon = "foods" });
            Children.Add(new ChatPage());
            Children.Add(new OrdersPage());
        }

        protected override void OnCurrentPageChanged()
        {
            base.OnCurrentPageChanged();
        }

        object IViewFor.ViewModel
        {
            get => ViewModel;
            set => ViewModel = (TabbedMainViewModel)value;
        }

        public TabbedMainViewModel ViewModel { get; set; }
    }
}