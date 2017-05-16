using Akavache;
using ModernHttpClient;
using ReactiveUI;
using Restaurant.Pages;
//using Restaurant.ReactiveUI;
using Restaurant.ViewModels;
using Splat;
using System.Net.Http;
using Restaurant.Themes;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]

namespace Restaurant
{
    public partial class App : Application
    {
        public bool SignIn { get; set; } = false;

        public App()
        {
            InitializeComponent();
            var app = new AppBotstrapper();
            if (SignIn)
            {
                MainPage = app.MainPage();
            }
            else
            {
                MainPage = app.WelcomeStartPage();
            }
            AnimationSpeed = 200;
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

        public MaterialTheme GetThemeFromColor(string color)
        {
           return new MaterialTheme();
        }
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
            Locator.CurrentMutable.RegisterConstant(new NativeMessageHandler(), typeof(HttpMessageHandler));

            Locator.CurrentMutable.Register(() => new AuthenticationPage(), typeof(IViewFor<AuthenticationViewModel>));

            Locator.CurrentMutable.Register(() => new SignUpPage(), typeof(IViewFor<SignUpViewModel>));

            Locator.CurrentMutable.Register(() => new MainPage(), typeof(IViewFor<MainViewModel>)); 

        }

        public Page WelcomeStartPage()
        {
            Locator.CurrentMutable.RegisterConstant(new AuthenticationViewModel(), typeof(AuthenticationViewModel));
            return new WelcomeStartPage().WithinNavigationPage();
        }

        public Page MainPage()
        {
            Locator.CurrentMutable.RegisterConstant(new MainViewModel(null), typeof(MainViewModel));
            return new MainPage();
        }
    }

    public interface ILoginManager
    {
        void ShowMainPage();

        void LogOut();
    }
}