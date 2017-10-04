using System;
using Xamarin.Forms;

namespace Restaurant.Controls
{
    public class ParallaxScrollView : ScrollView
    {
        public ParallaxScrollView()
        {
            Scrolled += (sender, e) => Parallax();
        }

        public static readonly BindableProperty ParallaxViewProperty =
            BindableProperty.Create(nameof(ParallaxView), typeof(View), typeof(ParallaxScrollView), null);

        public View ParallaxView
        {
            get { return (View)GetValue(ParallaxViewProperty); }
            set { SetValue(ParallaxViewProperty, value); }
        }

        double height;
        public void Parallax()
        {
            if (ParallaxView == null || Device.OS == TargetPlatform.Windows || Device.OS == TargetPlatform.WinPhone)
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
            else if (Device.OS == TargetPlatform.iOS)
            {
                //Calculate a scale that equalizes the height vs scroll
                double newHeight = height + (ScrollY * -1);
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

