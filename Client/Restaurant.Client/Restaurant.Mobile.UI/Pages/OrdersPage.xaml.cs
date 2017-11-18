using Restaurant.Core.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Restaurant.Mobile.UI.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class OrdersPage : OrdersXamlPage
    {
        public OrdersPage()
        {
            InitializeComponent();
        }

        protected override async void OnLoaded()
        {
            BindingContext = ViewModel;
            await ViewModel.LoadOrders();
        }
    }

    public abstract class OrdersXamlPage : BaseContentPage<OrdersViewModel> { } 
}