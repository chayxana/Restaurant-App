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

            Observable.FromEventPattern<SelectedItemChangedEventArgs>(FoodsList, "ItemSelected")
                .Select(x => x.Sender)
                .Cast<ListView>()
                .Subscribe(l =>
                {
                    l.SelectedItem = null;
                });
        }

        protected override async void OnLoaded()
        {
            BindingContext = ViewModel;
            await ViewModel.LoadFoods();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
        }
    }
    public abstract class FoodsXamlPage : BaseContentPage<FoodsViewModel>
    {
    }
}
