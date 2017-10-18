using System.Reflection;
using Android.App;
using Android.Content.PM;
using Android.OS;
using Android.Views;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;


namespace Restaurant.Droid
{
    [Activity(Label = "Restaurant", Icon = "@drawable/icon", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : FormsAppCompatActivity
    {
	    int _statusBarHeight = -1;
		internal int GetStatusBarHeight()
	    {
		    if (_statusBarHeight >= 0)
			    return _statusBarHeight;

		    var result = 0;
		    int resourceId = Resources.GetIdentifier("status_bar_height", "dimen", "android");
		    if (resourceId > 0)
			    result = Resources.GetDimensionPixelSize(resourceId);
		    return _statusBarHeight = result;
	    }
		
		protected override void OnCreate(Bundle bundle)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.toolbar;

            base.OnCreate(bundle);
            
            Forms.Init(this, bundle);
	        ImageCircle.Forms.Plugin.Droid.ImageCircleRenderer.Init();
            LoadApplication(new App());


            XFGloss.Droid.Library.Init(this, bundle);
	        RoundedBoxView.Forms.Plugin.Droid.RoundedBoxViewRenderer.Init();
		}
	}
}

