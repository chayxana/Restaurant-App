using ReactiveUI;

namespace Restaurant.Abstractions.Factories
{
    public interface IMainPageFactory
    {
        IViewFor GetMainPage(INavigatableViewModel vm);
    }
}
