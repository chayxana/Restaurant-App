using System.Windows.Input;
using JetBrains.Annotations;
using ReactiveUI;
using Restaurant.Abstractions.Services;
using Restaurant.Abstractions.ViewModels;

namespace Restaurant.Core.ViewModels
{
    [UsedImplicitly]
    public class WelcomeViewModel : IWelcomeViewModel
    {
        public WelcomeViewModel(INavigationService navigationService)
        {
            GoLogin = ReactiveCommand.CreateFromTask(async () =>
                await navigationService.NavigateAsync(typeof(ISignInViewModel)));

            GoRegister = ReactiveCommand.CreateFromTask(async () =>
                await navigationService.NavigateAsync(typeof(ISignUpViewModel)));
        }

        public string Title => "Welcome page";

        /// <summary>
        ///     Gets and sets Open regester,
        ///     Command that opens regester page
        /// </summary>
        public ICommand GoRegister { get; }

        /// <summary>
        ///     Gets and sets OpenLogin
        ///     Command thats opens login page
        /// </summary>
        public ICommand GoLogin { get; }
    }
}