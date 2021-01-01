using Restaurant.Core.ViewModels.Food;
using Xamarin.Forms.Xaml;

namespace Restaurant.Mobile.UI.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    // ReSharper disable once RedundantExtendsListEntry
    public partial class FoodsPage : FoodsXamlPage
    {
        public FoodsPage()
        {
            InitializeComponent();
            FoodsList.SelectionChanged += (s, e) => { FoodsList.SelectedItem = null; };
        }

        protected override async void OnLoaded()
        {
            base.OnLoaded();
            await ViewModel.LoadFoods();
        }
    }

    public abstract class FoodsXamlPage : BaseContentPage<FoodsViewModel>
    {
    }
}