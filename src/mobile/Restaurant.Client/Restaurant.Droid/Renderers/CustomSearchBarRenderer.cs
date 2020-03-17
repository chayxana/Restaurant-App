using Android.Content;
using Android.Graphics;
using Android.Support.V4.Content;
using Android.Support.V4.Graphics.Drawable;
using Android.Widget;
using Restaurant.Droid.Renderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using AView = Android.Views.View;
using AColor = Android.Graphics.Color;

[assembly: ExportRenderer(typeof(SearchBar), typeof(CustomSearchBarRenderer))]

namespace Restaurant.Droid.Renderers
{
    public class CustomSearchBarRenderer : SearchBarRenderer
    {
	    public CustomSearchBarRenderer(Context context) : base(context)
	    {	
	    }

        protected override void OnElementChanged(ElementChangedEventArgs<SearchBar> e)
        {
            base.OnElementChanged(e);

            var searchPlateId = Control.Resources.GetIdentifier("android:id/search_plate", null, null);
            if (searchPlateId != 0)
            {
                var v = FindViewById<AView>(searchPlateId);

                v.Background.SetColorFilter(AColor.White, PorterDuff.Mode.Multiply);
            }

            var searchButtonId = Control.Resources.GetIdentifier("android:id/search_mag_icon", null, null);
            if (searchButtonId != 0)
            {
                var image = FindViewById<ImageView>(searchButtonId);

                if (image != null && image.Drawable != null)
                    DrawableCompat.SetTint(image.Drawable,
                        ContextCompat.GetColor(Context, Android.Resource.Color.White));
            }
        }
    }
}