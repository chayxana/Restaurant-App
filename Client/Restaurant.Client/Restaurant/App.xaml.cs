using Autofac;
using ReactiveUI;
using Restaurant.Abstractions.Managers;
using Restaurant.Abstractions.Services;
using Restaurant.Abstractions.ViewModels;
using Restaurant.Mappers;
using Restaurant.Pages;
using Restaurant.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]

namespace Restaurant
{
    public partial class App : Application
    {
        public static IContainer Container { get; private set; }

	    public App()
	    {
		    InitializeComponent();
		    var boot = new BootstrapperBase();

			Container = boot.Build();
		    AutoMapperConfiguration.Configure();

		    var navigationService = Container.Resolve<INavigationService>();
		    var welcomePage = navigationService.ResolveView(Container.Resolve<IWelcomeViewModel>());

			MainPage =  new NavigationPage(welcomePage as Page);
	    }

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

        public new static App Current => (App)Application.Current;

    }
}