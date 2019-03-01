using System;
using System.Reactive.Linq;
using Restaurant.Core.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Restaurant.Mobile.UI.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    // ReSharper disable once RedundantExtendsListEntry
    public partial class FoodsPage : FoodsXamlPage
    {
        private readonly IDisposable _itemSelectedSubscriber;

        public FoodsPage()
        {
            InitializeComponent();

            _itemSelectedSubscriber = Observable
                .FromEventPattern<SelectedItemChangedEventArgs>(FoodsList, "ItemSelected")
                .Select(x => x.Sender)
                .Cast<ListView>()
                .Subscribe(l => l.SelectedItem = null);
        }

        protected override async void OnLoaded()
        {
            base.OnLoaded();
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