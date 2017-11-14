using Android.App;
using Android.Content.PM;
using Android.OS;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;


namespace Restaurant.Droid
{
    [Activity(Label = "Restaurant", Icon = "@drawable/icon", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : FormsAppCompatActivity
    {	
		protected override void OnCreate(Bundle bundle)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.toolbar;

			base.OnCreate(bundle);
            
            Forms.Init(this, bundle);
	        ImageCircle.Forms.Plugin.Droid.ImageCircleRenderer.Init();
            LoadApplication(new Mobile.UI.App());


            XFGloss.Droid.Library.Init(this, bundle);
	        RoundedBoxView.Forms.Plugin.Droid.RoundedBoxViewRenderer.Init();
		}
	}
}

