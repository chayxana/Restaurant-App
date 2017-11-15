using ReactiveUI;

namespace Restaurant.Abstractions.Factories
{
    public interface IViewFactory
    {
        IViewFor ResolveView(INavigatableViewModel vm);

        IViewFor ResolveView(INavigatableViewModel vm, string name);
    }
}
