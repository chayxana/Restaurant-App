using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Android.App;
using Android.Content.Res;
using Android.Graphics;
using Android.Graphics.Drawables;
using Android.OS;
using Android.Util;
using Android.Views;
using Com.Mikepenz.Actionitembadge.Library;
using Com.Mikepenz.Actionitembadge.Library.Utils;
using Restaurant.Controls;
using Restaurant.Pages;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.Platform.Android;
using Xamarin.Forms.Platform.Android.AppCompat;
using AToolbar = Android.Support.V7.Widget.Toolbar;
using AView = Android.Views.View;
using Color = Xamarin.Forms.Color;
using AColor = Android.Graphics.Color;

[assembly: ExportRenderer(typeof(NavigationPage), typeof(Restaurant.Droid.Renderers.CustomNavigationPageRenderer))]
namespace Restaurant.Droid.Renderers
{
	public class CustomNavigationPageRenderer : NavigationPageRenderer
	{
		private AToolbar _toolbar;
		IPageController PageController => Element;
		ToolbarTracker _toolbarTracker;
		private bool _disposed;

		protected override void OnElementChanged(ElementChangedEventArgs<NavigationPage> e)
		{
			base.OnElementChanged(e);

			var memberInfo = typeof(CustomNavigationPageRenderer).BaseType;
			if (memberInfo != null)
			{
				var field = memberInfo.GetField(nameof(_toolbar), BindingFlags.Instance | BindingFlags.NonPublic);
				_toolbar = field?.GetValue(this) as AToolbar;
			}
			if (e.NewElement != null)
			{
				if (_toolbarTracker == null)
				{
					_toolbarTracker = new ToolbarTracker();
					_toolbarTracker.CollectionChanged += _toolbarTracker_CollectionChanged;
				}

				var parents = new List<Page>();
				Page root = Element;
				while (!IsApplicationOrNull(root.Parent))
				{
					root = (Page)root.Parent;
					parents.Add(root);
				}
				_toolbarTracker.Target = e.NewElement;
				_toolbarTracker.AdditionalTargets = parents;
				UpdateMenu(true);

			}

			if ((int)Build.VERSION.SdkInt >= 21)
			{
				var navPage = Element;
				if (navPage.CurrentPage is IColoredPage page)
					SetThemeColors(page);
			}
		}

