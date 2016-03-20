using Restaurant.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace Restaurant.Pages
{
    public partial class FoodsPage : FoodsXamlPage
    {
        public FoodsPage()
        {
            InitializeComponent();
            var theme = App.Current.GetThemeFromColor("bluePink");
            ActionBarBackgroundColor = theme.Primary;
            StatusBarColor = theme.Dark;
            ActionBarTextColor = Color.White;
            list.ItemSelected += (s, e) =>
            {
                list.SelectedItem = null;
            };
            Title = "Foods";
                       
        }
    }
    public class FoodsXamlPage : BaseContentPage<FoodsViewModel>
    {

    }
}
