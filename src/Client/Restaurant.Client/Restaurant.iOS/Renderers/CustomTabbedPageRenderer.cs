using System;
using System.Collections.Generic;
using System.Text;
using Restaurant.iOS.Renderers;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(TabbedPage), typeof(CustomTabbedPageRenderer))]
namespace Restaurant.iOS.Renderers
{
	public class CustomTabbedPageRenderer : TabbedRenderer
	{
		public override void ViewWillAppear(bool animated)
		{
			base.ViewWillAppear(animated);

			TabBar.TintColor = UIColor.Black;
		}
	}
}
