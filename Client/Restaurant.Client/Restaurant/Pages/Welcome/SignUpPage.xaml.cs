using ReactiveUI;
using Restaurant.ViewModels;
using System;
using Restaurant.Abstractions.Managers;
using Xamarin.Forms;

namespace Restaurant.Pages
{
    public partial class SignUpPage : SignUpPageXaml
    {
        private readonly IThemeManager _themeManager;

        public SignUpPage(IThemeManager themeManager)
        {
            _themeManager = themeManager;
            InitializeComponent();
            Initialize();
            
            // Example: Using WhenAny instead of Value Converters 
            //ViewModel.WhenAnyValue(x => x.IsLoading).Subscribe(isLoading => 
            //{
            //    if (isLoading)
            //    {
            //        loadingLayout.IsVisible = true;
            //        regesterStack.IsVisible = false;
            //    }
            //    else
            //    {
            //        loadingLayout.IsVisible = false;
            //        regesterStack.IsVisible = true;
            //    }
            //});
        }

        protected override void Initialize()
        {
            var color = _themeManager.GetThemeFromColor("indigo");
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
