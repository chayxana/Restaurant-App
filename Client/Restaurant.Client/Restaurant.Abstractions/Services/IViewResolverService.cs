using ReactiveUI;

namespace Restaurant.Abstractions.Services
{
    public interface IViewResolverService
    {
        IViewFor ResolveView(INavigatableViewModel vm);

        IViewFor ResolveView(INavigatableViewModel vm, string name);
    }
}
