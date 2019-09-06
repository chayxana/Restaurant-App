using System;
using Autofac;
using Foundation;
using ImageCircle.Forms.Plugin.iOS;
using Lottie.Forms.iOS.Renderers;
using Restaurant.Abstractions.Services;
using Restaurant.Mobile.UI;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

namespace Restaurant.iOS
{
    [Register("AppDelegate")]
    public class AppDelegate : FormsApplicationDelegate
    {
        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
//            UINavigationBar.Appearance.BarTintColor = UIColor.White;
//            UINavigationBar.Appearance.TintColor = UIColor.Black; //Tint color of button items
//            UINavigationBar.Appearance.SetBackgroundImage(new UIImage(), UIBarMetrics.Default);
//            UINavigationBar.Appearance.ShadowImage = new UIImage();
//            UINavigationBar.Appearance.BackgroundColor = new UIColor(0, 0, 0, 0);
//            UINavigationBar.Appearance.Translucent = true;
            
            Forms.SetFlags("CollectionView_Experimental");

            Forms.Init();
            AnimationViewRenderer.Init();
            ImageCircleRenderer.Init();

            LoadApplication(new App(new iOSPlatformInitializer()));

            return base.FinishedLaunching(app, options);
        }
    }

    public class iOSPlatformInitializer : MobilePlatformInitializer
    {
        protected override void RegisterTypes(ContainerBuilder builder)
        {
            base.RegisterTypes(builder);
            builder.RegisterType<LoggingService>().As<ILoggingService>();
        }
    }

    internal class LoggingService : ILoggingService
    {
        public void Debug(string message)
        {
        }

        public void Error(string message)
        {
        }

        public void Error(Exception ex)
        {
        }

        public void Error(Exception e, string message)
        {
        }

        public void Info(string message)
        {
        }

        public void Trace(string message)
        {
        }

        public void Warn(string message)
        {
        }
    }
}