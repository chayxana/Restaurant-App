using Restaurant.Abstractions.Managers;
using Restaurant.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Restaurant.Pages
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class BasketPage : BasketPageXaml
    {
        public BasketPage(IThemeManager themeManager)
        {
            InitializeComponent();

            //var theme = themeManager.GetThemeFromColor("green");
            //ActionBarBackgroundColor = theme.Primary;
            //StatusBarColor = theme.Dark;
            orders.ItemSelected += Orders_ItemSelected;
        }

        private void Orders_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            orders.SelectedItem = null;
        }

        protected override void OnLoaded()
        {
	        this.BindingContext = ViewModel;
        }

        protected override void UnLoad()
        {
            orders.ItemSelected -= Orders_ItemSelected;
        }
    }

    public abstract class BasketPageXaml : BaseContentPage<BasketViewModel>
    {
    }
}
