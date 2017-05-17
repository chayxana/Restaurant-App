using System.Reflection;
using Autofac;
using JetBrains.Annotations;
using ReactiveUI;
using Restaurant.Abstractions.Services;
using Restaurant.Pages;
using Restaurant.Services;
using Restaurant.ViewModels;

namespace Restaurant
{
    [UsedImplicitly]
    public class Bootstrapper
    {
        public static IContainer Container { get; private set; }

        public static IContainer Build()
        {
            var builder = new ContainerBuilder();
            builder.RegisterType<NavigationService>().As<INavigationService>().SingleInstance();
            builder.RegisterType<WelcomeStartPage>().As<IViewFor<WelcomeViewModel>>();
            Container = builder.Build();
            return Container;
        }
    }
}
