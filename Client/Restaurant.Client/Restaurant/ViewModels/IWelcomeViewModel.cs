using System.Windows.Input;
using Restaurant.Abstractions;

namespace Restaurant.ViewModels
{
    public interface IWelcomeViewModel : INavigatableViewModel
    {
        ICommand GoLogin { get; }
        ICommand GoRegister { get; }
    }
}