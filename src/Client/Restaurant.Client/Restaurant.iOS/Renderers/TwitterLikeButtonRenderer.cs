using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Foundation;
using Restaurant.iOS.Helpers;
using Restaurant.iOS.Renderers;
using TwitterLikeButton.Xamarin.iOS;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using SizeF = CoreGraphics.CGSize;


[assembly: ExportRenderer(typeof(Restaurant.Mobile.UI.Controls.TwitterLikeButton), typeof(TwitterLikeButtonRenderer))]

namespace Restaurant.iOS.Renderers
{
	public class TwitterLikeButtonRenderer : ViewRenderer<Restaurant.Mobile.UI.Controls.TwitterLikeButton, UIButton>
	{
		protected override void OnElementChanged(ElementChangedEventArgs<Mobile.UI.Controls.TwitterLikeButton> e)
		{
			base.OnElementChanged(e);

			if (e.NewElement == null)
				return;

			var buttonRect = UIButton.FromType(UIButtonType.Custom);
			buttonRect.SetImage(UIImage.FromFile("heart.png"), UIControlState.Normal);
			SetNativeControl(buttonRect);

			buttonRect.TouchDown += ButtonRect_TouchDown;
		}

		readonly nfloat _minimumButtonHeight = 20;
		readonly nfloat _minimumButtonWidth = 20;

		public override SizeF SizeThatFits(SizeF size)
		{
			var result = base.SizeThatFits(size);

			result.Height = _minimumButtonHeight;
			result.Width = _minimumButtonWidth;

			return result;
		}

		private void ButtonRect_TouchDown(object sender, EventArgs e)
		{
			Control.Scale(false, 0.1D, () =>
			{
				Element.IsChecked = !Element.IsChecked;
				if (Element.IsChecked)
				{
					Control.SetImage(UIImage.FromFile("heart_fill.png"), UIControlState.Normal);
				}
				else
				{
					Control.SetImage(UIImage.FromFile("heart.png"), UIControlState.Normal);
				}
				Control.Scale(true, 0.2D);
			});
		}
	}
}

