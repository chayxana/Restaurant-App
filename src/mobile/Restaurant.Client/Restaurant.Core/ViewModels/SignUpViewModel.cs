using System;
using System.Net;
using System.Windows.Input;
using AutoMapper;
using JetBrains.Annotations;
using ReactiveUI;
using Restaurant.Abstractions.DataTransferObjects;
using Restaurant.Abstractions.Facades;
using Restaurant.Abstractions.Providers;
using Restaurant.Abstractions.Services;
using Restaurant.Abstractions.ViewModels;

namespace Restaurant.Core.ViewModels
{
    [UsedImplicitly]
    public class SignUpViewModel : BaseViewModel, ISignUpViewModel
    {
        private string _confirmPassword;
        private string _email;
        private string _password;

        public SignUpViewModel(
            IMapper mapper,
            IAuthenticationProvider authenticationProvider,
            INavigationService navigationService)
        {
            var canRegester = this.WhenAny(x => x.Password,
                x => x.ConfirmPassword, (p, cp) => p.Value == cp.Value);

            Register = ReactiveCommand
                .CreateFromTask(async _ =>
                {
                    IsLoading = true;
                    var registerDto = mapper.Map<RegisterDto>(this);
                    var result = await authenticationProvider.Register(registerDto);

                    if (result.IsSuccessStatusCode)
                    {
                        var loginResult = await authenticationProvider.Login(
                            new LoginDto { Login = Email, Password = Password });

                        if (!loginResult.IsError && loginResult.HttpStatusCode == HttpStatusCode.OK)
                        {
                            await navigationService.NavigateToMainPage(typeof(IMainViewModel));
                        }
                    }

                    IsLoading = false;

                }, canRegester);
        }

        public string Email
        {
            get => _email;
            set => this.RaiseAndSetIfChanged(ref _email, value);
        }

        public string Password
        {
            get => _password;
            set => this.RaiseAndSetIfChanged(ref _password, value);
        }

        public string ConfirmPassword
        {
            get => _confirmPassword;
            set => this.RaiseAndSetIfChanged(ref _confirmPassword, value);
        }

        public ICommand Register { get; }

        public override string Title => "Sign Up";
    }
}