using System.ComponentModel;
using Android.App;
using Android.Support.V7.Widget;
using Android.Widget;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using ListView = Android.Widget.ListView;
using ListViewScrollEffect = Restaurant.Droid.Effects.ListViewScrollEffect;
using AToolbar = Android.Support.V7.Widget.Toolbar;
using System;
using Android.Views;
using Android.Views.Animations;
using View = Android.Views.View;

[assembly: ExportEffect(typeof(ListViewScrollEffect), "ListViewScrollEffect")]
namespace Restaurant.Droid.Effects
{
	public class ListViewScrollEffect : PlatformEffect
	{
		private ListView _control;
		private AToolbar toolbar;

		protected override void OnAttached()
		{
			_control = Control as ListView;
			var context = (Activity)Forms.Context;
			var toolbarId = context.Resources.GetIdentifier("toolbar", "id", context.PackageName);
			toolbar = context.FindViewById<AToolbar>(toolbarId);

			_control.Scroll += _control_Scroll;
		}

		private void _control_Scroll(object sender, AbsListView.ScrollEventArgs e)
		{
			var c = _control.GetChildAt(0);
			int scrolly = -c.Top + _control.FirstVisiblePosition * c.Height;
			var context = (Activity)Forms.Context;
			var view = context.Window.DecorView;

			if (scrolly > 20)
			{
				toolbar.Animate().TranslationY(-toolbar.Bottom).SetInterpolator(new AccelerateInterpolator()).Start();
				var view1 = toolbar.Parent as View;
				view1.Layout(view1.Left, -toolbar.Bottom, view1.Right, view1.Bottom + toolbar.Bottom);
				//view.Measure(MeasureSpecFactory.MakeMeasureSpec(view.Right - view.Left, MeasureSpecMode.Exactly), MeasureSpecFactory.MakeMeasureSpec(view.Top - view.Bottom, MeasureSpecMode.Exactly));
				//view.Layout(view.Left, -toolbar.Bottom, view.Right, view.Bottom + toolbar.Bottom);

			}
			else
			{
				//toolbar.Animate().TranslationY(0).SetInterpolator(new AccelerateInterpolator()).Start();
				//var view1 = toolbar.Parent as View;
				//view1.Layout(view1.Left, toolbar.Bottom, view1.Right, view1.Bottom - toolbar.Bottom);
			}
		}

		protected override void OnDetached()
		{
		}
	}

	internal static class MeasureSpecFactory
	{
		public static int GetSize(int measureSpec)
		{
			const int modeMask = 0x3 << 30;
			return measureSpec & ~modeMask;
		}

		// Literally does the same thing as the android code, 1000x faster because no bridge cross
		// benchmarked by calling 1,000,000 times in a loop on actual device
		public static int MakeMeasureSpec(int size, MeasureSpecMode mode)
		{
			return size + (int)mode;
		}
	}
}