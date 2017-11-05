using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Content.Res;
using Android.Graphics;
using Android.Graphics.Drawables;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

namespace Restaurant.Droid.Providers
{
	internal class DrawableProvider
	{
		internal Drawable GetFormsDrawable(Resources resource, FileImageSource fileImageSource)
		{
			var file = fileImageSource.File;
			Drawable drawable = resource.GetDrawable(fileImageSource);
			if (drawable == null)
			{
				var bitmap = resource.GetBitmap(file) ?? BitmapFactory.DecodeFile(file);
				if (bitmap != null)
					drawable = new BitmapDrawable(resource, bitmap);
			}
			return drawable;
		}
	}
}