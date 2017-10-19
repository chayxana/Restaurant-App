using Android.OS;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using Button = Xamarin.Forms.Button;

[assembly: ExportRenderer(typeof(Restaurant.Controls.FlatButton), typeof(Restaurant.Droid.Renderers.FlatButtonRenderer))]
namespace Restaurant.Droid.Renderers
{
    public class FlatButtonRenderer : ButtonRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Button> e)
        {
            base.OnElementChanged(e);
            if (Build.VERSION.SdkInt >= BuildVersionCodes.Lollipop)
            {
                Control.StateListAnimator = null;
                //Control.TransformationMethod = null;
            }
        }
    }
}