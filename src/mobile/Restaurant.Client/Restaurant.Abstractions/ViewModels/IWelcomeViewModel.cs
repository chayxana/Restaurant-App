using System.Reactive;
using System.Windows.Input;
using ReactiveUI;

namespace Restaurant.Abstractions.ViewModels
{
    public interface IWelcomeViewModel : INavigatableViewModel
    {
        ReactiveCommand<Unit, Unit> GoLogin { get; }
        ICommand GoRegister { get; }
    }
}