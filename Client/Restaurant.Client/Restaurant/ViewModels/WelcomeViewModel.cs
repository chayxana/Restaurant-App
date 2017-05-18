using System.Windows.Input;
using JetBrains.Annotations;
using ReactiveUI;
using Restaurant.Abstractions.Services;
using Restaurant.Abstractions.ViewModels;

namespace Restaurant.ViewModels
{
    [UsedImplicitly]
    public class WelcomeViewModel : IWelcomeViewModel
    {
        public WelcomeViewModel(INavigationService navigationService, 
            ISignInViewModel signInViewModel,
            ISignUpViewModel signUpViewModel)
        {
            GoLogin = ReactiveCommand.Create(() => 
                                    navigationService.NavigateAsync(signInViewModel));

            GoRegister = ReactiveCommand.Create(() => 
                                    navigationService.NavigateAsync(signUpViewModel));
        }

        public string Title => "Welcome page";

        /// <summary>
        /// Gets and sets Open regester, 
        /// Command that opens regester page
        /// </summary>
        public ICommand GoRegister { get; }

        /// <summary>
        /// Gets and sets OpenLogin
        /// Command thats opens login page 
        /// </summary>
        public ICommand GoLogin { get;  }
    }
}
