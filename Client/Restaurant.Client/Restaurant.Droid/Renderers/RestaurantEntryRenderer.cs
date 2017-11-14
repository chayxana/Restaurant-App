using System.ComponentModel;
using Android.Text;
using Android.Views;
using Restaurant.Droid.Renderers;
using Restaurant.Mobile.UI.Controls;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using Forms = Xamarin.Forms;

[assembly: ExportRenderer(typeof(RestaurantEntry), typeof(RestaurantEntryRenderer))]
namespace Restaurant.Droid.Renderers
{
    public class RestaurantEntryRenderer : EntryRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);

            var view = (RestaurantEntry)Element;

            SetFont(view);
            SetPlaceholderTextColor(view);
            SetTextAlignment(view);
            SetMaxLength(view);
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);

            var view = (RestaurantEntry)Element;

            if (e.PropertyName == RestaurantEntry.FontProperty.PropertyName)
                SetFont(view);

            if (e.PropertyName == RestaurantEntry.XAlignProperty.PropertyName)
                SetTextAlignment(view);

            if (e.PropertyName == RestaurantEntry.PlaceholderTextColorProperty.PropertyName)
            {
                SetPlaceholderTextColor(view);
            }
        }
        

        void SetTextAlignment(RestaurantEntry view)
        {
            switch (view.XAlign)
            {
                case Forms.TextAlignment.Center:
                    Control.Gravity = GravityFlags.CenterHorizontal;
                    break;
                case Forms.TextAlignment.End:
                    Control.Gravity = GravityFlags.End;
                    break;
                case Forms.TextAlignment.Start:
                    Control.Gravity = GravityFlags.Start;
                    break;
            }
        }

        void SetFont(RestaurantEntry view)
        {
            if (view.Font != Font.Default)
            {
                Control.TextSize = view.Font.ToScaledPixel();
            }
        }

        void SetMaxLength(RestaurantEntry view)
        {
            Control.SetFilters(new IInputFilter[] {
                new InputFilterLengthFilter(view.MaxLength)
            });
        }

        /// <summary>
        /// Sets the color of the placeholder text.
        /// </summary>
        /// <param name="view">The view.</param>
        private void SetPlaceholderTextColor(RestaurantEntry view)
        {
            if (view.PlaceholderTextColor != Color.Default)
            {
                Control.SetHintTextColor(view.PlaceholderTextColor.ToAndroid());
            }
        }
    }
}