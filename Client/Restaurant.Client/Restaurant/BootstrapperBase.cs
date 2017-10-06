using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading.Tasks;
using Autofac;
using ReactiveUI;
using Refit;
using Restaurant.Abstractions.Api;
using Restaurant.Abstractions.Facades;
using Restaurant.Abstractions.Managers;
using Restaurant.Abstractions.Services;
using Restaurant.Abstractions.ViewModels;
using Restaurant.Common.Constants;
using Restaurant.Common.DataTransferObjects;
using Restaurant.Facades;
using Restaurant.Managers;
using Restaurant.Pages;
using Restaurant.Services;
using Restaurant.ViewModels;
using Restaurant.Pages.Welcome;

namespace Restaurant
{
	public class BootstrapperBase
	{
		//protected abstract void RegisterTypes(ContainerBuilder builder);

		public IContainer Build()
		{
			foreach (var type in typeof(App).GetTypeInfo().Assembly.ExportedTypes)
			{
				if (type.IsAssignableTo<IViewFor>() && !type.GetTypeInfo().IsAbstract)
				{
				}
			}

			var builder = new ContainerBuilder();
			//RegisterTypes(builder);

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

			builder.RegisterType<WelcomeViewModel>().As<IWelcomeViewModel>();
			builder.RegisterType<SignInViewModel>().As<ISignInViewModel>();
			builder.RegisterType<SignUpViewModel>().As<ISignUpViewModel>();
			builder.RegisterType<FoodsViewModel>().AsSelf();
			builder.RegisterType<FoodDetailViewModel>().AsSelf();
            
			builder.RegisterType<AutoMapperFacade>().As<IAutoMapperFacade>();
			builder.RegisterType<MainViewModel>().As<IMainViewModel>();
		    builder.RegisterType<BasketViewModel>().As<IBasketViewModel>().SingleInstance();

			var foodApi = RestService.For<IFoodsApi>("http://restaurantserverapi.azurewebsites.net/");

			builder.RegisterInstance(foodApi).As<IFoodsApi>().SingleInstance();

			return builder.Build();
		}
	}

	
}
