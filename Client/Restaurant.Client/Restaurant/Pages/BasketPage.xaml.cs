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
            NavigationBarColor = theme.Dark;
            StatusBarColor = theme.Dark;
            orders.ItemSelected += Orders_ItemSelected;
        }

        private void Orders_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            orders.SelectedItem = null;
        }
    }
    public class BasketPageXaml : BaseContentPage<BasketViewModel>
    {

    }
}
