using Restaurant.ViewModels;
using System;
using System.Threading.Tasks;
using Restaurant.Abstractions.Managers;
using Xamarin.Forms;

namespace Restaurant.Pages
{
    public partial class SignInPage : SignInPageXaml
    {
        public SignInPage(IThemeManager themeManager)
        {
            InitializeComponent();
            var theme = themeManager.GetThemeFromColor("red");
            StatusBarColor = theme.Dark;
            ActionBarBackgroundColor = theme.Primary;
            //ViewModel.Login.Subscribe(async x =>
            //{
            //    await AnimateControls(0, Easing.SinOut);
            //    ViewModel.NavigateToMainPage(x);
            //    ViewModel.IsBusy = false;
            //});                 
        }

        private async Task AnimateControls(int scale, Easing easing)
        {
            await emailStack.ScaleTo(scale, App.AnimationSpeed, easing);
            await passwordStack.ScaleTo(scale, App.AnimationSpeed, easing);
            await loginStack.ScaleTo(scale, App.AnimationSpeed, easing);
        }

        protected override async void OnLoaded()
        {
            await AnimateControls(1, Easing.SinIn);
            base.OnLoaded();
        }
    }

    public class SignInPageXaml : BaseContentPage<SignInViewModel>
    {

    }
}