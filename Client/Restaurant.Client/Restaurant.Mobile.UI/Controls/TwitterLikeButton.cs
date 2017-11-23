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
        public static BindableProperty IconTypeProperty = BindableProperty.Create(nameof(IconType),
            typeof(LikeButtonIconType), typeof(TwitterLikeButton), LikeButtonIconType.Icon);

        public LikeButtonIconType IconType
        {
            get => (LikeButtonIconType) GetValue(IconTypeProperty);
            set => SetValue(IconTypeProperty, value);
        }
    }
}