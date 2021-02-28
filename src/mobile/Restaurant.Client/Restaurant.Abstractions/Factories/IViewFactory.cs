using ReactiveUI;

namespace Restaurant.Abstractions.Factories
{
    public interface IViewFactory
    {
        IViewFor ResolveView<TNavigatableViewModel>() where TNavigatableViewModel : IRouteViewModel;

        IViewFor ResolveView(IRouteViewModel vm);

        IViewFor ResolveView(IRouteViewModel vm, string name);
    }
}