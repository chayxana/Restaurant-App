using Autofac;

namespace Restaurant.Core
{
    public interface IPlatformInitializer
    {
        IContainer Build();
    }
}