using Restaurant.Core.ViewModels.Food;
using Xamarin.Forms.Xaml;

namespace Restaurant.Mobile.UI.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    // ReSharper disable once RedundantExtendsListEntry
    public partial class FoodDetailPage : FoodDetailPageXaml, ITransparentActionBarPage
    {
        public FoodDetailPage()
        {
            InitializeComponent();
        }

        public bool IsTransparentActionBar => true;
    }

    public abstract class FoodDetailPageXaml : BaseContentPage<FoodDetailViewModel>
    {
    }
}