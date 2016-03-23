using Restaurant.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace Restaurant.Pages
{
    public partial class BasketPage : BasketPageXaml
    {
        public BasketPage()
        {
            InitializeComponent();
            var theme = App.Current.GetThemeFromColor("green");
            ActionBarBackgroundColor = theme.Primary;
            StatusBarColor = theme.Dark;
        }
    }
    public class BasketPageXaml : BaseContentPage<BasketViewModel>
    {

    }
}
