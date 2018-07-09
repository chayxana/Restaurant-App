using Autofac;
using Plugin.Settings;
using Plugin.Settings.Abstractions;
using ReactiveUI;
using Restaurant.Abstractions.Facades;
using Restaurant.Abstractions.Factories;
using Restaurant.Abstractions.Providers;
using Restaurant.Abstractions.ViewModels;
using Restaurant.Core;
using Restaurant.Core.ViewModels;
using Restaurant.Core.ViewModels.Android;
using Restaurant.Core.ViewModels.iOS;
using Restaurant.Mobile.UI.Facades;
using Restaurant.Mobile.UI.Factories;
using Restaurant.Mobile.UI.Pages;
using Restaurant.Mobile.UI.Pages.Android;
using Restaurant.Mobile.UI.Pages.iOS;
using Restaurant.Mobile.UI.Pages.Welcome;
using Restaurant.Mobile.UI.Providers;
using Xamarin.Forms;

namespace Restaurant.Mobile.UI
{
    public abstract class MobilePlatformInitializer : CorePlatformInitializer
    {
        protected override void RegisterTypes(ContainerBuilder builder)
        {
            builder.RegisterType<WelcomeStartPage>().As<IViewFor<WelcomeViewModel>>();
            builder.RegisterType<SignInPage>().As<IViewFor<SignInViewModel>>();
            builder.RegisterType<SignUpPage>().As<IViewFor<SignUpViewModel>>();
            builder.RegisterType<FoodsPage>().As<IViewFor<FoodsViewModel>>();
            builder.RegisterType<FoodDetailPage>().As<IViewFor<FoodDetailViewModel>>();
            builder.RegisterType<BasketPage>().As<IViewFor<BasketViewModel>>();
            builder.RegisterType<MainPageAndroid>().As<IViewFor<MasterDetailedMainViewModel>>();
            builder.RegisterType<MainPageiOS>().As<IViewFor<TabbedMainViewModel>>();
            builder.RegisterType<MenuPage>().As<IViewFor<MasterViewModel>>();
            builder.RegisterType<OrdersPage>().As<IViewFor<OrdersViewModel>>();

            builder.RegisterType<MasterDetailedMainViewModel>().Named<IMainViewModel>(Device.Android);
            builder.RegisterType<TabbedMainViewModel>().Named<IMainViewModel>(Device.iOS);
            builder.RegisterType<NavigationFacade>().As<INavigationFacade>();
            builder.RegisterType<PlatformFacade>().As<IPlatformFacade>();
            builder.RegisterType<ViewFactory>().As<IViewFactory>();
            builder.RegisterType<DiagnosticsFacade>().As<IDiagnosticsFacade>();
	        builder.RegisterType<SettingsProvider>().As<ISettingsProvider>().SingleInstance();
	        builder.RegisterInstance(CrossSettings.Current).As<ISettings>().SingleInstance();
        }
    }
}