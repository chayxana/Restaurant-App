using System.Windows.Input;
using JetBrains.Annotations;
using ReactiveUI;
using Restaurant.Abstractions.Facades;
using Restaurant.Abstractions.Managers;
using Restaurant.Abstractions.Services;
using Restaurant.Abstractions.ViewModels;
using Restaurant.Common.DataTransferObjects;
using Restaurant.Pages;

namespace Restaurant.ViewModels
{
    [UsedImplicitly]
    public class SignInViewModel : ViewModelBase, ISignInViewModel
    {
        private string _password;
        private string _email;

        /// <summary>
        /// Gets and sets login command
        /// Command that logins to service
        /// </summary>
        public ICommand Login { get; }

        /// <summary>
        /// Gets and sets user Email
        /// </summary>
        public string Email
        {
            get => _email;
            set => this.RaiseAndSetIfChanged(ref _email, value);
        }

        /// <summary>
        /// Gets and sets non encrypted user passwords
        /// </summary>
        public string Password
        {
            get => _password;
            set => this.RaiseAndSetIfChanged(ref _password, value);
        }

        public override string Title => "Login";

        public SignInViewModel(
            IAuthenticationManager authenticationManager, 
            IAutoMapperFacade autoMapperFacade,
            INavigationService navigationService)
        {
            var canLogin = this.WhenAny(x => x.Email, x => x.Password,
                (e, p) => !string.IsNullOrEmpty(e.Value) && !string.IsNullOrEmpty(p.Value));

            Login = ReactiveCommand.CreateFromTask(async _ =>
            {
                var result = await authenticationManager.Login(autoMapperFacade.Map<LoginDto>(this));
                if (!result.IsError)
                {
                   App.Current.MainPage = new MainPage();
                }
            }, canLogin);
        }


    }
}