		private void _toolbarTracker_CollectionChanged(object sender, EventArgs e)
		{
			UpdateMenu(true);
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

			if (_disposed)
				return;

			var navPage = Element;

			if (!(navPage.CurrentPage is IColoredPage page))
				return;

			if (!page.IsTransparentToolbar)
				return;


			if (!(Context is MainActivity context)) return;

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

		void UpdateMenu(bool init = false, string propertyName = null)
		{
			if (_disposed)
				return;

			var activity = (FormsAppCompatActivity)Context;
			AToolbar bar = _toolbar;
			IMenu menu = bar.Menu;

			if (init)
				UpdateToolbarItems(menu);

			UpdateBadgeToolbarItem(activity, menu, propertyName);
		}

		private void UpdateToolbarItems(IMenu menu)
		{
			foreach (ToolbarItem item in _toolbarTracker.ToolbarItems)
				item.PropertyChanged -= HandleToolbarItemPropertyChanged;

			menu.Clear();

			foreach (ToolbarItem item in _toolbarTracker.ToolbarItems)
			{
				IMenuItemController controller = item;
				item.PropertyChanged += HandleToolbarItemPropertyChanged;
				if (item.Order == ToolbarItemOrder.Secondary)
				{
					IMenuItem menuItem = menu.Add(item.Text);
					menuItem.SetEnabled(controller.IsEnabled);
					menuItem.SetOnMenuItemClickListener(new MenuClickListener(controller.Activate));
				}
				else
				{
					IMenuItem menuItem;
					if (item is BadgeToolbarItem badgeMenuItem)
						menuItem = menu.Add(0, badgeMenuItem.UniqId, 0, badgeMenuItem.Text);
					else
						menuItem = menu.Add(item.Text);

					FileImageSource icon = item.Icon;
					if (!string.IsNullOrEmpty(icon))
					{
						Drawable iconDrawable = GetFormsDrawable(Context.Resources, icon);
						if (iconDrawable != null)
							menuItem.SetIcon(iconDrawable);
					}
					menuItem.SetEnabled(controller.IsEnabled);
					menuItem.SetShowAsAction(ShowAsAction.Always);
					menuItem.SetOnMenuItemClickListener(new MenuClickListener(controller.Activate));
				}
			}
		}

		private void UpdateBadgeToolbarItem(FormsAppCompatActivity activity, IMenu menu, string propertyName)
		{
			foreach (BadgeToolbarItem item in _toolbarTracker.ToolbarItems.OfType<BadgeToolbarItem>())
			{
				IMenuItemController controller = item;
				item.PropertyChanged -= HandleToolbarItemPropertyChanged;
				item.PropertyChanged += HandleToolbarItemPropertyChanged;

				if (item.HasInitialized && propertyName == BadgeToolbarItem.BadgeTextProperty.PropertyName)
				{
					var menuItem1 = menu.FindItem(item.UniqId);
					ActionItemBadge.Update(menuItem1, item.BadgeText);
				}
				else
				{
					menu.RemoveItem(item.UniqId);
					IMenuItem menuItem = menu.Add(0, item.UniqId, 0, item.Text);
					FileImageSource icon = item.Icon;
					if (!string.IsNullOrEmpty(icon))
					{
						Drawable iconDrawable = GetFormsDrawable(Context.Resources, icon);
						if (iconDrawable != null)
							menuItem.SetIcon(iconDrawable);

						var color = item.BadgeColor.ToAndroid();
						var colorPressed = AColor.ParseColor("#CC0000");
						var textColor = item.BadgeTextColor.ToAndroid();

						var badgeStyle = new BadgeStyle(BadgeStyle.Style.Default,
							Resource.Layout.menu_action_item_badge,
							color,
							colorPressed,
							textColor);


						ActionItemBadge.Update(activity, menuItem, iconDrawable, badgeStyle, item.BadgeText);
						menuItem.ActionView.Click += (_, __) => controller.Activate();
						menuItem.SetEnabled(controller.IsEnabled);
						menuItem.SetShowAsAction(ShowAsAction.Always);
						item.HasInitialized = true;
					}
				}
			}
		}

		internal Drawable GetFormsDrawable(Resources resource, FileImageSource fileImageSource)
		{
			var file = fileImageSource.File;
			Drawable drawable = resource.GetDrawable(fileImageSource);
			if (drawable == null)
			{
				var bitmap = resource.GetBitmap(file) ?? BitmapFactory.DecodeFile(file);
				if (bitmap != null)
					drawable = new BitmapDrawable(resource, bitmap);
			}
			return drawable;
		}

		private void HandleToolbarItemPropertyChanged(object sender, PropertyChangedEventArgs e)
		{
			if (e.PropertyName == BadgeToolbarItem.BadgeTextProperty.PropertyName
				|| e.PropertyName == BadgeToolbarItem.BadgeTextColorProperty.PropertyName
				|| e.PropertyName == BadgeToolbarItem.BadgeColorProperty.PropertyName)
			{
				UpdateMenu(false, e.PropertyName);
			}
		}

		public bool IsApplicationOrNull(Element element)
		{
			return element == null || element is Xamarin.Forms.Application;
		}

		protected override void Dispose(bool disposing)
		{
			if (disposing && !_disposed)
			{
				_disposed = true;
			}
			base.Dispose(disposing);
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

	public class MenuClickListener : Java.Lang.Object, IMenuItemOnMenuItemClickListener
	{
		readonly Action _callback;

		public MenuClickListener(Action callback)
		{
			_callback = callback;
		}

		public bool OnMenuItemClick(IMenuItem item)
		{
			_callback();
			return true;
		}
	}
}