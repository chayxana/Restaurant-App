using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using ReactiveUI;
using System.Reactive.Linq;
using Android.Graphics.Drawables;
using ImageCircle.Forms.Plugin.Droid;
using NControl.Controls.Droid;

namespace Restaurant.Droid
{
    [Activity(Label = "Restaurant", Icon = "@drawable/icon", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsApplicationActivity
    {
        protected override void OnCreate(Bundle bundle)
        {

            base.OnCreate(bundle);
            ActionBar.SetIcon(new ColorDrawable(Resources.GetColor(Android.Resource.Color.Transparent)));

            global::Xamarin.Forms.Forms.Init(this, bundle);
            ImageCircleRenderer.Init();
            NControls.Init();
            //if ((int)Android.OS.Build.VERSION.SdkInt >= 21) {  }

            UserError.RegisterHandler(ue =>
            {
                var toast = Toast.MakeText(this, ue.ErrorMessage, ToastLength.Short);
                toast.Show();

                return Observable.Return(RecoveryOptionResult.CancelOperation);
            });

            LoadApplication(new App());            
        }
        private int hot_number = 0;
        private TextView ui_hot = null;

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            menu.Clear();
            MenuInflater.Inflate(Resource.Menu.menu_actionbar, menu);
            View menu_hotlist = menu.FindItem(Resource.Id.menu_hotlist).ActionView;
            ui_hot = (TextView)menu_hotlist.FindViewById(Resource.Id.hotlist_hot);
            return true;
            //return base.OnCreateOptionsMenu(menu);
        }


        public override bool OnPrepareOptionsMenu(IMenu menu)
        {
            return base.OnPrepareOptionsMenu(menu);
        }
    }
}

