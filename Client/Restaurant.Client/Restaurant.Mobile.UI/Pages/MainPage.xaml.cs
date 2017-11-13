using Autofac;
using ReactiveUI;
using Restaurant.Abstractions.ViewModels;
using Restaurant.Core;
using Restaurant.Core.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Restaurant.Mobile.UI.Pages
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	// ReSharper disable once RedundantExtendsListEntry
	public partial class MainPage : MainPageXaml
	{
		public MainPage()
		{
			InitializeComponent();
			BindingContext = BootstrapperBase.Container.Resolve<IMainViewModel>();
			Master = new MasterPage();
			var view = BootstrapperBase.Container.Resolve<IViewFor<FoodsViewModel>>();
			view.ViewModel = BootstrapperBase.Container.Resolve<FoodsViewModel>();
			var page = view as Page;

			Detail = new NavigationPage(page);
		}
	}

	public class MainPageXaml : BaseMasterDetailPage<MainViewModel>
	{
		protected MainPageXaml()
		{
		}
	}

	public class BaseMasterDetailPage<T> : MasterDetailPage, IViewFor<T> where T : class
	{
		protected BaseMasterDetailPage()
		{
		}

		public T ViewModel { get; set; }

		object IViewFor.ViewModel
		{
			get => ViewModel;

			set => ViewModel = (T) value;
		}
	}
}