using Autofac;
using ReactiveUI;
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

        public App(BootstrapperBase bootstrapper)
        {
            InitializeComponent();
            AnimationSpeed = 200;
            
            Container = bootstrapper.Build();
            AutoMapperConfiguration.Configure();

            MainPage = new MainPage();
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

        public static uint AnimationSpeed { get; internal set; }

    }
}