using Autofac;
using ReactiveUI;
using Refit;
using Restaurant.Abstractions.Adapters;
using Restaurant.Abstractions.Api;
using Restaurant.Abstractions.Facades;
using Restaurant.Abstractions.Managers;
using Restaurant.Abstractions.Services;
using Restaurant.Abstractions.ViewModels;
using Restaurant.Adapters;
using Restaurant.Facades;
using Restaurant.Managers;
using Restaurant.Pages;
using Restaurant.Pages.Welcome;
using Restaurant.Services;
using Restaurant.ViewModels;

namespace Restaurant
{
	public class Bootstrapper
	{
		//protected abstract void RegisterTypes(ContainerBuilder builder);
		public static IContainer Container { get; private set; }

		public IContainer Build()
		{
			var builder = new ContainerBuilder();

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
			builder.RegisterType<AuthenticationManager>().As<IAuthenticationManager>();
			builder.RegisterType<ThemeManager>().As<IThemeManager>().SingleInstance();

			builder.RegisterType<WelcomeStartPage>().As<IViewFor<WelcomeViewModel>>();
			builder.RegisterType<SignInPage>().As<IViewFor<SignInViewModel>>();
			builder.RegisterType<SignUpPage>().As<IViewFor<SignUpViewModel>>();
			builder.RegisterType<MainPage>().As<IViewFor<MainViewModel>>();
			builder.RegisterType<FoodsPage>().As<IViewFor<FoodsViewModel>>();
			builder.RegisterType<MasterViewModel>().As<IMasterViewModel>();
			builder.RegisterType<FoodDetailPage>().As<IViewFor<FoodDetailViewModel>>();
			builder.RegisterType<BasketPage>().As<IViewFor<BasketViewModel>>();

			builder.RegisterType<WelcomeViewModel>().As<IWelcomeViewModel>();
			builder.RegisterType<SignInViewModel>().As<ISignInViewModel>();
			builder.RegisterType<SignUpViewModel>().As<ISignUpViewModel>();
			builder.RegisterType<FoodsViewModel>().AsSelf();
			builder.RegisterType<FoodDetailViewModel>().AsSelf();

			builder.RegisterType<AutoMapperFacade>().As<IAutoMapperFacade>();
			builder.RegisterType<MainViewModel>().As<IMainViewModel>();
			builder.RegisterType<BasketViewModel>().As<IBasketViewModel>().SingleInstance();
			builder.RegisterType<FoodDetailViewModelAdapter>().As<IFoodDetailViewModelAdapter>();

			var foodApi = RestService.For<IFoodsApi>("http://restaurantserverapi.azurewebsites.net/");

			builder.RegisterInstance(foodApi).As<IFoodsApi>().SingleInstance();

			Container = builder.Build();

			return Container;
		}
	}
}