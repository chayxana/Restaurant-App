using Restaurant.ViewModels;
using Xamarin.Forms.Xaml;

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
		}
	}

	public abstract class FoodDetailPageXaml : BaseContentPage<FoodDetailViewModel>
	{
	}
}