using System.ComponentModel;
using System.Reflection;
using Android.App;
using Android.OS;
using Android.Util;
using Android.Views;
using Restaurant.Pages;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using Xamarin.Forms.Platform.Android.AppCompat;
using AToolbar = Android.Support.V7.Widget.Toolbar;
using AView = Android.Views.View;

[assembly: ExportRenderer(typeof(NavigationPage), typeof(Restaurant.Droid.Renderers.CustomNavigationPageRenderer))]
namespace Restaurant.Droid.Renderers
{
	public class CustomNavigationPageRenderer : NavigationPageRenderer
	{
		private AToolbar _toolbar;
		IPageController PageController => Element;

		protected override void OnElementChanged(ElementChangedEventArgs<NavigationPage> e)
		{
			base.OnElementChanged(e);

			var memberInfo = typeof(CustomNavigationPageRenderer).BaseType;
			if (memberInfo != null)
			{
				var field = memberInfo.GetField(nameof(_toolbar), BindingFlags.Instance | BindingFlags.NonPublic);
				_toolbar = field?.GetValue(this) as AToolbar;
			}
		}

		protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
		{
			base.OnElementPropertyChanged(sender, e);
			if (e.PropertyName == NavigationPage.CurrentPageProperty.PropertyName)
			{

				if ((int)Build.VERSION.SdkInt >= 21)
				{
					var navPage = Element;
					if (navPage.CurrentPage is IColoredPage page)
						SetThemeColors(page);
				}
			}
		}

		private void SetThemeColors(IColoredPage page)
		{
			if (Context is Activity context)
			{
				if (page.IsTransparentToolbar)
				{
					context.Window.DecorView.SystemUiVisibility = (StatusBarVisibility)(SystemUiFlags.LayoutFullscreen | SystemUiFlags.LayoutStable);
					context.Window.SetStatusBarColor(Color.Transparent.ToAndroid());
					_toolbar.SetBackgroundColor(Color.Transparent.ToAndroid());
				}
				else
				{
					context.Window.SetStatusBarColor(page.StatusBarColor.ToAndroid());
					context.Window.DecorView.SystemUiVisibility = StatusBarVisibility.Visible;
				}
			}
		}


		protected override void OnLayout(bool changed, int l, int t, int r, int b)
		{
			base.OnLayout(changed, l, t, r, b);

			var navPage = Element;
			if (!(navPage.CurrentPage is IColoredPage page))
				return;

			if (!page.IsTransparentToolbar)
				return;


			var context = Context as MainActivity;

			var statusBarHeight = context.GetStatusBarHeight();

			AToolbar bar = _toolbar;
			int barHeight = ActionBarHeight();
			
			int containerHeight = b - t;

			PageController.ContainerArea = new Rectangle(0, 0, Context.FromPixels(r - l), Context.FromPixels(containerHeight));
			Element.ForceLayout();

			for (var i = 0; i < ChildCount; i++)
			{
				AView child = GetChildAt(i);
				if (child is AToolbar)
				{
					bar.Layout(0, statusBarHeight, r - l, barHeight + statusBarHeight);
				}

				var pageContainer = child.GetType().GetProperty("Child")?.GetValue(child) as IVisualElementRenderer;

				if (!(pageContainer?.Element is Page childPage))
					return;

				bool childHasNavBar = NavigationPage.GetHasNavigationBar(childPage);

				if (childHasNavBar)
				{
					child.Layout(0, 0, r, b);
				}
			}
		}

		int ActionBarHeight()
		{
			int attr = Resource.Attribute.actionBarSize;

			int actionBarHeight;
			using (var tv = new TypedValue())
			{
				actionBarHeight = 0;
				if (Context.Theme.ResolveAttribute(attr, tv, true))
					actionBarHeight = TypedValue.ComplexToDimensionPixelSize(tv.Data, Resources.DisplayMetrics);
			}

			//if (actionBarHeight <= 0)
			//	return Device.Info.CurrentOrientation.IsPortrait() ? (int)Context.ToPixels(56) : (int)Context.ToPixels(48);

			return actionBarHeight;
		}
	}
}