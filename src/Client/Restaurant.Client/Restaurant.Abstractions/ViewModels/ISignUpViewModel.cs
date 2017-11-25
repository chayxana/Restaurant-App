using System.Windows.Input;

namespace Restaurant.Abstractions.ViewModels
{
    public interface ISignUpViewModel : INavigatableViewModel
    {
        ICommand Register { get; }
        string Email { get; set; }
        string Password { get; set; }
	    string ConfirmPassword { get; set; }
	}
}