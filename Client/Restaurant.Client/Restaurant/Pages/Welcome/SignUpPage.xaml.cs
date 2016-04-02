using ReactiveUI;
using Restaurant.ViewModels;
using System;
using Xamarin.Forms;

namespace Restaurant.Pages
{
    public partial class SignUpPage : SignUpPageXaml
    {
        public SignUpPage()
        {
            InitializeComponent();
            Initialize();
            
            // Example: Using WhenAny instead of Value Converters 
            ViewModel.WhenAnyValue(x => x.IsLoading).Subscribe(isLoading => 
            {
                if (isLoading)
                {
                    loadingLayout.IsVisible = true;
                    regesterStack.IsVisible = false;
                }
                else
                {
                    loadingLayout.IsVisible = false;
                    regesterStack.IsVisible = true;
                }
            });
        }

        protected override void Initialize()
        {
            var color = App.Current.GetThemeFromColor("indigo");
            StatusBarColor = color.Dark;
            ActionBarBackgroundColor = color.Primary;
            NavigationBarColor = Color.Black;
            base.Initialize();
        }
    }
    public class SignUpPageXaml : BaseContentPage<SignUpViewModel>
    {

    }
}
