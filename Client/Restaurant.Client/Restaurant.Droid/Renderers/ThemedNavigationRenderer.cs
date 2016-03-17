using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using Android.Graphics.Drawables;
using System.ComponentModel;

[assembly: ExportRenderer(typeof(NavigationPage), typeof(Restaurant.Droid.Renderers.ThemedNavigationRenderer))]
namespace Restaurant.Droid.Renderers
{
    /// <summary>
    /// This custom NavigationRender is only necessary on iOS so we can change the navigation bar color *prior* to navigating instead of after
    /// Forms currently doesn't give us a lifecycle event before the navigation takes place
    /// This isn't an issue on Android
    /// </summary>
    public class ThemedNavigationRenderer : NavigationRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<NavigationPage> e)
        {
            base.OnElementChanged(e);            
        }
        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);
            //if (e.PropertyName == ThemedNavigationPage.CurrentPageProperty.PropertyName)
            //{
            //    var navPage = (NavigationPage)Element;
            //    var page = navPage.CurrentPage as IColoredPage;
            //    if ((int)Android.OS.Build.VERSION.SdkInt >= 21)
            //    {
            //        SetThemeColors(page);
            //    }
            //}
        }
        protected override Task<bool> OnPushAsync(Page view, bool animated)
        {
            ChangeTheme(view as IColoredPage);
            return base.OnPushAsync(view, animated);
        }

        protected override Task<bool> OnPopViewAsync(Page page, bool animated)
        {

            return base.OnPopViewAsync(page, animated);
        }

        void ChangeTheme(IColoredPage page)
        {
            var basePage = page as IColoredPage;
            if (basePage != null)
            {
                if ((int)Android.OS.Build.VERSION.SdkInt >= 21)
                {
                    SetThemeColors(basePage);
                }
            }
        }

        private void SetThemeColors(IColoredPage basePage)
        {
            var context = Context as Activity;
            context.Window.SetNavigationBarColor(basePage.NavigationBarColor.ToAndroid());
            context.Window.SetStatusBarColor(basePage.StatusBarColor.ToAndroid());

            var actionBar = context.ActionBar;
            ColorDrawable colorDrawable = new ColorDrawable(basePage.ActionBarBackgroundColor.ToAndroid());
            actionBar.SetBackgroundDrawable(colorDrawable);

            int titleId = context.Resources.GetIdentifier("action_bar_title", "id", "android");

            //TextView abTitle = (TextView)context.FindViewById(titleId);
            //abTitle.SetTextColor(basePage.BarTextColor.ToAndroid());
        }
    }
}