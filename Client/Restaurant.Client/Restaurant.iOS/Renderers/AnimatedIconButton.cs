using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

namespace Restaurant.iOS.Renderers
{
    public class AnimatedIconButton : ButtonRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Button> e)
        {
            base.OnElementChanged(e);
            var a = ((UIButton) NativeView).ButtonType;
        }
    }
}