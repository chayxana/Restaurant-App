using ReactiveUI;

namespace Restaurant.Abstractions.Factories
{
    public interface IViewFactory
    {
        IViewFor ResolveView<TNavigatableViewModel>() where TNavigatableViewModel : INavigatableViewModel;

        IViewFor ResolveView(INavigatableViewModel vm);

        IViewFor ResolveView(INavigatableViewModel vm, string name);
    }
}