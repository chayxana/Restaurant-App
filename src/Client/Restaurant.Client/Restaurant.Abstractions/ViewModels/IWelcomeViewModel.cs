using System.Windows.Input;

namespace Restaurant.Abstractions.ViewModels
{
    public interface IWelcomeViewModel : INavigatableViewModel
    {
        ICommand GoLogin { get; }
        ICommand GoRegister { get; }
    }
}