using System;
using Autofac;
using Foundation;
using ImageCircle.Forms.Plugin.iOS;
using Restaurant.Mobile.UI;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

namespace Restaurant.iOS
{
    // The UIApplicationDelegate for the application. This class is responsible for launching the 
    // User Interface of the application, as well as listening (and optionally responding) to 
    // application events from iOS.
    [Register("AppDelegate")]
    public class AppDelegate : FormsApplicationDelegate
    {
        //
        // This method is invoked when the application has loaded and is ready to run. In this 
        // method you should instantiate the window, load the UI into it and then make the window
        // visible.
        //
        // You have 17 seconds to return from this method, or iOS will terminate your application.
        //
        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
            //UINavigationBar.Appearance.BarTintColor = FromHexString("#2196F3"); //bar background
            UINavigationBar.Appearance.TintColor = UIColor.Black; //Tint color of button items
            //UINavigationBar.Appearance.SetTitleTextAttributes(new UITextAttributes
            //{
            //	//Font = UIFont.FromName("HelveticaNeue-Light", 20f),
            //	TextColor = UIColor.White
            //});

            UIApplication.SharedApplication.StatusBarStyle = UIStatusBarStyle.LightContent;
            Forms.Init();
            ImageCircleRenderer.Init();

            LoadApplication(new App(new iOSPlatformInitializer()));

            return base.FinishedLaunching(app, options);
        }

        private static UIColor FromHexString(string hexValue)
        {
            var colorString = hexValue.Replace("#", "");
            float red, green, blue;

            switch (colorString.Length)
            {
                case 3: // #RGB
                {
                    red = Convert.ToInt32(string.Format("{0}{0}", colorString.Substring(0, 1)), 16) / 255f;
                    green = Convert.ToInt32(string.Format("{0}{0}", colorString.Substring(1, 1)), 16) / 255f;
                    blue = Convert.ToInt32(string.Format("{0}{0}", colorString.Substring(2, 1)), 16) / 255f;
                    return UIColor.FromRGB(red, green, blue);
                }
                case 6: // #RRGGBB
                {
                    red = Convert.ToInt32(colorString.Substring(0, 2), 16) / 255f;
                    green = Convert.ToInt32(colorString.Substring(2, 2), 16) / 255f;
                    blue = Convert.ToInt32(colorString.Substring(4, 2), 16) / 255f;
                    return UIColor.FromRGB(red, green, blue);
                }
                case 8: // #AARRGGBB
                {
                    var alpha = Convert.ToInt32(colorString.Substring(0, 2), 16) / 255f;
                    red = Convert.ToInt32(colorString.Substring(2, 2), 16) / 255f;
                    green = Convert.ToInt32(colorString.Substring(4, 2), 16) / 255f;
                    blue = Convert.ToInt32(colorString.Substring(6, 2), 16) / 255f;
                    return UIColor.FromRGBA(red, green, blue, alpha);
                }
                default:
                    throw new ArgumentOutOfRangeException(string.Format(
                        "Invalid color value {0} is invalid. It should be a hex value of the form #RBG, #RRGGBB, or #AARRGGBB",
                        hexValue));
            }
        }
    }

    public class iOSPlatformInitializer : MobilePlatformInitializer
    {
        protected override void RegisterTypes(ContainerBuilder builder)
        {
            base.RegisterTypes(builder);
        }
    }
}