using Autofac;
using Restaurant.Abstractions.Services;
using Restaurant.Services;

namespace Restaurant
{
    public class Bootstrapper
    {
        public static IContainer Container { get; private set; }

        public static void Build()
        {
            var builder = new ContainerBuilder();

            builder.RegisterType<INavigationService>().As<NavigationService>().SingleInstance();

            Container = builder.Build();
        }
    }
}
