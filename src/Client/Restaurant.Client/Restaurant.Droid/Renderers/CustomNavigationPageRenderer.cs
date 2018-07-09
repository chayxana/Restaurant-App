using Android.Content;
using Android.Views;
using Restaurant.Droid.Providers;
using Restaurant.Droid.Renderers;
using Restaurant.Mobile.UI.Controls;
using Restaurant.Mobile.UI.Pages;
using System.ComponentModel;
using Xamarin.Forms;
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
        private readonly StatusBarProvider _statusBarProvider;
        private readonly ActionBarProvider _actionBarProvider;
        private readonly BadgeMenuItemProvider _badgeMenuItemProvider;

        public CustomNavigationPageRenderer(Context context) : base(context)
        {
            _statusBarProvider = new StatusBarProvider(context);
            _actionBarProvider = new ActionBarProvider(context);
            _badgeMenuItemProvider = new BadgeMenuItemProvider(context);
        }

        private CustomNavigationPage NavigationPage => Element as CustomNavigationPage;
        private MainActivity MainActivity => Context as MainActivity;

        protected override void OnToolbarItemPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnToolbarItemPropertyChanged(sender, e);

            if (sender is BadgeToolbarItem badgeToolbarItem)
            {
                BadgeToolbarItemPropertyChanged(badgeToolbarItem);
            }
        }

        protected override void UpdateMenuItemIcon(Context context, IMenuItem menuItem, ToolbarItem toolBarItem)
        {
            base.UpdateMenuItemIcon(context, menuItem, toolBarItem);

            if (toolBarItem is BadgeToolbarItem item)
            {
                _badgeMenuItemProvider.UpdateMenuItemBadge(menuItem, item);
            }
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);
            if (e.PropertyName == Xamarin.Forms.NavigationPage.CurrentPageProperty.PropertyName)
            {
                if (IsNavigationBarTranslucent())
                {
                    NavigationPage.BarBackgroundColor = Color.Transparent;
                    MainActivity.MakeStatusBarTranslucent(true);
                }
                else
                {
                    NavigationPage.BarBackgroundColor = MainActivity.GetColorPrimary().ToColor();
                    MainActivity.MakeStatusBarTranslucent(false);
                }
            }
        }

        private void BadgeToolbarItemPropertyChanged(BadgeToolbarItem badgeToolbarItem)
        {
            if (badgeToolbarItem?.BadgeText == null)
                return;

            var index = NavigationPage.CurrentPage.ToolbarItems.IndexOf(badgeToolbarItem);
            if (index < 0)
                return;

            if (MainActivity.FindViewById(Resource.Id.toolbar) is AToolbar toolbar && toolbar.Menu.Size() > index)
            {
                var menuItem = toolbar.Menu.GetItem(index);
                _badgeMenuItemProvider.UpdateMenuItemBadge(menuItem, badgeToolbarItem);
            }
        }

        protected override void OnLayout(bool changed, int l, int t, int r, int b)
        {
            base.OnLayout(changed, l, t, r, b);

            if (IsNavigationBarTranslucent())
            {
                int containerHeight = b - t;
                NavigationPage.ContainerArea = new Rectangle(0, 0, Context.FromPixels(r - l), Context.FromPixels(containerHeight));

                for (var i = 0; i < ChildCount; i++)
                {
                    AView child = GetChildAt(i);

                    if (child is AToolbar toolbar)
                    {
                        var (barHeight, statusBarHeight) = GetHeights();
                        toolbar.Layout(0, statusBarHeight, r - l, barHeight + statusBarHeight);
                        continue;
                    }

                    child.Layout(0, 0, r, b);
                }
            }
        }

        private (int actionBarHeight, int statusBarHeight) GetHeights()
        {
            var barHeight = _actionBarProvider.GetActionBarHeight();
            var statusBarHeight = _statusBarProvider.GetStatusBarHeight();
            return (barHeight, statusBarHeight);
        }

        private bool IsNavigationBarTranslucent()
        {
            return Element.CurrentPage is ITransparentActionBarPage transparentPage 
                && transparentPage.IsTransparentActionBar;
        }
    }
}