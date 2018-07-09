using Android.Content;
using Android.Graphics;
using Android.Graphics.Drawables;
using Android.Views;
using Com.Mikepenz.Actionitembadge.Library;
using Com.Mikepenz.Actionitembadge.Library.Utils;
using Restaurant.Droid.Renderers;
using Restaurant.Mobile.UI.Controls;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

namespace Restaurant.Droid.Providers
{
    internal class BadgeMenuItemProvider
    {
        private readonly Context _context;

        private MainActivity MainActivity => _context as MainActivity;

        public BadgeMenuItemProvider(Context context)
        {
            _context = context;
        }

        internal void UpdateMenuItemBadge(IMenuItem menuItem, BadgeToolbarItem item)
        {
            var color = item.BadgeColor.ToAndroid();
            var colorPressed = item.BadgePressedColor.ToAndroid();
            var textColor = item.BadgeTextColor.ToAndroid();

            var badgeStyle = new BadgeStyle(BadgeStyle.Style.Default,
                Resource.Layout.menu_action_item_badge,
                color,
                colorPressed,
                textColor);

            var iconDrawable = GetFormsDrawable(item.Icon);

            ActionItemBadge.Update(MainActivity,
                menuItem,
                iconDrawable,
                badgeStyle,
                item.BadgeText,
                new MenuClickListener(item.Activate));
        }

        private Drawable GetFormsDrawable(FileImageSource fileImageSource)
        {
            var file = fileImageSource.File;
            var drawable = _context.GetDrawable(fileImageSource);
            if (drawable == null)
            {
                var bitmap = _context.Resources.GetBitmap(file) ?? BitmapFactory.DecodeFile(file);
                if (bitmap != null)
                    drawable = new BitmapDrawable(_context.Resources, bitmap);
            }
            return drawable;
        }
    }
}