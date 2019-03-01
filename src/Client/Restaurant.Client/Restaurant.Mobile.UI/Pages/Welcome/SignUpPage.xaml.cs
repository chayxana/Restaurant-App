using Restaurant.Core.ViewModels;
using Xamarin.Forms.Xaml;

namespace Restaurant.Mobile.UI.Pages.Welcome
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	// ReSharper disable once RedundantExtendsListEntry
	public partial class SignUpPage : SignUpPageXaml
    {
        public SignUpPage()
        {
            InitializeComponent();
        }
    }

    public abstract class SignUpPageXaml : BaseContentPage<SignUpViewModel>
    {
    }
}