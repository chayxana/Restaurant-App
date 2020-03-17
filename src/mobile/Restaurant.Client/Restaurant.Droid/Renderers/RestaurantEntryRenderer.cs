using System.ComponentModel;
using Android.Content;
using Android.Text;
using Android.Views;
using Restaurant.Droid.Renderers;
using Restaurant.Mobile.UI.Controls;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(RestaurantEntry), typeof(RestaurantEntryRenderer))]

namespace Restaurant.Droid.Renderers
{
    public class RestaurantEntryRenderer : EntryRenderer
    {
	    public RestaurantEntryRenderer(Context context) : base(context)
	    {   
	    }

        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);

            var view = (RestaurantEntry) Element;

            SetFont(view);
            SetPlaceholderTextColor(view);
            SetTextAlignment(view);
            SetMaxLength(view);
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);

            var view = (RestaurantEntry) Element;

            if (e.PropertyName == RestaurantEntry.FontProperty.PropertyName)
                SetFont(view);

            if (e.PropertyName == RestaurantEntry.XAlignProperty.PropertyName)
                SetTextAlignment(view);

            if (e.PropertyName == RestaurantEntry.PlaceholderTextColorProperty.PropertyName)
                SetPlaceholderTextColor(view);
        }


        private void SetTextAlignment(RestaurantEntry view)
        {
            switch (view.XAlign)
            {
                case Xamarin.Forms.TextAlignment.Center:
                    Control.Gravity = GravityFlags.CenterHorizontal;
                    break;
                case Xamarin.Forms.TextAlignment.End:
                    Control.Gravity = GravityFlags.End;
                    break;
                case Xamarin.Forms.TextAlignment.Start:
                    Control.Gravity = GravityFlags.Start;
                    break;
            }
        }

        private void SetFont(RestaurantEntry view)
        {
            if (view.Font != Font.Default)
                Control.TextSize = view.Font.ToScaledPixel();
        }

        private void SetMaxLength(RestaurantEntry view)
        {
            Control.SetFilters(new IInputFilter[]
            {
                new InputFilterLengthFilter(view.MaxLength)
            });
        }

        /// <summary>
        ///     Sets the color of the placeholder text.
        /// </summary>
        /// <param name="view">The view.</param>
        private void SetPlaceholderTextColor(RestaurantEntry view)
        {
            if (view.PlaceholderTextColor != Color.Default)
                Control.SetHintTextColor(view.PlaceholderTextColor.ToAndroid());
        }
    }
}