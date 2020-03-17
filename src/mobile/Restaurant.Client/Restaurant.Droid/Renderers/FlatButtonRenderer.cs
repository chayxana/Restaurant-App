using Android.Content;
using Android.OS;
using Restaurant.Droid.Renderers;
using Restaurant.Mobile.UI.Controls;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(FlatButton), typeof(FlatButtonRenderer))]

namespace Restaurant.Droid.Renderers
{
    public class FlatButtonRenderer : ButtonRenderer
    {
	    public FlatButtonRenderer(Context context) : base(context)
	    {   
	    }

        protected override void OnElementChanged(ElementChangedEventArgs<Button> e)
        {
            base.OnElementChanged(e);
            if (Build.VERSION.SdkInt >= BuildVersionCodes.Lollipop)
                Control.StateListAnimator = null;
        }
    }
}