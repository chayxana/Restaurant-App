using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(Restaurant.ThemedNavigationPage), typeof(Restaurant.iOS.Renderers.ThemedNavigationRenderer))]
namespace Restaurant.iOS.Renderers
{
    /// <summary>
    /// This custom NavigationRender is only necessary on iOS so we can change the navigation bar color *prior* to navigating instead of after
    /// Forms currently doesn't give us a lifecycle event before the navigation takes place
    /// This isn't an issue on Android
    /// </summary>
    public class ThemedNavigationRenderer : NavigationRenderer
    {
        protected override Task<bool> OnPushAsync(Page page, bool animated)
        {
            ChangeTheme(page);
            return base.OnPushAsync(page, animated);
        }

        public override UIViewController PopViewController(bool animated)
        {
            var obj = Element.GetType().InvokeMember("StackCopy", BindingFlags.GetProperty | BindingFlags.NonPublic | BindingFlags.Instance, Type.DefaultBinder, Element, null);
            if (obj != null)
            {
                var pages = obj as Stack<Page>;
                if (pages != null && pages.Count >= 2)
                {
                    var copy = new Page[pages.Count];
                    pages.CopyTo(copy, 0);

                    var prev = copy[1];
                    ChangeTheme(prev);
                }
            }
            return base.PopViewController(animated);
        }

        void ChangeTheme(Page page)
        {
            var basePage = page as MainBaseContentPage;
            if (basePage != null)
            {
                NavigationBar.BarTintColor = basePage.ActionBarBackgroundColor.ToUIColor();
                NavigationBar.TintColor = basePage.ActionBarTextColor.ToUIColor();

                var titleAttributes = new UIStringAttributes();
                titleAttributes.Font = UIFont.FromName("SegoeUI", 22);
                titleAttributes.ForegroundColor = basePage.ActionBarTextColor == Color.Default ? titleAttributes.ForegroundColor ?? UINavigationBar.Appearance.TintColor : basePage.ActionBarTextColor.ToUIColor();
                NavigationBar.TitleTextAttributes = titleAttributes;

                UIApplication.SharedApplication.StatusBarStyle = UIStatusBarStyle.LightContent;
            }
        }
    }
}
