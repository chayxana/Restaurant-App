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
        private readonly IDisposable _itemSelectedSubscriber;

        public FoodsPage(IThemeManager themeManager)
        {
            InitializeComponent();

            var theme = themeManager.GetThemeFromColor("red");
            ActionBarBackgroundColor = theme.Primary;
            StatusBarColor = theme.Dark;
            ActionBarTextColor = Color.White;
            Title = "Foods";

            _itemSelectedSubscriber = Observable.FromEventPattern<SelectedItemChangedEventArgs>(FoodsList, "ItemSelected")
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

        protected override void UnLoad()
        {
            base.UnLoad();
            _itemSelectedSubscriber.Dispose();
        }
    }
    public abstract class FoodsXamlPage : BaseContentPage<FoodsViewModel>
    {
    }
}
