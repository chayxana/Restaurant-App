using System.ComponentModel;
using Android.Text;
using Android.Widget;
using Restaurant.Droid.Effects;
using Restaurant.Mobile.UI.Effects;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportEffect(typeof(MaxLinesEffect), "MaxLinesEffect")]

namespace Restaurant.Droid.Effects
{
    public class MaxLinesEffect : PlatformEffect
    {
        private TextView _control;

        protected override void OnAttached()
        {
            _control = Control as TextView;
            SetMaxLines();
        }

        protected override void OnDetached()
        {
        }

        protected override void OnElementPropertyChanged(PropertyChangedEventArgs args)
        {
            if (args.PropertyName == NumberOfLinesEffect.NumberOfLinesProperty.PropertyName)
                SetMaxLines();
        }

        private void SetMaxLines()
        {
            var maxLines = NumberOfLinesEffect.GetNumberOfLines(Element);
            _control.SetMaxLines(maxLines);
            _control.Ellipsize = TextUtils.TruncateAt.End;
        }
    }
}