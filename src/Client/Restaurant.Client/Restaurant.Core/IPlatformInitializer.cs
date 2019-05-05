using Autofac;

namespace Restaurant.Core
{
    /// <summary>
    /// Base IoC container
    /// </summary>
    public interface IPlatformInitializer
    {
        IContainer Build();
    }
}