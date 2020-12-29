using System.Reactive;
using System.Threading.Tasks;
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
        private readonly INavigationService _navigationService;

        public WelcomeViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
            GoLogin = ReactiveCommand.CreateFromTask(GotoSignin);

            GoRegister = ReactiveCommand.CreateFromTask(async () =>
                await navigationService.NavigateAsync(typeof(ISignUpViewModel)));
        }

        private Task GotoSignin() => _navigationService.NavigateAsync(typeof(ISignInViewModel));

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
        public ReactiveCommand<Unit, Unit> GoLogin { get; }
    }
}