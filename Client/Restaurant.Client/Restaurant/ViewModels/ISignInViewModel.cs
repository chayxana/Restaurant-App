using System.Windows.Input;
using Restaurant.Abstractions;

namespace Restaurant.ViewModels
{
    public interface ISignInViewModel : INavigatableViewModel
    {
        string Email { get; set; }
        string Password { get; set; }
        ICommand Login { get; }
    }

}