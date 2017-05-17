using Akavache;
using ModernHttpClient;
using ReactiveUI;
using Restaurant.Pages;
using Restaurant.ViewModels;
using Splat;
using System.Net.Http;
using Autofac;
using Restaurant.Themes;
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
            AnimationSpeed = 200;


            var bootstrapper = new Bootstrapper();
            Container = bootstrapper.Build();

            var welcomePage = Container.Resolve<IViewFor<WelcomeViewModel>>();
            welcomePage.ViewModel = Container.Resolve<IWelcomeViewModel>() as WelcomeViewModel;


            MainPage = new NavigationPage(welcomePage as Page);
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

    public class AppBotstrapper : ReactiveObject// INavigatableScreen
    {
        // The Router holds the ViewModels for the back stack. Because it's
        // in this object, it will be serialized automatically.
        //public NavigationState Navigation { get; protected set; }

        public AppBotstrapper()
        {
            //Navigation = new NavigationState();

            //Locator.CurrentMutable.RegisterConstant(this, typeof(INavigatableScreen));

            // Set up Akavache
            // 
            // Akavache is a portable data serialization library that we'll use to
            // cache data that we've downloaded
            BlobCache.ApplicationName = "Restaurant";

            // Set up Fusillade
            //
            // Fusillade is a super cool library that will make it so that whenever
            // we issue web requests, we'll only issue 4 concurrently, and if we
            // end up issuing multiple requests to the same resource, it will
            // de-dupe them. We're saying here, that we want our *backing*
            // HttpMessageHandler to be ModernHttpClient.
            //Locator.CurrentMutable.RegisterConstant(new NativeMessageHandler(), typeof(HttpMessageHandler));

            //Locator.CurrentMutable.Register(() => new SignInPage(), typeof(IViewFor<SignInViewModel>));

            //Locator.CurrentMutable.Register(() => new SignUpPage(), typeof(IViewFor<SignUpViewModel>));

            //Locator.CurrentMutable.Register(() => new MainPage(), typeof(IViewFor<MainViewModel>)); 

        }

    }
}