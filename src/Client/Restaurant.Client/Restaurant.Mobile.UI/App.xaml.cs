using Autofac;
using Restaurant.Abstractions.Factories;
using Restaurant.Abstractions.ViewModels;
using Restaurant.Core;
using Restaurant.Core.Mappers;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Restaurant.Mobile.UI
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
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

	      //  AppCenter.Start("android=afb856fc-388d-4304-8f8e-4156155cc49f;" +
							//"ios=df01b975-ee7c-4006-8758-34926d7245e6;",
		     //   typeof(Analytics), typeof(Crashes));
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