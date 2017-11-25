using System.Windows.Input;

namespace Restaurant.Abstractions.ViewModels
{
    public interface ISignInViewModel : INavigatableViewModel
    {
        string Email { get; set; }
        string Password { get; set; }
        ICommand Login { get; }
    }
}