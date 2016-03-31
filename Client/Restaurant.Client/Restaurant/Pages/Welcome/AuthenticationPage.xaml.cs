using ReactiveUI;
using Restaurant.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using System.Text;
using System.Threading.Tasks;
using System.Reactive.Linq;
using Xamarin.Forms;

namespace Restaurant.Pages
{
    public partial class AuthenticationPage : AuthenticationPageXaml
    {
        public AuthenticationPage()
        {
            InitializeComponent();
            var theme = App.Current.GetThemeFromColor("red");
            StatusBarColor = theme.Dark;
            ActionBarBackgroundColor = theme.Primary;
            ViewModel.Login.Subscribe(async x =>
            {
                await AnimateControls(0, Easing.SinOut);
                ViewModel.NavigateToMainPage(x);
                ViewModel.IsBusy = false;
            });                 
        }

        private async Task AnimateControls(int scale, Easing easing)
        {
            await emailStack.ScaleTo(scale, App.AnimationSpeed, easing);
            await passwordStack.ScaleTo(scale, App.AnimationSpeed, easing);
            await loginStack.ScaleTo(scale, App.AnimationSpeed, easing);
        }

        protected async override void OnLoaded()
        {
            await AnimateControls(1, Easing.SinIn);
            base.OnLoaded();
        }
    }

    public class AuthenticationPageXaml : BaseContentPage<AuthenticationViewModel>
    {

    }
}