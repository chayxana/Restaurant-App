using Restaurant.Core.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;
using Xamarin.Forms.Xaml;

namespace Restaurant.Mobile.UI.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FoodDetailPage : FoodDetailPageXaml, ITransparentActionBarPage
    {
        public FoodDetailPage()
        {
            InitializeComponent();
            IsTransparentActionBar = true;
        }

        public bool IsTransparentActionBar { get; }

        protected override void OnLoaded()
        {
            BindingContext = ViewModel;
        }
    }

    public abstract class FoodDetailPageXaml : BaseContentPage<FoodDetailViewModel>
    {
    }
}