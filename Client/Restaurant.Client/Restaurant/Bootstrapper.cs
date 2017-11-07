using System;
using System.Collections.Generic;
using System.Linq;
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
using Restaurant.Common.DataTransferObjects;
using Restaurant.Facades;
using Restaurant.Managers;
using Restaurant.MockData;
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

		public static bool MockData = true;

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

			Container = builder.Build();

			return Container;
		}
	}

	public static class Data
	{
		public static IEnumerable<CategoryDto> Categories = new List<CategoryDto>
		{
			new CategoryDto
			{
				Color = "red",
				Name = "Foods"
			},

			new CategoryDto
			{
				Color = "blue",
				Name = "Drinks"
			},
			new CategoryDto
			{
				Color = "brown",
				Name = "Cakes"
			}
		};

		public static IEnumerable<FoodDto> Foods = new List<FoodDto>
		{
			new FoodDto
			{
				Picture = "https://i.imgur.com/vAVUGtZm.jpg",
				CategoryDto = Categories.FirstOrDefault(x => x.Name == "Foods"),
				Name = "Hamburger",
				Description = "A hamburger, beefburger or burger is a sandwich consisting of one or more cooked patties of ground meat, usually beef, placed inside a sliced bread roll or bun. The patty may be pan fried, barbecued, or flame broiled. Hamburgers are often served with cheese, lettuce, tomato, bacon, onion, pickles, or chiles; condiments such as mustard, mayonnaise, ketchup, relish, or \"special sauce\"; and are frequently placed on sesame seed buns. A hamburger topped with cheese is called a cheeseburger.",
				Price = 50,
				Id = Guid.NewGuid()
			},
			new FoodDto
			{
				Picture	= "https://i.imgur.com/IBqp2Bbm.jpg",
				CategoryDto = Categories.FirstOrDefault(x => x.Name == "Foods"),
				Name = "Steak",
				Description = "A steak (/ˈsteɪk/) is a meat generally sliced across the muscle fibers, potentially including a bone. Exceptions, in which the meat is sliced parallel to the fibers, include the skirt steak that is cut from the plate, the flank steak that is cut from the abdominal muscles, and the Silverfinger steak that is cut from the loin and includes three rib bones. When the word \"steak\" is used without qualification, it generally refers to a beefsteak. In a larger sense, there are also fish steaks, ground meat steaks, pork steak and many more varieties of steaks.",
				Price = 65,
				Id = Guid.NewGuid()
			},
			new FoodDto
			{
				Picture = "https://i.imgur.com/amGnES4m.jpg",
				Name = "Sushi",
				Price = 40,
				CategoryDto = Categories.FirstOrDefault(x => x.Name == "Foods"),
				Description = "Sushi (すし, 寿司, 鮨) is the Japanese preparation and serving of specially prepared vinegared rice (鮨飯 sushi-meshi) combined with varied ingredients (ネタ neta) such as chiefly seafood (often uncooked), vegetables, and occasionally tropical fruits. Styles of sushi and its presentation vary widely, but the key ingredient is sushi rice, also referred to as shari (しゃり), or sumeshi (酢飯)."
			}
		};
	}
}