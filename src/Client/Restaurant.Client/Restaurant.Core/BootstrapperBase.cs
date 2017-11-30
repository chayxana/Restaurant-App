using System.Diagnostics.CodeAnalysis;
using System.Net;
using Autofac;
using Refit;
using Restaurant.Abstractions.Adapters;
using Restaurant.Abstractions.Api;
using Restaurant.Abstractions.Facades;
using Restaurant.Abstractions.Factories;
using Restaurant.Abstractions.Providers;
using Restaurant.Abstractions.Services;
using Restaurant.Abstractions.ViewModels;
using Restaurant.Common.Constants;
using Restaurant.Core.Adapters;
using Restaurant.Core.Facades;
using Restaurant.Core.Factories;
using Restaurant.Core.MockData;
using Restaurant.Core.Providers;
using Restaurant.Core.Services;
using Restaurant.Core.ViewModels;
using Restaurant.Core.ViewModels.Android;

namespace Restaurant.Core
{
    [ExcludeFromCodeCoverage]
    public abstract class BootstrapperBase
    {
        public static bool MockData = false;
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
            builder.RegisterType<FoodDetailViewModelFactory>().As<IFoodDetailViewModelFactory>();
            builder.RegisterType<ViewModelFactory>().As<IViewModelFactory>();
            builder.RegisterType<NavigationService>().As<INavigationService>().SingleInstance();
            builder.RegisterType<NavigationItemAdapter>().As<INavigationItemAdapter>();
            builder.RegisterType<OrderDtoAdapter>().As<IOrderDtoAdapter>();
            builder.RegisterType<IdentityModelTokenProvider>().As<ITokenProvider>();
            
            if (MockData)
            {
                builder.RegisterType<MockOrdersApi>().As<IOrdersApi>();
                builder.RegisterType<MockFoodsApi>().As<IFoodsApi>();
                builder.RegisterType<MockAuthenticationProvider>().As<IAuthenticationProvider>();
            }
            else
            {
                var foodApi = RestService.For<IFoodsApi>(ApiConstants.ApiClientUrl);
                var ordersApi = RestService.For<IOrdersApi>(ApiConstants.ApiClientUrl);
                var accountApi = RestService.For<IAccountApi>(ApiConstants.ApiClientUrl);

                builder.RegisterInstance(accountApi).As<IAccountApi>().SingleInstance();
                builder.RegisterInstance(foodApi).As<IFoodsApi>().SingleInstance();
                builder.RegisterInstance(ordersApi).As<IOrdersApi>().SingleInstance();
                builder.RegisterType<AuthenticationProvider>().As<IAuthenticationProvider>();
            }

            RegisterSelf(builder);

            RegisterTypes(builder);

            Container = builder.Build();

            return Container;
        }

        protected abstract void RegisterTypes(ContainerBuilder builder);
        
        private static void RegisterSelf(ContainerBuilder builder)
        {
            IContainer container = null;
            builder.Register(c => container).AsSelf();
            builder.RegisterBuildCallback(c => container = c);
        }
    }
}