using Restaurant.Core.ViewModels;

namespace Restaurant.Mobile.UI.Pages.Welcome
{
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