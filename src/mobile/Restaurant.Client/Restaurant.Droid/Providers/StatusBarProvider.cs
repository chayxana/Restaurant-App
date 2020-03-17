using Android.Content;

namespace Restaurant.Droid.Providers
{
    internal class StatusBarProvider
    {
        private readonly Context _context;
        private int _statusBarHeight = -1;

        public StatusBarProvider(Context context)
        {
            _context = context;
        }

        internal int GetStatusBarHeight()
        {
            if (_statusBarHeight >= 0)
            {
                return _statusBarHeight;
            }

            var result = 0;
            var resourceId = _context.Resources.GetIdentifier("status_bar_height", "dimen", "android");
            if (resourceId > 0)
            {
                result = _context.Resources.GetDimensionPixelSize(resourceId);
            }

            return _statusBarHeight = result;
        }
    }
}