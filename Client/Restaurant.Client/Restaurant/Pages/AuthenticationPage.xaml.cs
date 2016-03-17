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
    public partial class AuthenticationPage : AuthenticationPageXaml
    {
        public AuthenticationPage()
        {
            InitializeComponent();
            var theme = App.Current.GetThemeFromColor("red");
            StatusBarColor = theme.Dark;
            ActionBarBackgroundColor = theme.Primary;
        }
    }

    public class AuthenticationPageXaml : BaseContentPage<AuthenticationViewModel>
    {

    }
}