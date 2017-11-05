using Restaurant.Abstractions.Managers;
using Restaurant.ViewModels;

namespace Restaurant.Pages.Welcome
{
	public partial class SignUpPage : SignUpPageXaml
	{
		private readonly IThemeManager _themeManager;

		public SignUpPage(IThemeManager themeManager)
		{
			_themeManager = themeManager;
			SetColors();

			InitializeComponent();
		}

		public SignUpPage()
		{
			InitializeComponent();
		}

		private void SetColors()
		{
			//var color = _themeManager.GetThemeFromColor("indigo");
			//StatusBarColor = color.Dark;
			//ActionBarBackgroundColor = color.Primary;
		}

		protected override void OnLoaded()
		{
		}
	}

	public abstract class SignUpPageXaml : BaseContentPage<SignUpViewModel>
	{
	}
}