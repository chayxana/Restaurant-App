using Restaurant.ViewModels;

namespace Restaurant.Pages.Welcome
{
	public partial class SignUpPage : SignUpPageXaml
	{
		public SignUpPage()
		{
			InitializeComponent();
		}

		protected override void OnLoaded()
		{
		    BindingContext = ViewModel;
		}
	}

	public abstract class SignUpPageXaml : BaseContentPage<SignUpViewModel>
	{
	}
}