using Com.Like;
using Restaurant.Droid.Renderers;
using Restaurant.Mobile.UI.Controls;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(TwitterLikeButton), typeof(TwitterLikeButtonRenderer))]

namespace Restaurant.Droid.Renderers
{
    public class
        TwitterLikeButtonRenderer : Xamarin.Forms.Platform.Android.AppCompat.ViewRenderer<TwitterLikeButton, LikeButton>
    {
        protected override void OnElementChanged(ElementChangedEventArgs<TwitterLikeButton> e)
        {
            base.OnElementChanged(e);

            if (e.NewElement == null)
                return;

            var twitterLikeButton = new LikeButton(Context);
            twitterLikeButton.SetIconSizeDp(14);
            twitterLikeButton.SetAnimationScaleFactor(3);
            twitterLikeButton.SetUnlikeDrawableRes(Resource.Drawable.like);
            twitterLikeButton.SetLikeDrawableRes(Resource.Drawable.like_fill);
            SetNativeControl(twitterLikeButton);
        }

        private IconType GetIconType(LikeButtonIconType type)
        {
            switch (type)
            {
                case LikeButtonIconType.Star:
                    return IconType.Star;
                case LikeButtonIconType.Heart:
                    return IconType.Heart;
                case LikeButtonIconType.Thumb:
                    return IconType.Thumb;
                default:
                    return IconType.Heart;
            }
        }

        protected override void OnLayout(bool changed, int l, int t, int r, int b)
        {
            base.OnLayout(changed, l, t, r, b);
            Control.BringToFront();
        }
    }
}