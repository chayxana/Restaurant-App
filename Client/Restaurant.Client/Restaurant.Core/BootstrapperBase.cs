using System.Diagnostics.CodeAnalysis;
using Autofac;
using Refit;
using Restaurant.Abstractions.Adapters;
using Restaurant.Abstractions.Api;
using Restaurant.Abstractions.Facades;
using Restaurant.Abstractions.Factories;
using Restaurant.Abstractions.Managers;
using Restaurant.Abstractions.Services;
using Restaurant.Abstractions.ViewModels;
using Restaurant.Core.Adapters;
using Restaurant.Core.Facades;
using Restaurant.Core.Factories;
using Restaurant.Core.MockData;
using Restaurant.Core.Services;
using Restaurant.Core.ViewModels;
using Restaurant.Core.ViewModels.Android;

namespace Restaurant.Core
{
    [ExcludeFromCodeCoverage]
    public abstract class BootstrapperBase
    {
        public static bool MockData = true;
        public static IContainer Container { get; private set; }

        public IContainer Build()
        {
            var builder = new ContainerBuilder();

            builder.RegisterType<WelcomeViewModel>().As<IWelcomeViewModel>();
            builder.RegisterType<SignInViewModel>().As<ISignInViewModel>();
            builder.RegisterType<SignUpViewModel>().As<ISignUpViewModel>();
            builder.RegisterType<FoodsViewModel>().AsSelf();
            builder.RegisterType<FoodDetailViewModel>().AsSelf();
            builder.RegisterType<OrdersViewModel>().AsSelf();
            builder.RegisterType<BasketViewModel>().As<IBasketViewModel>().SingleInstance();
            builder.RegisterType<MasterViewModel>().As<IMasterViewModel>().SingleInstance();

            builder.RegisterType<AutoMapperFacade>().As<IAutoMapperFacade>();
            builder.RegisterType<FoodDetailViewModelAdapter>().As<IFoodDetailViewModelAdapter>();
            builder.RegisterType<ViewModelFactory>().As<IViewModelFactory>();
            builder.RegisterType<NavigationService>().As<INavigationService>().SingleInstance();
            builder.RegisterType<NavigationItemAdapter>().As<INavigationItemAdapter>();
            builder.RegisterType<OrderDtoAdapter>().As<IOrderDtoAdapter>();

            IFoodsApi foodApi;
            IOrdersApi ordersApi;
            if (MockData)
            {
                foodApi = new MockFoodsApi();
                ordersApi = new MockOrdersApi();
                builder.RegisterType<MockAuthenticationManager>().As<IAuthenticationManager>();
            }
            else
            {
                foodApi = RestService.For<IFoodsApi>("http://restaurantserverapi.azurewebsites.net/");
                ordersApi = RestService.For<IOrdersApi>("http://restaurantserverapi.azurewebsites.net/");
            }

            builder.RegisterInstance(foodApi).As<IFoodsApi>().SingleInstance();
            builder.RegisterInstance(ordersApi).As<IOrdersApi>().SingleInstance();


            RegisterTypes(builder);

            Container = builder.Build();

            return Container;
        }

        protected abstract void RegisterTypes(ContainerBuilder builder);
    }
}