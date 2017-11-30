using System;
using System.Net;
using System.Windows.Input;
using JetBrains.Annotations;
using ReactiveUI;
using Restaurant.Abstractions.Facades;
using Restaurant.Abstractions.Providers;
using Restaurant.Abstractions.Services;
using Restaurant.Abstractions.ViewModels;
using Restaurant.Common.DataTransferObjects;

namespace Restaurant.Core.ViewModels
{
    [UsedImplicitly]
    public class SignUpViewModel : BaseViewModel, ISignUpViewModel
    {
        private readonly INavigationService _navigationService;

        private string _confirmPassword;
        private string _email;
        private string _password;

        public SignUpViewModel(
            IAutoMapperFacade autoMapperFacade,
            IAuthenticationProvider authenticationProvider,
            INavigationService navigationService)
        {
            _navigationService = navigationService;

            var canRegester = this.WhenAny(x => x.Password,
                x => x.ConfirmPassword, (p, cp) => p.Value == cp.Value);

            Register = ReactiveCommand
                .CreateFromTask(async _ =>
	            {
		            IsLoading = true;
                    var registerDto = autoMapperFacade.Map<RegisterDto>(this);
	                try
	                {
	                    var result = await authenticationProvider.Register(registerDto);

	                    if (result.IsSuccessStatusCode)
	                    {
	                        var loginResult = await authenticationProvider.Login(
	                            new LoginDto {Login = Email, Password = Password});

	                        if (!loginResult.IsError && loginResult.HttpStatusCode == HttpStatusCode.OK)
	                        {
								await _navigationService.NavigateToMainPage(typeof(IMainViewModel));
	                        }
	                    }
	                    IsLoading = false;
	                }
	                catch (Exception ex)
	                {
	                    // ignored
	                }
	                finally
	                {
	                    IsLoading = false;
	                }

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