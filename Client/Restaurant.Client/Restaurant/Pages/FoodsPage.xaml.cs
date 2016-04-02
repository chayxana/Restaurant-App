using Restaurant.ViewModels;
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
            NavigationBarColor = theme.Dark;

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
