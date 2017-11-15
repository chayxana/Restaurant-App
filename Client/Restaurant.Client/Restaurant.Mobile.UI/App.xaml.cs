using Autofac;
using Restaurant.Abstractions.Factories;
using Restaurant.Abstractions.Services;
using Restaurant.Abstractions.ViewModels;
using Restaurant.Core;
using Restaurant.Core.Mappers;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]

namespace Restaurant.Mobile.UI
{
	public partial class App : Application
	{
		public App()
		{
			InitializeComponent();
            var boot = new Bootstrapper();
            boot.Build();
            AutoMapperConfiguration.Configure();

            var viewResolverService = BootstrapperBase.Container.Resolve<IViewFactory>();
			var welcomePage = viewResolverService.ResolveView(BootstrapperBase.Container.Resolve<IWelcomeViewModel>());

			MainPage = new NavigationPage(welcomePage as Page);
		}

		public new static App Current => (App) Application.Current;

		protected override void OnStart()
		{
			base.OnStart();
		}


		protected override void OnResume()
		{
			base.OnResume();
		}

		protected override void OnSleep()
		{
			base.OnSleep();
		}
	}
}