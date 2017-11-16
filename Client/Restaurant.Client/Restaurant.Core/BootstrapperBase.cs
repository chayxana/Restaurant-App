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
using Restaurant.Core.Managers;
using Restaurant.Core.MockData;
using Restaurant.Core.Services;
using Restaurant.Core.ViewModels;
using Restaurant.Core.ViewModels.Android;
using Restaurant.MockData;

namespace Restaurant.Core
{
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
            builder.RegisterType<BasketViewModel>().As<IBasketViewModel>().SingleInstance();
            builder.RegisterType<MasterViewModel>().As<IMasterViewModel>().SingleInstance();

            builder.RegisterType<AutoMapperFacade>().As<IAutoMapperFacade>();
            builder.RegisterType<FoodDetailViewModelAdapter>().As<IFoodDetailViewModelAdapter>();
            builder.RegisterType<ViewModelFactory>().As<IViewModelFactory>();
            builder.RegisterType<NavigationService>().As<INavigationService>().SingleInstance();
            builder.RegisterType<NavigationItemAdapter>().As<INavigationItemAdapter>();

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

            RegisterTypes(builder);

            Container = builder.Build();

            return Container;
        }

        protected abstract void RegisterTypes(ContainerBuilder builder);
    }
}