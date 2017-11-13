using Autofac;
using ReactiveUI;
using Refit;
using Restaurant.Abstractions.Adapters;
using Restaurant.Abstractions.Api;
using Restaurant.Abstractions.Facades;
using Restaurant.Abstractions.Factories;
using Restaurant.Abstractions.Managers;
using Restaurant.Abstractions.Services;
using Restaurant.Abstractions.ViewModels;
using Restaurant.Core;
using Restaurant.Core.Adapters;
using Restaurant.Core.Facades;
using Restaurant.Core.Factories;
using Restaurant.Core.Managers;
using Restaurant.Core.MockData;
using Restaurant.Core.Services;
using Restaurant.Core.ViewModels;
using Restaurant.Mobile.UI.Facades;
using Restaurant.Mobile.UI.Factories;
using Restaurant.Mobile.UI.Pages;
using Restaurant.Mobile.UI.Pages.Android;
using Restaurant.Mobile.UI.Pages.iOS;
using Restaurant.Mobile.UI.Pages.Welcome;
using Restaurant.Mobile.UI.Services;
using Restaurant.MockData;
using Restaurant.ViewModels;
using Xamarin.Forms;

namespace Restaurant.Mobile.UI
{
	public class Bootstrapper : BootstrapperBase
	{
		//protected abstract void RegisterTypes(ContainerBuilder builder);
		public static IContainer Container { get; private set; }

		public static bool MockData = true;
        

	    protected override void RegisterTypes(ContainerBuilder builder)
	    {
            //foreach (var type in typeof(App).GetTypeInfo().Assembly.ExportedTypes)
            //{
            //	if (type.IsAssignableTo<IViewFor>() && !type.GetTypeInfo().IsAbstract)
            //	{
            //	}
            //}

            //builder.RegisterAssemblyTypes().AsImplementedInterfaces()
            //	.Except<IViewFor>()
            //	.Except<IFoodsApi>();

            builder.RegisterType<NavigationFacade>().As<INavigationFacade>();
            builder.RegisterType<NavigationService>().As<INavigationService>().SingleInstance();

            builder.RegisterType<WelcomeStartPage>().As<IViewFor<WelcomeViewModel>>();
            builder.RegisterType<SignInPage>().As<IViewFor<SignInViewModel>>();
            builder.RegisterType<SignUpPage>().As<IViewFor<SignUpViewModel>>();
            //builder.RegisterType<MainPage>().As<IViewFor<MainViewModel>>();
            builder.RegisterType<FoodsPage>().As<IViewFor<FoodsViewModel>>();
            builder.RegisterType<FoodDetailPage>().As<IViewFor<FoodDetailViewModel>>();
            builder.RegisterType<BasketPage>().As<IViewFor<BasketViewModel>>();
            builder.RegisterType<MainPageAndroid>().Named<IViewFor<MainViewModel>>(Device.Android);
            builder.RegisterType<MainPageiOS>().Named<IViewFor<MainViewModel>>(Device.iOS);

            builder.RegisterType<WelcomeViewModel>().As<IWelcomeViewModel>();
            builder.RegisterType<SignInViewModel>().As<ISignInViewModel>();
            builder.RegisterType<SignUpViewModel>().As<ISignUpViewModel>();
            builder.RegisterType<FoodsViewModel>().AsSelf();
            builder.RegisterType<FoodDetailViewModel>().AsSelf();

            builder.RegisterType<AutoMapperFacade>().As<IAutoMapperFacade>();
            builder.RegisterType<MainViewModel>().As<IMainViewModel>();
            builder.RegisterType<BasketViewModel>().As<IBasketViewModel>().SingleInstance();
            builder.RegisterType<FoodDetailViewModelAdapter>().As<IFoodDetailViewModelAdapter>();
            builder.RegisterType<ViewResolverService>().As<IViewResolverService>();
            builder.RegisterType<MainPageFactory>().As<IMainPageFactory>();
            builder.RegisterType<ViewModelFactory>().As<IViewModelFactory>();

            IFoodsApi foodApi;
            if (MockData)
            {
                foodApi = new MockFoodsApi();
                builder.RegisterType<MockAuthenticationManager>().As<IAuthenticationManager>();

            }
            else
            {
                foodApi = RestService.For<IFoodsApi>("http://restaurantserverapi.azurewebsites.net/");
                builder.RegisterType<AuthenticationManager>().As<IAuthenticationManager>();
            }


            builder.RegisterInstance(foodApi).As<IFoodsApi>().SingleInstance();
        }
	}
}