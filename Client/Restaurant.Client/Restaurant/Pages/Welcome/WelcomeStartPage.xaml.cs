using Restaurant.ViewModels;
using System.Threading.Tasks;
using Restaurant.Abstractions.Managers;
using Xamarin.Forms;

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
            base.OnLoaded();
            await Task.Delay(300);
            await label1.ScaleTo(1, (uint)App.AnimationSpeed, Easing.SinIn);
            await label2.ScaleTo(1, (uint)App.AnimationSpeed, Easing.SinIn);
            await buttonStack.ScaleTo(1, (uint)App.AnimationSpeed, Easing.SinIn);
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

    public class WelcomeStartPageXaml : BaseContentPage<WelcomeViewModel>
    {

    }
}