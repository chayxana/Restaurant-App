using Restaurant.Abstractions.Managers;
using Restaurant.Constants;
using Restaurant.ViewModels;
using Xamarin.Forms;

namespace Restaurant.Pages.Welcome
{
	public partial class WelcomeStartPage : WelcomeStartPageXaml
	{
		public WelcomeStartPage()
		{
		    NavigationPage.SetHasNavigationBar(this, false);
            InitializeComponent();
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