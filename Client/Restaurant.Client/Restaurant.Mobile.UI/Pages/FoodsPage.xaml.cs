using System;
using System.Reactive.Linq;
using Restaurant.Core.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Restaurant.Mobile.UI.Pages
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class FoodsPage : FoodsXamlPage
	{
		public FoodsPage()
		{
			InitializeComponent();

			Observable.FromEventPattern<SelectedItemChangedEventArgs>(FoodsList, "ItemSelected")
				.Select(x => x.Sender)
				.Cast<ListView>()
				.Subscribe(l => { l.SelectedItem = null; });
		}

		protected override async void OnLoaded()
		{
			BindingContext = ViewModel;
			await ViewModel.LoadFoods();
		}
	}

	public abstract class FoodsXamlPage : BaseContentPage<FoodsViewModel>
	{
	}
}