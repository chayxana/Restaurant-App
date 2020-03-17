using System;
using System.Reactive.Linq;
using Restaurant.Core.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Restaurant.Mobile.UI.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    // ReSharper disable once RedundantExtendsListEntry
    public partial class BasketPage : BasketPageXaml
    {
        public BasketPage()
        {
            InitializeComponent();

            Observable.FromEventPattern<SelectedItemChangedEventArgs>(orders, "ItemSelected")
                .Select(x => x.Sender)
                .Cast<ListView>()
                .Subscribe(l => l.SelectedItem = null);
        }
    }
    
    public abstract class BasketPageXaml : BaseContentPage<BasketViewModel>
    {
    }
}