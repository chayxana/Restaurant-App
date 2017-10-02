using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
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