using System;
using System.Threading.Tasks;
using Android.App;
using Android.Content.PM;
using Android.OS;
using Android.Support.V7.App;
using Android.Util;
using Android.Views;
using ImageCircle.Forms.Plugin.Droid;
using Restaurant.Mobile.UI;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using AColor = Android.Graphics.Color;

namespace Restaurant.Droid
{
    [Activity(
        Label = "Restaurant-App", 
        Icon = "@drawable/icon",
        Theme = "@style/MainTheme",
        MainLauncher = true,
        ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : FormsAppCompatActivity
    {
        static MainActivity()
        {
            AppCompatDelegate.CompatVectorFromResourcesEnabled = true;
        }

        protected override void OnCreate(Bundle bundle)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.toolbar;

            base.OnCreate(bundle);

            Forms.Init(this, bundle);
            ImageCircleRenderer.Init();
            MakeStatusBarTranslucent(false);

            LoadApplication(new App());
        }       

        internal void MakeStatusBarTranslucent(bool makeTranslucent)
        {
            if (makeTranslucent)
            {
                SetStatusBarColor(AColor.Transparent);
                if (Build.VERSION.SdkInt >= BuildVersionCodes.Lollipop)
                {
                    Window.DecorView.SystemUiVisibility = (StatusBarVisibility)(SystemUiFlags.LayoutFullscreen | SystemUiFlags.LayoutStable);
                }
            }
            else
            {                
                SetStatusBarColor(GetColorPrimaryDark());
                if (Build.VERSION.SdkInt >= BuildVersionCodes.Lollipop)
                {
                    Window.DecorView.SystemUiVisibility = StatusBarVisibility.Visible;
                }
            }
        }

        internal AColor GetColorPrimaryDark()
        {
            using (var value = new TypedValue())
            {
                if (Theme.ResolveAttribute(Resource.Attribute.colorPrimaryDark, value, true))
                {
                    var color = new AColor(value.Data);
                    return color;
                }

                return AColor.Transparent;
            }
        }

        internal AColor GetColorPrimary()
        {
            using (var value = new TypedValue())
            {
                if (Theme.ResolveAttribute(Resource.Attribute.colorPrimary, value, true))
                {
                    var color = new AColor(value.Data);
                    return color;
                }

                return AColor.Transparent;
            }
        }
    }
}