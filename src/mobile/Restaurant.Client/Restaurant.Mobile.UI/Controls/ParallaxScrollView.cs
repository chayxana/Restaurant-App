using Xamarin.Forms;

namespace Restaurant.Mobile.UI.Controls
{
	public class ParallaxScrollView : ScrollView
	{
		public static readonly BindableProperty ParallaxViewProperty =
			BindableProperty.Create(nameof(ParallaxView), typeof(View), typeof(ParallaxScrollView), null);

		private double height;

		public ParallaxScrollView()
		{
			Scrolled += (sender, e) => Parallax();
		}

		public View ParallaxView
		{
			get => (View)GetValue(ParallaxViewProperty);
			set => SetValue(ParallaxViewProperty, value);
		}

		public void Parallax()
		{
			if (ParallaxView == null || Device.RuntimePlatform == Device.UWP)
				return;

			if (height <= 0)
				height = ParallaxView.Height;

			var y = -(int)((float)ScrollY / 2.5f);
			if (y < 0)
			{
				//Move the Image's Y coordinate a fraction of the ScrollView's Y position
				ParallaxView.Scale = 1;
				ParallaxView.TranslationY = y;
			}
			else if (Device.RuntimePlatform == Device.iOS)
			{
				//Calculate a scale that equalizes the height vs scroll
				var newHeight = height + ScrollY * -1;
				ParallaxView.Scale = newHeight / height;
				ParallaxView.TranslationY = -(ScrollY / 2);
			}
			else
			{
				ParallaxView.Scale = 1;
				ParallaxView.TranslationY = 0;
			}
		}
	}
}