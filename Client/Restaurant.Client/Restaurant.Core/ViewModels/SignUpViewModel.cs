using System.Net;
using System.Windows.Input;
using JetBrains.Annotations;
using ReactiveUI;
using Restaurant.Abstractions.Facades;
using Restaurant.Abstractions.Managers;
using Restaurant.Abstractions.Services;
using Restaurant.Abstractions.ViewModels;
using Restaurant.Common.DataTransferObjects;

namespace Restaurant.Core.ViewModels
{
	[UsedImplicitly]
	public class SignUpViewModel : ViewModelBase, ISignUpViewModel
	{
		private readonly INavigationService _navigationService;

		private string _confirmPassword;
		private string _email;
		private string _name;
		private string _password;

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
					var registerDto = autoMapperFacade.Map<RegisterDto>(this);
					var result = await authenticationManager.Register(registerDto);

					if (result != null)
					{
						var loginResult = await authenticationManager.Login(
							new LoginDto() { Login = this.Email, Password = this.Password });

						if (!loginResult.IsError && loginResult.HttpStatusCode == HttpStatusCode.OK)
						{
							await _navigationService.NavigateAsync(typeof(IMainViewModel));
						}
					}

				}, canRegester);
		}

		public string Name
		{
			get => _name;
			set => this.RaiseAndSetIfChanged(ref _name, value);
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

		public ICommand Regester { get; }

		public override string Title => "Sign Up";
	}
}