using System.Windows.Input;

namespace Restaurant.Abstractions.ViewModels
{
    public interface ISignUpViewModel : INavigatableViewModel
    {
        string ConfirmPassword { get; set; }
        string Name { get; set; }
        ICommand Register { get; }
        string Email { get; set; }
        string Password { get; set; }
    }
}