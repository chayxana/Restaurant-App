using System.Threading.Tasks;
using Restaurant.Abstractions.Managers;
using Restaurant.ViewModels;
using Xamarin.Forms;

// ReSharper disable once CheckNamespace
namespace Restaurant.Pages
{
    public partial class WelcomeStartPage : WelcomeStartPageXaml
    {
        private readonly IThemeManager _themeManager;

        public WelcomeStartPage(IThemeManager themeManager)
        {
            _themeManager = themeManager;
            NavigationPage.SetHasNavigationBar(this, false);

            InitializeComponent();
            Initialize();
        }
        protected override async void OnLoaded()
        {
            await label1.ScaleTo(1, App.AnimationSpeed, Easing.SinIn);
            await label2.ScaleTo(1, App.AnimationSpeed, Easing.SinIn);
            await buttonStack.ScaleTo(1, App.AnimationSpeed, Easing.SinIn);

            BindingContext = ViewModel;
        }

        protected sealed override void Initialize()
        {
            base.Initialize();
            var theme = _themeManager.GetThemeFromColor("blue");
            StatusBarColor = theme.Dark;
            NavigationBarColor = theme.Primary;
            BackgroundColor = theme.Primary;
        }
    }

    public abstract class WelcomeStartPageXaml : BaseContentPage<WelcomeViewModel>
    {
    }
}