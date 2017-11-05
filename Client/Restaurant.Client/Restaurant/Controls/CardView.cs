using Xamarin.Forms;

namespace Restaurant.Controls
{
	public class CardView : Frame
	{
		public CardView()
		{
			Padding = 0;
			if (Device.RuntimePlatform == "iOS")
			{
				HasShadow = false;
				OutlineColor = Color.Transparent;
				BackgroundColor = Color.Transparent;
			}
		}
	}
}