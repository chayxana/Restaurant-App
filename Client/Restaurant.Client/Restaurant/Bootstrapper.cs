using Autofac;
using JetBrains.Annotations;
using ReactiveUI;
using Restaurant.Abstractions.Facades;
using Restaurant.Abstractions.Managers;
using Restaurant.Abstractions.Services;
using Restaurant.Abstractions.ViewModels;
using Restaurant.Facades;
using Restaurant.Managers;
using Restaurant.Pages;
using Restaurant.Services;
using Restaurant.ViewModels;

namespace Restaurant
{
    [UsedImplicitly]
    public class Bootstrapper
    {
        public IContainer Build()
        {
            var builder = new ContainerBuilder();
            builder.RegisterType<NavigationFacade>().As<INavigationFacade>();
            builder.RegisterType<NavigationService>().As<INavigationService>().SingleInstance();
            builder.RegisterType<ThemeManager>().As<IThemeManager>().SingleInstance();

            builder.RegisterType<WelcomeStartPage>().As<IViewFor<WelcomeViewModel>>();
            builder.RegisterType<SignInPage>().As<IViewFor<SignInViewModel>>();
            builder.RegisterType<SignUpPage>().As<IViewFor<SignUpViewModel>>();
            builder.RegisterType<MainPage>().As<IViewFor<MainViewModel>>();
            builder.RegisterType<FoodsPage>().As<IViewFor<FoodsViewModel>>();


            builder.RegisterType<WelcomeViewModel>().As<IWelcomeViewModel>();
            builder.RegisterType<SignInViewModel>().As<ISignInViewModel>();
            builder.RegisterType<SignUpViewModel>().As<ISignUpViewModel>();

            return builder.Build();
        }
    }
}
