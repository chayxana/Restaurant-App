using Autofac;
using Microsoft.AppCenter;
using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Crashes;
using Restaurant.Abstractions.Factories;
using Restaurant.Abstractions.ViewModels;
using Restaurant.Core;
using Restaurant.Core.Mappers;
using Restaurant.Mobile.UI.Pages;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Restaurant.Mobile.UI
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class App : Application
    {
        private readonly IPlatformInitializer _platformInitializer;

        public App(IPlatformInitializer platformInitializer)
        {
            _platformInitializer = platformInitializer;
            InitializeComponent();
            AutoMapperConfiguration.Configure();
            SetupMainPage();
        }

        private void SetupMainPage()
        {
            var container = _platformInitializer.Build();
            var viewFactory = container.Resolve<IViewFactory>();
            var welcomePage = viewFactory.ResolveView<IWelcomeViewModel>() as Page;
            MainPage = new CustomNavigationPage(welcomePage);
        }

        public new static App Current => (App)Application.Current;

        protected override void OnStart()
        {
            base.OnStart();

            AppCenter.Start("android=afb856fc-388d-4304-8f8e-4156155cc49f;" +
                            "ios=df01b975-ee7c-4006-8758-34926d7245e6;",
                             typeof(Analytics), typeof(Crashes));

            AppCenter.LogLevel = LogLevel.Verbose;
        }
    }
}