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
using Restaurant.Abstractions.Repositories;
using Restaurant.Abstractions.Services;
using Restaurant.Abstractions.ViewModels;
using Restaurant.Common.Constants;
using Restaurant.Common.DataTransferObjects;
using Restaurant.Facades;
using Restaurant.Managers;
using Restaurant.Model;
using Restaurant.Pages;
using Restaurant.Repositories;
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

			builder.RegisterType<UserRepository>().As<IUserRepository>();
			builder.RegisterType<FoodRepository>().As<IFoodRepository>();
			var api = RestService.For<IRestaurantApi>(ApiConstants.ApiClientUrl);
			builder.RegisterInstance(api).As<IRestaurantApi>();

			builder.RegisterType<AutoMapperFacade>().As<IAutoMapperFacade>();
			builder.RegisterType<MainViewModel>().As<IMainViewModel>();

			var foodApi = RestService.For<IFoodsApi>("http://restaurantserverapi.azurewebsites.net/");

			builder.RegisterInstance(foodApi).As<IFoodsApi>().SingleInstance();

			return builder.Build();
		}
	}

	public class RestaurantApiTest : IRestaurantApi
	{
		public const string test_email = "test@test.ru";
		public const string test_password = "123";

		public Task<object> RegesterRaw(RegisterDto registerDto)
		{
			return Task.FromResult(new object());
		}

		public Task<object> GetValues(string accessToken)
		{
			throw new NotImplementedException();
		}

		//public Task<UserInfoDto> GetUserInfoRaw(string accessToken)
		//{
		//	return Task.FromResult(new UserInfoDto()
		//	{
		//		Picture = "https://media.licdn.com/mpr/mpr/shrinknp_100_100/AAEAAQAAAAAAAAmKAAAAJDE4OGFkYzA4LWFkMTYtNDE5YS05NDZmLTBhZGNhMzc0Y2Q5Mg.jpg",
		//		Email = "jurabek.azizkhujaev@gmail.com",
		//		IsRegistered = true,
		//		Name = "Jurabek"
		//	});

		//}

		public Task<IEnumerable<FoodDto>> GetFoods()
		{
			return Task.FromResult<IEnumerable<FoodDto>>(new List<FoodDto>
			{
				new FoodDto
				{
					Id = Guid.NewGuid(),
					Picture =  "https://images.pexels.com/photos/115095/pexels-photo-115095.jpeg?w=320&h=200&auto=compress&cs=tinysrgb",
					Name = "Bread Dish",
					Description = "Bread Dish With Potato Fries and Vegetable Dish",
					Price = 3.5m
				},
				new FoodDto
				{
					Id = Guid.NewGuid(),
					Picture = "https://images.pexels.com/photos/254884/pexels-photo-254884.jpeg?w=320&h=200&auto=compress&cs=tinysrgb",
					Name = "Directly",
					Description = "Above Shot of Food Served on Table",
					Price = 10
				},
				new FoodDto
				{
					Id = Guid.NewGuid(),
					Picture = "https://images.pexels.com/photos/132716/pexels-photo-132716.jpeg?w=320&h=200&auto=compress&cs=tinysrgb",
					Name = "Frying Pan",
					Description = "Vegetable Food Cooked on Frying Pan",
					Price = 9
				},
				new FoodDto()
				{
					Id = Guid.NewGuid(),
					Picture = "https://static.pexels.com/photos/76093/pexels-photo-76093.jpegw=320&h=200&auto=compress&cs=tinysrgb",
					Name = "Some food",
					Price = 20,
					Description = "Some food description"
				},
				new FoodDto
				{
					Id = Guid.NewGuid(),
					Picture =  "https://images.pexels.com/photos/115095/pexels-photo-115095.jpeg?w=320&h=200&auto=compress&cs=tinysrgb",
					Name = "Bread Dish",
					Description = "Bread Dish With Potato Fries and Vegetable Dish",
					Price = 3.5m
				},
				new FoodDto
				{
					Id = Guid.NewGuid(),
					Picture = "https://images.pexels.com/photos/254884/pexels-photo-254884.jpeg?w=320&h=200&auto=compress&cs=tinysrgb",
					Name = "Directly",
					Description = "Above Shot of Food Served on Table",
					Price = 10
				},
				new FoodDto
				{
					Id = Guid.NewGuid(),
					Picture = "https://images.pexels.com/photos/132716/pexels-photo-132716.jpeg?w=320&h=200&auto=compress&cs=tinysrgb",
					Name = "Frying Pan",
					Description = "Vegetable Food Cooked on Frying Pan",
					Price = 9
				},
				new FoodDto()
				{
					Id = Guid.NewGuid(),
					Picture = "https://static.pexels.com/photos/76093/pexels-photo-76093.jpegw=320&h=200&auto=compress&cs=tinysrgb",
					Name = "Some food",
					Price = 20,
					Description = "Some food description"
				}
			});
		}
	}
}
