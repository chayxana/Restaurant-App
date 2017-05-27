using ReactiveUI;
using Restaurant.Abstractions.Managers;
using Restaurant.ViewModels;
using Xamarin.Forms;
using System;
using System.Reactive.Linq;

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
            Title = "Foods";                       
        }

        protected override async void OnLoaded()
        {
            BindingContext = ViewModel;
            await ViewModel.LoadFoods();
            this.WhenAnyValue(x => x.ViewModel.SelectedFood).Where(x => x != null).Subscribe(food =>
            {
                Navigation.PushAsync(new FoodDetailPage());
            });
        }
    }
    public abstract class FoodsXamlPage : BaseContentPage<FoodsViewModel>
    {
    }
}
