using Android.Content.Res;
using Android.Graphics;
using Android.Graphics.Drawables;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

namespace Restaurant.Droid.Providers
{
    internal class DrawableProvider
    {
        internal Drawable GetFormsDrawable(Resources resource, FileImageSource fileImageSource)
        {
            var file = fileImageSource.File;
            var drawable = resource.GetDrawable(fileImageSource);
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