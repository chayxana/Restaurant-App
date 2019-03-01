using System.Threading.Tasks;
using Restaurant.Core.ViewModels;
using Restaurant.Mobile.UI.Constants;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Restaurant.Mobile.UI.Pages.Welcome
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	// ReSharper disable once RedundantExtendsListEntry
	public partial class SignInPage : SignInPageXaml
    {
        public SignInPage()
        {
            InitializeComponent();
        }

        protected override async void OnLoaded()
        {
            await AnimateControls(1, Easing.SinIn);
        }

        private async Task AnimateControls(int scale, Easing easing)
        {
            await emailStack.ScaleTo(scale, AppConstants.AnimationSpeed, easing);
            await passwordStack.ScaleTo(scale, AppConstants.AnimationSpeed, easing);
            await loginStack.ScaleTo(scale, AppConstants.AnimationSpeed, easing);
        }
    }

    public abstract class SignInPageXaml : BaseContentPage<SignInViewModel>
    {
    }
}