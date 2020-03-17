using Android.Content;
using Android.Util;
using Xamarin.Forms.Platform.Android;

namespace Restaurant.Droid.Providers
{
    internal class ActionBarProvider
    {
        private readonly Context _context;

        public ActionBarProvider(Context context)
        {
            _context = context;
        }

        internal int GetActionBarHeight()
        {
            var attr = Resource.Attribute.actionBarSize;

            int actionBarHeight;
            using (var tv = new TypedValue())
            {
                actionBarHeight = 0;
                if (_context.Theme.ResolveAttribute(attr, tv, true))
                {
                    actionBarHeight = TypedValue.ComplexToDimensionPixelSize(tv.Data, _context.Resources.DisplayMetrics);
                }
            }

            if (actionBarHeight <= 0)
            {
                return IsPortrait() ? (int)_context.ToPixels(56) : (int)_context.ToPixels(48);
            }

            return actionBarHeight;
        }

        private bool IsPortrait()
        {
            return _context.Resources.Configuration.Orientation == Android.Content.Res.Orientation.Portrait;
        }
    }
}