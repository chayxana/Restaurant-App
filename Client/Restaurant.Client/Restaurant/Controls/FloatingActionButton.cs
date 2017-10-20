using Xamarin.Forms;

namespace Restaurant.Controls
{
    public class FloatingActionButton : Button
    {
        public static BindableProperty ButtonColorProperty = BindableProperty.Create(nameof(ButtonColor), typeof(Color), typeof(FloatingActionButton), Color.Accent);
        public Color ButtonColor
        {
            get => (Color)GetValue(ButtonColorProperty);
	        set => SetValue(ButtonColorProperty, value);
        }

	    public static BindableProperty RippleColorProperty = BindableProperty.Create(nameof(RippleColor), typeof(Color), typeof(FloatingActionButton), Color.Default);
	    public Color RippleColor
	    {
		    get => (Color)GetValue(RippleColorProperty);
		    set => SetValue(RippleColorProperty, value);
	    }
    }
}
