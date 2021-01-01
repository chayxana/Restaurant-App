using System.Reactive;
using System.Windows.Input;
using ReactiveUI;

namespace Restaurant.Abstractions.ViewModels
{
    public interface IWelcomeViewModel : INavigatableViewModel
    {
        ICommand GoLogin { get; }
        ICommand GoRegister { get; }
    }
}