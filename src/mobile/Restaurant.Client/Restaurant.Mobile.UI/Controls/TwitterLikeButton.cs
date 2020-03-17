using Xamarin.Forms;

namespace Restaurant.Mobile.UI.Controls
{
    public enum LikeButtonIconType
    {
        Star,
        Heart,
        Thumb,
        Icon
    }

    public class TwitterLikeButton : View
    {
	    public static BindableProperty IconTypeProperty =
		    BindableProperty.Create(nameof(IconType), typeof(LikeButtonIconType), typeof(TwitterLikeButton),
			    LikeButtonIconType.Icon);

        public LikeButtonIconType IconType
        {
            get => (LikeButtonIconType) GetValue(IconTypeProperty);
            set => SetValue(IconTypeProperty, value);
        }

	    public static BindableProperty IsCheckedProperty =
		    BindableProperty.Create(nameof(IsChecked), typeof(bool), typeof(TwitterLikeButton), false);


	    public bool IsChecked
	    {
		    get => (bool)GetValue(IsCheckedProperty);
		    set => SetValue(IsCheckedProperty, value);
	    }
	}
}