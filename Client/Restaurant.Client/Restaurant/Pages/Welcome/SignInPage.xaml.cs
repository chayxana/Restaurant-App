using System.Threading.Tasks;
using Restaurant.Abstractions.Managers;
using Restaurant.Constants;
using Restaurant.ViewModels;
using Xamarin.Forms;

namespace Restaurant.Pages.Welcome
{
	public partial class SignInPage : SignInPageXaml
	{
		private readonly IThemeManager _themeManager;

		public SignInPage(IThemeManager themeManager)
		{
			_themeManager = themeManager;
			InitializeComponent();
			SetColors();

			//SignInViewModel.Login.Subscribe(async x =>
			//{
			//    await AnimateControls(0, Easing.SinOut);
			//    SignInViewModel.NavigateToMainPage(x);
			//    SignInViewModel.IsBusy = false;
			//});                 
		}

		private void SetColors()
		{
			//var theme = _themeManager.GetThemeFromColor("red");
			//StatusBarColor = theme.Dark;
			//ActionBarBackgroundColor = theme.Primary;
		}

		private async Task AnimateControls(int scale, Easing easing)
		{
			await emailStack.ScaleTo(scale, AppConstants.AnimationSpeed, easing);
			await passwordStack.ScaleTo(scale, AppConstants.AnimationSpeed, easing);
			await loginStack.ScaleTo(scale, AppConstants.AnimationSpeed, easing);
		}

		protected override async void OnLoaded()
		{
			BindingContext = ViewModel;
			await AnimateControls(1, Easing.SinIn);
		}
	}

	public abstract class SignInPageXaml : BaseContentPage<SignInViewModel>
	{

	}
}