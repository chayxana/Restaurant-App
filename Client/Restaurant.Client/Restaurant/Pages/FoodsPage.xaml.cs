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
        }

        private void List_OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            ((ListView) sender).SelectedItem = null;
        }
    }
    public abstract class FoodsXamlPage : BaseContentPage<FoodsViewModel>
    {
    }
}
