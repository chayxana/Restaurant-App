using Autofac;
using Autofac.Core;
using Restaurant.Abstractions.Services;
using Restaurant.Abstractions.ViewModels;
using Restaurant.Mappers;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace Restaurant
{
    public partial class App : Application
    {
	    public App()
	    {
		    InitializeComponent();
		    var boot = new Bootstrapper();
	        boot.Build();
		    AutoMapperConfiguration.Configure();

		    var navigationService = Bootstrapper.Container.Resolve<INavigationService>();
		    var welcomePage = navigationService.ResolveView(Bootstrapper.Container.Resolve<IWelcomeViewModel>());

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