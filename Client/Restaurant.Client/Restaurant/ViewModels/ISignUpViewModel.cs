using System.Windows.Input;
using Restaurant.Abstractions;

namespace Restaurant.ViewModels
{
    public interface ISignUpViewModel : INavigatableViewModel
    {
        string ConfirmPassword { get; set; }
        string Name { get; set; }
        ICommand Regester { get; }
        string RegesterEmail { get; set; }
        string RegesterPassword { get; set; }
    }
}