using Android.App;
using Android.Content.PM;
using Android.OS;
using Android.Views;
using ImageCircle.Forms.Plugin.Droid;
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
            //Android.App.ActionBar.SetIcon(new ColorDrawable(Android.Graphics.Color.Transparent));
            Forms.Init(this, bundle);
            ImageCircleRenderer.Init();
            //if ((int)Android.OS.Build.VERSION.SdkInt >= 21) {  }

            //UserError.RegisterHandler(ue =>
            //{
            //    var toast = Toast.MakeText(this, ue.ErrorMessage, ToastLength.Short);
            //    toast.Show();

            //    return Observable.Return(RecoveryOptionResult.CancelOperation);
            //});
            
            LoadApplication(new App());          
            
            

        }

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            
            return base.OnCreateOptionsMenu(menu);
        }
    }
}

