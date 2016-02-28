using Akavache;
using ModernHttpClient;
using ReactiveUI;
using ReactiveUI.XamForms;
using Restaurant.ViewModels;
using Restaurant.Views;
using Splat;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;

using Xamarin.Forms;

namespace Restaurant
{
    public class App : Application
    {
        public App()
        {
            var app = new AppBotstrapper();
            MainPage = app.MainPage();
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
    }

    public class AppBotstrapper : ReactiveObject, IScreen
    {
        // The Router holds the ViewModels for the back stack. Because it's
        // in this object, it will be serialized automatically.
        public RoutingState Router { get; protected set; }

        public RouterHost RouterHost { get; protected set; }

        public AppBotstrapper()
        {
            Router = new RoutingState();
            RouterHost = new RouterHost();

            Locator.CurrentMutable.RegisterConstant(this, typeof(IScreen));

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

            Locator.CurrentMutable.Register(() => new LoginView(), typeof(IViewFor<LoginViewModel>));

            Locator.CurrentMutable.Register(() => new RegesterView(), typeof(IViewFor<RegesterViewModel>));

            Locator.CurrentMutable.Register(() => new MainView(), typeof(IViewFor<MainViewModel>));

            var a = new LoginViewModel(this);
            Locator.CurrentMutable.RegisterConstant(a, typeof(LoginViewModel));


            Router.Navigate.Execute(a);
        }

        public Page MainPage()
        {
            return new ViewModels.RoutedViewHost();
        }
    }
    public class RouterHost : RoutingState
    {
        public RouterHost() : base()
        {
            PopToRoot = ReactiveCommand.Create();               
        }
        public ReactiveCommand<object> PopToRoot { get; set; }
    }
}
