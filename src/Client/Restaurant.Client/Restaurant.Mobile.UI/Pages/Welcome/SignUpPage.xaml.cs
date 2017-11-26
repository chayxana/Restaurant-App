using Restaurant.Core.ViewModels;
using Xamarin.Forms.Xaml;

namespace Restaurant.Mobile.UI.Pages.Welcome
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
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