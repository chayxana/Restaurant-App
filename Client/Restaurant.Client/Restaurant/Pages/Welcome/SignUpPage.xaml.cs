using Restaurant.Abstractions.Managers;
using Restaurant.ViewModels;
using Xamarin.Forms;

namespace Restaurant.Pages
{
    public partial class SignUpPage : SignUpPageXaml
    {
        private readonly IThemeManager _themeManager;

        public SignUpPage(IThemeManager themeManager)
        {
            _themeManager = themeManager;
            InitializeComponent();
        }

        protected override void Initialize()
        {
            var color = _themeManager.GetThemeFromColor("indigo");
            StatusBarColor = color.Dark;
            ActionBarBackgroundColor = color.Primary;
            NavigationBarColor = Color.Black;
            base.Initialize();
        }

        protected override void OnLoaded()
        {
        }
    }
    public abstract class SignUpPageXaml : BaseContentPage<SignUpViewModel>
    {

    }
}
