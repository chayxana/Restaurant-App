using Restaurant.Abstractions.Managers;
using Restaurant.ViewModels;
using Xamarin.Forms;

namespace Restaurant.Pages
{
    public partial class FoodsPage : FoodsXamlPage
    {
        public FoodsPage(IThemeManager themeManager)
        {
            InitializeComponent();

            var theme = themeManager.GetThemeFromColor("bluePink");
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

        protected override async void OnLoaded()
        {
            base.OnLoaded();
            BindingContext = ViewModel;
            await ViewModel.LoadFoods();
        }
    }
    public class FoodsXamlPage : BaseContentPage<FoodsViewModel>
    {
    }
}
