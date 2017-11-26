using Android.Content;
using Android.Content.Res;
using Android.Graphics;
using Android.Graphics.Drawables;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

namespace Restaurant.Droid.Providers
{
    internal class DrawableProvider
    {
        internal Drawable GetFormsDrawable(Context context, FileImageSource fileImageSource)
        {
            var file = fileImageSource.File;
            var drawable = context.GetDrawable(fileImageSource);
            if (drawable == null)
            {
                var bitmap = context.Resources.GetBitmap(file) ?? BitmapFactory.DecodeFile(file);
                if (bitmap != null)
                    drawable = new BitmapDrawable(context.Resources, bitmap);
            }
            return drawable;
        }
    }
}