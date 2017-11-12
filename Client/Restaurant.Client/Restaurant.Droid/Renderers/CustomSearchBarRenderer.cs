using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Support.V4.Content;
using Android.Support.V4.Graphics.Drawable;
using Android.Views;
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
		protected override void OnElementChanged(ElementChangedEventArgs<SearchBar> e)
		{
			base.OnElementChanged(e);

			int searchPlateId = Control.Resources.GetIdentifier("android:id/search_plate", null, null);
			if (searchPlateId != 0)
			{
				var v = FindViewById<AView>(searchPlateId);

				v.Background.SetColorFilter(AColor.White, PorterDuff.Mode.Multiply);
			}

			int searchButtonId = Control.Resources.GetIdentifier("android:id/search_mag_icon", null, null);
			if (searchButtonId != 0)
			{
				var image = FindViewById<ImageView>(searchButtonId);

				if (image != null && image.Drawable != null)
				{
                    DrawableCompat.SetTint(image.Drawable, ContextCompat.GetColor(Context, Android.Resource.Color.White));
				}
			}
		}
	}
}