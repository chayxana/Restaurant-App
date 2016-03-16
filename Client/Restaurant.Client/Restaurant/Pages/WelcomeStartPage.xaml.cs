using ReactiveUI;
using Restaurant.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace Restaurant.Pages
{
    public partial class WelcomeStartPage : WelcomeStartPageXaml
    {
        public WelcomeStartPage()
        {
            NavigationPage.SetHasNavigationBar(this, false);

            InitializeComponent();
            Initialize();
        }
        protected async override void OnLoaded()
        {
            base.OnLoaded();
            await Task.Delay(300);
            await label1.ScaleTo(1, (uint)App.AnimationSpeed, Easing.SinIn);
            await label2.ScaleTo(1, (uint)App.AnimationSpeed, Easing.SinIn);
            await buttonStack.ScaleTo(1, (uint)App.AnimationSpeed, Easing.SinIn);
        }
        protected override void Initialize()
        {
            base.Initialize();
            var theme = App.Current.GetThemeFromColor("blue");
            StatusBarColor = theme.Dark;
            NavigationBarColor = theme.Primary;
            BackgroundColor = theme.Primary;

        }
    }

    public class WelcomeStartPageXaml : BaseContentPage<AuthenticationViewModel>
    {

    }
}