using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CoreGraphics;
using Foundation;
using UIKit;

namespace Restaurant.iOS.Helpers
{
	public static class Animations
	{
		public static void Scale(this UIView view, bool isIn, double duration = 0.3, Action onFinished = null)
		{
			var minAlpha = (nfloat)0.0f;
			var maxAlpha = (nfloat)1.0f;
			var minTransform = CGAffineTransform.MakeScale((nfloat)0.1, (nfloat)0.1);
			var maxTransform = CGAffineTransform.MakeScale((nfloat)1, (nfloat)1);

			view.Alpha = isIn ? minAlpha : maxAlpha;
			view.Transform = isIn ? minTransform : maxTransform;
			UIView.Animate(duration, 0, UIViewAnimationOptions.CurveEaseInOut,
				() => {
					view.Alpha = isIn ? maxAlpha : minAlpha;
					view.Transform = isIn ? maxTransform : minTransform;
				},
				onFinished
			);
		}

		internal static T FindDescendantView<T>(this UIView view) where T : UIView
		{
			var queue = new Queue<UIView>();
			queue.Enqueue(view);

			while (queue.Count > 0)
			{
				var descendantView = queue.Dequeue();

				var result = descendantView as T;
				if (result != null)
					return result;

				for (var i = 0; i < descendantView.Subviews.Length; i++)
					queue.Enqueue(descendantView.Subviews[i]);
			}

			return null;
		}
	}
}