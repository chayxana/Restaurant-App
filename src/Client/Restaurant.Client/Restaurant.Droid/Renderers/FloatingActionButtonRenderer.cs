using System;
using System.ComponentModel;
using Android.Content;
using Android.Content.Res;
using Android.OS;
using Restaurant.Droid.Renderers;
using Restaurant.Mobile.UI.Controls;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using FAB = Android.Support.Design.Widget.FloatingActionButton;


[assembly: ExportRenderer(typeof(FloatingActionButton), typeof(FloatingActionButtonRenderer))]

namespace Restaurant.Droid.Renderers
{
    public class
        FloatingActionButtonRenderer : Xamarin.Forms.Platform.Android.AppCompat.ViewRenderer<FloatingActionButton, FAB>
    {
	    public FloatingActionButtonRenderer(Context context) : base(context)
	    {   
	    }

        protected override void OnElementChanged(ElementChangedEventArgs<FloatingActionButton> e)
        {
            if(Build.VERSION.SdkInt < BuildVersionCodes.Lollipop)
                return;

            base.OnElementChanged(e);

            if (e.NewElement == null)
                return;

            // set the bg
            var fab = new FAB(Context)
            {
                BackgroundTintList = ColorStateList.ValueOf(Element.ButtonColor.ToAndroid())
            };

            // set the icon
            var elementImage = Element.Image;
            var imageFile = elementImage?.File;

            if (imageFile != null)
                fab.SetImageDrawable(Context.GetDrawable(imageFile));
            //fab.SetRippleColor(Element.RippleColor.ToAndroid());

            fab.Click += Fab_Click;
            SetNativeControl(fab);
        }

        protected override void OnLayout(bool changed, int l, int t, int r, int b)
        {
            if (Build.VERSION.SdkInt < BuildVersionCodes.Lollipop)
                return;

            base.OnLayout(changed, l, t, r, b);
            Control.BringToFront();
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (Build.VERSION.SdkInt < BuildVersionCodes.Lollipop)
                return;
            var fab = Control;
            if (e.PropertyName == nameof(Element.ButtonColor))
                fab.BackgroundTintList = ColorStateList.ValueOf(Element.ButtonColor.ToAndroid());
            if (e.PropertyName == nameof(Element.Image))
            {
                var elementImage = Element.Image;
                var imageFile = elementImage?.File;

                if (imageFile != null)
                    fab.SetImageDrawable(Context.GetDrawable(imageFile));
            }
            if (e.PropertyName == nameof(Element.RippleColor))
            {
                //fab.SetRippleColor(Element.RippleColor.ToAndroid());
            }
            base.OnElementPropertyChanged(sender, e);
        }

        private void Fab_Click(object sender, EventArgs e)
        {
            // proxy the click to the element
            ((IButtonController) Element).SendClicked();
        }
    }
}