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
            InitializeComponent();
        }
    }

    public class WelcomeStartPageXaml : BaseContentPage<AuthenticationViewModel>
    {

    }
}