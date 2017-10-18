using System;
using Restaurant.Abstractions.Managers;
using Restaurant.Constants;
using Restaurant.ViewModels;
using Xamarin.Forms;

namespace Restaurant.Pages.Welcome
{
	public partial class WelcomeStartPage : WelcomeStartPageXaml
	{
		public WelcomeStartPage(IThemeManager themeManager)
		{
			NavigationPage.SetHasNavigationBar(this, false);
			InitializeComponent();

			var theme = themeManager.GetThemeFromColor("blue");
			StatusBarColor = theme.Dark;
			BackgroundColor = theme.Primary;
		}
		protected override async void OnLoaded()
		{
			await label1.ScaleTo(1, AppConstants.AnimationSpeed, Easing.SinIn);
			await label2.ScaleTo(1, AppConstants.AnimationSpeed, Easing.SinIn);
			await buttonStack.ScaleTo(1, AppConstants.AnimationSpeed, Easing.SinIn);
			await signUpStack.ScaleTo(1, AppConstants.AnimationSpeed, Easing.SinIn);

			BindingContext = ViewModel;
			
		}
	}

	public abstract class WelcomeStartPageXaml : BaseContentPage<WelcomeViewModel>
	{
	}
}