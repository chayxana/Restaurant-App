using Android.App;
using Android.Graphics;
using Android.Util;

namespace Restaurant.Droid.Providers
{
	public class ColorProvider
	{
		public Color GetPimaryColor(Activity context)
		{
			int colorPrimaryAttr = context.Resources.GetIdentifier("colorPrimary", "attr", context.PackageName);
			
			var primaryOutValue = new TypedValue();
			context.Theme.ResolveAttribute(colorPrimaryAttr, primaryOutValue, true);
			var primary = primaryOutValue.Data;

			return new Color(primary);
		}

		public Color GetPrimaryDarkColor(Activity context)
		{
			int colorPrimaryDarkAttr = context.Resources.GetIdentifier("colorPrimaryDark", "attr", context.PackageName);

			var primaryDarkOutValue = new TypedValue();
			context.Theme.ResolveAttribute(colorPrimaryDarkAttr, primaryDarkOutValue, true);
			var primaryDark = primaryDarkOutValue.Data;
			return new Color(primaryDark);
		}
	}
}