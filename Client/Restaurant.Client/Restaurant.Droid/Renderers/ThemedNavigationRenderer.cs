using System.Linq;
using System.Threading.Tasks;
using Android.App;
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
            if (e.PropertyName == NavigationPage.CurrentPageProperty.PropertyName)
            {
                var navPage = Element;
                var page = navPage.CurrentPage as IColoredPage;
                if ((int)Android.OS.Build.VERSION.SdkInt >= 21)
                {
                    SetThemeColors(page);
                }
            }
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
            if (context != null)
            {
                context.Window.SetNavigationBarColor(basePage.NavigationBarColor.ToAndroid());
                context.Window.SetStatusBarColor(basePage.StatusBarColor.ToAndroid());

                var actionBar = context.ActionBar;
                ColorDrawable colorDrawable = new ColorDrawable(basePage.ActionBarBackgroundColor.ToAndroid());
                actionBar.SetBackgroundDrawable(colorDrawable);

                int titleId = context.Resources.GetIdentifier("action_bar_title", "id", "android");


                var page = basePage as Page;
                if (page == null) return;
                if(page.Title == "Foods")
                {
                    var basketMenu = page.ToolbarItems.FirstOrDefault(t => t.ClassId == "basket");
                    //page.ToolbarItems.Remove(basketMenu);
                    var tempContext = context as MainActivity;
                    if(tempContext != null)
                    {
                        //tempContext.SetActionBar()                                            
                    }                    
                }
            }

            //TextView abTitle = (TextView)context.FindViewById(titleId);
            //abTitle.SetTextColor(basePage.BarTextColor.ToAndroid());
        }
    }
}