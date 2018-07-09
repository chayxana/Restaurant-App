using Android.App;
using Android.Content;
using Android.Content.Res;
using Android.OS;
using Android.Util;
using Android.Views;
using Com.Mikepenz.Actionitembadge.Library;
using Com.Mikepenz.Actionitembadge.Library.Utils;
using Restaurant.Droid.Providers;
using Restaurant.Droid.Renderers;
using Restaurant.Mobile.UI.Controls;
using Restaurant.Mobile.UI.Pages;
using System;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.Platform.Android;
using Xamarin.Forms.Platform.Android.AppCompat;
using AColor = Android.Graphics.Color;
using AToolbar = Android.Support.V7.Widget.Toolbar;
using AView = Android.Views.View;

[assembly: ExportRenderer(typeof(CustomNavigationPage), typeof(CustomNavigationPageRenderer))]

namespace Restaurant.Droid.Renderers
{
    public class CustomNavigationPageRenderer : NavigationPageRenderer
    {
        private readonly DrawableProvider _drawableProvider = new DrawableProvider();
        private bool _disposed;
        private int _statusBarHeight = -1;

        public CustomNavigationPageRenderer(Context context) : base(context)
        {
        }

        private CustomNavigationPage PageController => Element as CustomNavigationPage;

        private MainActivity MainActivity => Context as MainActivity;

        protected override void OnToolbarItemPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnToolbarItemPropertyChanged(sender, e);
            UpdateBadge(sender as BadgeToolbarItem);
        }

        protected override void UpdateMenuItemIcon(Context context, IMenuItem menuItem, ToolbarItem toolBarItem)
        {
            base.UpdateMenuItemIcon(context, menuItem, toolBarItem);

            if (toolBarItem is BadgeToolbarItem item)
            {
                var color = item.BadgeColor.ToAndroid();
                var colorPressed = item.BadgePressedColor.ToAndroid();
                var textColor = item.BadgeTextColor.ToAndroid();

                var badgeStyle = new BadgeStyle(BadgeStyle.Style.Default,
                    Resource.Layout.menu_action_item_badge,
                    color,
                    colorPressed,
                    textColor);

                var iconDrawable = _drawableProvider.GetFormsDrawable(Context, item.Icon);
                ActionItemBadge.Update(MainActivity, 
                    menuItem, 
                    iconDrawable, 
                    badgeStyle, 
                    item.BadgeText, 
                    new MenuClickListener(item.Activate));
            }
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);
            if (e.PropertyName == NavigationPage.CurrentPageProperty.PropertyName)
            {
                if (IsNavigationBarTranslucent())
                {
                    PageController.BarBackgroundColor = Color.Transparent;
                    MainActivity.MakeStatusBarTranslucent(true);
                }
                else
                {
                    PageController.BarBackgroundColor = MainActivity.GetColorPrimary().ToColor();
                    MainActivity.MakeStatusBarTranslucent(false);
                }
            }
        }

        private void UpdateBadge(BadgeToolbarItem item)
        {
            if (item == null || item.BadgeText == null)
            {
                return;
            }

            var idx = PageController.CurrentPage.ToolbarItems.IndexOf(item);
            if (idx < 0)
            {
                return;
            }

            if (MainActivity.FindViewById(Resource.Id.toolbar) is AToolbar toolbar && toolbar.Menu.Size() > idx)
            {
                var color = item.BadgeColor.ToAndroid();
                var colorPressed = item.BadgePressedColor.ToAndroid();
                var textColor = item.BadgeTextColor.ToAndroid();

                var badgeStyle = new BadgeStyle(BadgeStyle.Style.Default,
                    Resource.Layout.menu_action_item_badge,
                    color,
                    colorPressed,
                    textColor);

                var iconDrawable = _drawableProvider.GetFormsDrawable(Context, item.Icon);
                var menuItem = toolbar.Menu.GetItem(idx);

                ActionItemBadge.Update(MainActivity, 
                    menuItem, 
                    iconDrawable, 
                    badgeStyle, 
                    item.BadgeText, 
                    new MenuClickListener(item.Activate));
            }
        }

        protected override void OnLayout(bool changed, int l, int t, int r, int b)
        {
            base.OnLayout(changed, l, t, r, b);

            if (IsNavigationBarTranslucent())
            {
                int containerHeight = b - t;
                PageController.ContainerArea = new Rectangle(0, 0, Context.FromPixels(r - l), Context.FromPixels(containerHeight));

                for (var i = 0; i < ChildCount; i++)
                {
                    AView child = GetChildAt(i);

                    if (child is AToolbar toolbar)
                    {
                        var barHeight = ActionBarHeight();
                        var statusBarHeight = GetStatusBarHeight();
                        toolbar.Layout(0, statusBarHeight, r - l, barHeight + statusBarHeight);
                        continue;
                    }

                    child.Layout(0, 0, r, b);
                }
            }
        }

        private bool IsNavigationBarTranslucent()
        {
            return Element.CurrentPage is ITransparentActionBarPage transparentPage 
                && transparentPage.IsTransparentActionBar;
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing && !_disposed)
            {
                _disposed = true;
            }

            base.Dispose(disposing);
        }

        private int ActionBarHeight()
        {
            var attr = Resource.Attribute.actionBarSize;

            int actionBarHeight;
            using (var tv = new TypedValue())
            {
                actionBarHeight = 0;
                if (Context.Theme.ResolveAttribute(attr, tv, true))
                {
                    actionBarHeight = TypedValue.ComplexToDimensionPixelSize(tv.Data, Resources.DisplayMetrics);
                }
            }

            if (actionBarHeight <= 0)
            {
                return IsPortrait() ? (int)Context.ToPixels(56) : (int)Context.ToPixels(48);
            }

            return actionBarHeight;
        }

        private int GetStatusBarHeight()
        {
            if (_statusBarHeight >= 0)
            {
                return _statusBarHeight;
            }

            var result = 0;
            var resourceId = Resources.GetIdentifier("status_bar_height", "dimen", "android");
            if (resourceId > 0)
            {
                result = Resources.GetDimensionPixelSize(resourceId);
            }

            return _statusBarHeight = result;
        }

        private bool IsPortrait()
        {
            return Context.Resources.Configuration.Orientation == Orientation.Portrait;
        }
    }
}