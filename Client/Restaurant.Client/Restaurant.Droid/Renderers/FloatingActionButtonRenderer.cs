using System;
using System.ComponentModel;
using Android.Content.Res;
using Restaurant.Droid.Renderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using FAB = Android.Support.Design.Widget.FloatingActionButton;

[assembly: ExportRenderer(typeof(Restaurant.Controls.FloatingActionButton), typeof(FloatingActionButtonRenderer))]
namespace Restaurant.Droid.Renderers
{
    public class FloatingActionButtonRenderer : Xamarin.Forms.Platform.Android.AppCompat.ViewRenderer<Controls.FloatingActionButton, FAB>
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Controls.FloatingActionButton> e)
        {
            base.OnElementChanged(e);

            if (e.NewElement == null)
                return;

            var fab = new FAB(Context)
            {
                BackgroundTintList = ColorStateList.ValueOf(Element.ButtonColor.ToAndroid())
            };
            // set the bg

            // set the icon
            var elementImage = Element.Image;
            var imageFile = elementImage?.File;

            if (imageFile != null)
            {
                fab.SetImageDrawable(Context.Resources.GetDrawable(imageFile));
            }
			fab.SetRippleColor(Element.RippleColor.ToAndroid());
		
            fab.Click += Fab_Click;
            SetNativeControl(fab);

        }
        protected override void OnLayout(bool changed, int l, int t, int r, int b)
        {
            base.OnLayout(changed, l, t, r, b);
            Control.BringToFront();
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            var fab = (FAB)Control;
            if (e.PropertyName == nameof(Element.ButtonColor))
            {
                fab.BackgroundTintList = ColorStateList.ValueOf(Element.ButtonColor.ToAndroid());
            }
            if (e.PropertyName == nameof(Element.Image))
            {
                var elementImage = Element.Image;
                var imageFile = elementImage?.File;

                if (imageFile != null)
                {
                    fab.SetImageDrawable(Context.Resources.GetDrawable(imageFile));
                }
            }
	        if (e.PropertyName == nameof(Element.RippleColor))
	        {
		        fab.SetRippleColor(Element.RippleColor.ToAndroid());
	        }
            base.OnElementPropertyChanged(sender, e);

        }

        private void Fab_Click(object sender, EventArgs e)
        {
            // proxy the click to the element
            ((IButtonController)Element).SendClicked();
        }
    }
}