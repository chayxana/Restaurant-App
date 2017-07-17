using System;
using System.Net.Http;
using System.Windows.Input;
using JetBrains.Annotations;
using ReactiveUI;
using Refit;
using Restaurant.Abstractions.Facades;
using Restaurant.Abstractions.Managers;
using Restaurant.Abstractions.Services;
using Restaurant.Abstractions.ViewModels;
using Restaurant.DataTransferObjects;
using Restaurant.Model;
using Restaurant.Models;

namespace Restaurant.ViewModels
{
    [UsedImplicitly]
    public class SignUpViewModel : ViewModelBase, ISignUpViewModel
    {
        private readonly INavigationService _navigationService;
        private string _name;

        public string Name
        {
            get => _name;
            set => this.RaiseAndSetIfChanged(ref _name, value);
        }

        private string _email;

        public string Email
        {
            get => _email;
            set => this.RaiseAndSetIfChanged(ref _email, value);
        }

        private string _password;

        public string Password
        {
            get => _password;
            set => this.RaiseAndSetIfChanged(ref _password, value);
        }

        private string _confirmPassword;

        public string ConfirmPassword
        {
            get => _confirmPassword;
            set => this.RaiseAndSetIfChanged(ref _confirmPassword, value);
        }

        public ICommand Regester { get; }


        public override string Title => "Sign Up";

        public SignUpViewModel(
            IAutoMapperFacade autoMapperFacade,
            IAuthenticationManager authenticationManager,
            INavigationService navigationService)
        {
            _navigationService = navigationService;

            var canRegester = this.WhenAny(x => x.Name, x => x.Email, x => x.Password,
                x => x.ConfirmPassword, (n, e, p, cp) => !string.IsNullOrEmpty(n.Value));

            Regester = ReactiveCommand
                .CreateFromTask(async _ =>
                {
                    var result = await authenticationManager.Register(autoMapperFacade.Map<RegisterDto>(this));
                    if (result != null)
                    {
                        var loginResult = await authenticationManager.Login(
                            new LoginDto() { Login = this.Email, Password = this.Password });

                        if (!loginResult.IsError)
                        {
                            await _navigationService.NavigateAsync(typeof(IMainViewModel));
                        }
                    }
                }, canRegester);
        }
    }
}
