using ReactiveUI;
using Restaurant.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System;
using System.Threading.Tasks;

namespace Restaurant.Pages
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class FoodDetailPage : FoodDetailPageXaml
	{
		public FoodDetailPage()
		{
			InitializeComponent();
			IsTransparentToolbar = true;
		}

		protected override void OnLoaded()
		{
			BindingContext = ViewModel;

			this.WhenAnyValue(x => x.ViewModel.Quantity).Subscribe(async x =>
			{
				var totalPrice = ViewModel.SelectedFood.Price * (decimal)x;
				for (decimal i = totalPrice - 15; i <= totalPrice; i++)
				{
					await Task.Delay(5);
					TotalPrice.Text = $"${i}.00";
				}
			});
		}
	}

	public abstract class FoodDetailPageXaml : BaseContentPage<FoodDetailViewModel>
	{
	}
}