using Restaurant.Core.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Restaurant.Mobile.UI.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    // ReSharper disable once RedundantExtendsListEntry
    public partial class OrdersPage : OrdersXamlPage
    {
        public OrdersPage()
        {
            InitializeComponent();
        }

        protected override void OnLoaded()
        {
            //await ViewModel.LoadOrders();
        }
    }

    public abstract class OrdersXamlPage : BaseContentPage<OrdersViewModel>
    {
    }
}