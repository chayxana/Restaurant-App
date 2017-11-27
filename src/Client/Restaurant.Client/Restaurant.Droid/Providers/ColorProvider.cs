using Android.Content;
using Android.Graphics;
using Android.Util;

namespace Restaurant.Droid.Providers
{
    public class ColorProvider
    {
	    private readonly Context _context;

	    public ColorProvider(Context context)
	    {
		    _context = context;
	    }

        public Color GetPimaryColor()
        {
            var colorPrimaryAttr = _context.Resources.GetIdentifier("colorPrimary", "attr", _context.PackageName);

            var primaryOutValue = new TypedValue();
            _context.Theme.ResolveAttribute(colorPrimaryAttr, primaryOutValue, true);
            var primary = primaryOutValue.Data;

            return new Color(primary);
        }

        public Color GetPrimaryDarkColor()
        {
            var colorPrimaryDarkAttr = _context.Resources.GetIdentifier("colorPrimaryDark", "attr", _context.PackageName);

            var primaryDarkOutValue = new TypedValue();
            _context.Theme.ResolveAttribute(colorPrimaryDarkAttr, primaryDarkOutValue, true);
            var primaryDark = primaryDarkOutValue.Data;
            return new Color(primaryDark);
        }
    }
}